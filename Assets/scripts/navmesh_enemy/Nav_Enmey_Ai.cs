using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class navmesh_enemy : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] Transform target;
    [SerializeField] float detectionRange = 7f;
    [SerializeField] float attackRange = 1.5f;
    [SerializeField] float attackCooldown = 1f;

    // Removed obstacleLayer — was unused duplicate of losLayer
    [SerializeField] LayerMask losLayer;

    [SerializeField] float memoryDuration = 3f;
    [SerializeField] int bulletsPerBurst = 3;
    [SerializeField] float burstDelay = 0.15f;
    [SerializeField] float meleeDamage = 10f;

    // Tank knockback
    [SerializeField] float knockbackForce = 6f;
    [SerializeField] float knockbackDuration = 0.15f;

    float attackTimer;
    bool isBursting;
    float memoryTimer;
    NavMeshAgent agent;

    public enum EnemyType { Melee, Ranged, Tank, Archer }
    [SerializeField] EnemyType enemyType;

    enum EnemyState { Idle, Chase, Attack }
    EnemyState currentState;

    // ─────────────────────────────────────────
    //  Start
    // ─────────────────────────────────────────
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;   // FIX: was true, fought manual rotation
        agent.updateUpAxis   = false;

        // FIX: pre-fill timer so enemy doesn't attack instantly on first contact
        attackTimer  = attackCooldown;
        currentState = EnemyState.Idle;
    }

    // ─────────────────────────────────────────
    //  Update
    // ─────────────────────────────────────────
    private void Update()
    {
         if (!RoundManager.RoundRunning)
    {
        agent.ResetPath();
        return;
    }
        attackTimer += Time.deltaTime;
        memoryTimer -= Time.deltaTime;

        float distance = Vector2.Distance(transform.position, target.position);

        // ── State selection ──────────────────
        if (distance > detectionRange)
        {
            currentState = EnemyState.Idle;
        }
        else if (HasLineOfSight())
        {
            lastKnownPosition = target.position;
            memoryTimer       = memoryDuration;

            currentState = distance > attackRange
                ? EnemyState.Chase
                : EnemyState.Attack;
        }
        else if (memoryTimer > 0)
        {
            currentState = EnemyState.Chase;
        }
        else
        {
            currentState = EnemyState.Idle;
        }

        // ── State execution ──────────────────
        switch (currentState)
        {
            case EnemyState.Idle:
                agent.ResetPath();
                break;

            case EnemyState.Chase:
                agent.SetDestination(
                    HasLineOfSight() ? target.position : lastKnownPosition);
                break;

            case EnemyState.Attack:
                if (enemyType == EnemyType.Tank)
                    agent.SetDestination(target.position);
                else
                    agent.ResetPath();

                // FIX: face player with lerp instead of instant snap,
                // and only do it here — removed the duplicate velocity-based
                // rotation block that was overwriting this every frame
                FaceTarget();

                if (attackTimer >= attackCooldown)
                {
                    Attack();
                    attackTimer = 0f;
                }
                break;
        }

        // Rotation while moving (Chase / Idle wandering)
        // Only runs when NOT in Attack state to avoid the overwrite conflict
        if (currentState != EnemyState.Attack)
        {
            Vector2 vel = agent.velocity.normalized;
            if (vel.sqrMagnitude > 0.01f)
            {
                float   a   = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg - 90f;
                transform.rotation = Quaternion.Lerp(
                    transform.rotation,
                    Quaternion.Euler(0, 0, a),
                    10f * Time.deltaTime);
            }
        }
    }

    // ─────────────────────────────────────────
    //  LOS  — FIX: logic was inverted
    // ─────────────────────────────────────────
    Vector3 lastKnownPosition;

    bool HasLineOfSight()
    {
        Vector2 direction = target.position - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            direction.normalized,
            detectionRange,
            losLayer);

        Debug.DrawRay(
            transform.position,
            direction.normalized * detectionRange,
            Color.red);

        // FIX: null hit means nothing blocked the ray → clear LOS
        if (hit.collider == null) return true;

        // Hit something — is it the player?
        return hit.transform == target;
    }

    // ─────────────────────────────────────────
    //  Smooth rotation toward player
    // ─────────────────────────────────────────
    void FaceTarget()
    {
        Vector2 lookDir = target.position - transform.position;
        float   angle   = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.Euler(0, 0, angle),
            10f * Time.deltaTime);
    }

    // ─────────────────────────────────────────
    //  Attack dispatch
    // ─────────────────────────────────────────
    void Attack()
    {
        switch (enemyType)
        {
            case EnemyType.Melee:
                MeleeAttack();
                break;

            case EnemyType.Ranged:
                if (!isBursting)
                    StartCoroutine(BurstFire());
                break;

            case EnemyType.Archer:
                Shoot();
                break;

            case EnemyType.Tank:
                // FIX: Tank now does melee damage + knockback
                TankAttack();
                break;
        }
    }

    // ─────────────────────────────────────────
    //  Melee
    // ─────────────────────────────────────────
    void MeleeAttack()
    {
        PlayerCombat player = target.GetComponent<PlayerCombat>();
        if (player != null)
            player.TakeDamage(meleeDamage);
    }

    // ─────────────────────────────────────────
    //  Tank — melee + knockback
    // ─────────────────────────────────────────
    void TankAttack()
    {
        PlayerCombat player = target.GetComponent<PlayerCombat>();
        if (player != null)
            player.TakeDamage(meleeDamage);

        Rigidbody2D playerRb = target.GetComponent<Rigidbody2D>();
        if (playerRb != null)
            StartCoroutine(ApplyKnockback(playerRb));
    }

    IEnumerator ApplyKnockback(Rigidbody2D rb)
    {
        Vector2 dir = ((Vector2)target.position - (Vector2)transform.position).normalized;
        rb.linearVelocity = dir * knockbackForce;
        yield return new WaitForSeconds(knockbackDuration);
        rb.linearVelocity = Vector2.zero;
    }

    // ─────────────────────────────────────────
    //  Ranged — single shot
    // ─────────────────────────────────────────
    void Shoot()
    {
         if (!RoundManager.RoundRunning)
        return;
        if (projectilePrefab == null || firePoint == null) return;

        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.identity);

        Vector2 direction = (target.position - firePoint.position).normalized;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = direction * projectileSpeed;
    }

    // ─────────────────────────────────────────
    //  Ranged — burst fire
    // ─────────────────────────────────────────
   IEnumerator BurstFire()
{
    if (!RoundManager.RoundRunning)
        yield break;

    isBursting = true;

    for (int i = 0; i < bulletsPerBurst; i++)
    {
        if (!RoundManager.RoundRunning)
            yield break;

        Shoot();

        yield return new WaitForSeconds(burstDelay);
    }

    isBursting = false;
}

    // ─────────────────────────────────────────
    //  Gizmos
    // ─────────────────────────────────────────
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}