using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform sword;

    [Header("Sword")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackAngle = 120f;
    [SerializeField] private float swordDamage = 10f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Sword Swing")]
    [SerializeField] private float swingAngle = 75f;
    [SerializeField] private float swingDuration = 0.12f;

    [Header("Arrow")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowSpeed = 10f;
    [SerializeField] private float arrowWindupTime = 0.5f;

    [Header("Player Health")]
    public float health;
    public float maxhealth = 100f;
    public HealthBarscript_Player healthbar;

    private bool isSwinging;
    private float nextArrowTime;
    private Quaternion swordDefaultRotation;

    private void Start()
    {
        health = maxhealth;

        if (healthbar != null)
        {
            healthbar.SetMaxHealth(maxhealth);
        }

        if (sword != null)
        {
            swordDefaultRotation = sword.localRotation;
        }
    }

    public void SwordAttack(InputAction.CallbackContext context)
    {
        if (!context.performed || isSwinging)
            return;

        StartCoroutine(SwingSword());

        Collider2D[] enemies =
            Physics2D.OverlapCircleAll(
                attackPoint.position,
                attackRange,
                enemyLayer);

        Debug.Log("Enemies Found: " + enemies.Length);

        foreach (Collider2D enemy in enemies)
        {
            Vector2 directionToEnemy =
                (enemy.transform.position -
                 attackPoint.position).normalized;

            float angle =
                Vector2.Angle(
                    -attackPoint.up,
                    directionToEnemy);

            Debug.Log("Angle = " + angle);

            if (angle <= attackAngle * 0.5f)
            {
                Combat_enemy enemyHealth =
                    enemy.GetComponent<Combat_enemy>();

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(swordDamage);
                }

                Debug.Log("Sword Hit: " + enemy.name);
            }
        }
    }

    public void ShootArrow(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (Time.time < nextArrowTime)
            return;

        nextArrowTime = Time.time + arrowWindupTime;

        GameObject arrow =
            Instantiate(
                arrowPrefab,
                firePoint.position,
                firePoint.rotation);

        Rigidbody2D rb =
            arrow.GetComponent<Rigidbody2D>();

        rb.linearVelocity =
            -firePoint.up * arrowSpeed;
    }

    private IEnumerator SwingSword()
    {
        if (sword == null)
            yield break;

        if (swingDuration <= 0f)
            yield break;

        isSwinging = true;

        float timer = 0f;

        Quaternion startRotation =
            swordDefaultRotation *
            Quaternion.Euler(0, 0, -swingAngle);

        Quaternion endRotation =
            swordDefaultRotation *
            Quaternion.Euler(0, 0, swingAngle);

        while (timer < swingDuration)
        {
            timer += Time.deltaTime;

            float t = timer / swingDuration;

            sword.localRotation =
                Quaternion.Lerp(
                    startRotation,
                    endRotation,
                    t);

            yield return null;
        }

        sword.localRotation = swordDefaultRotation;

        isSwinging = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health < 0)
            health = 0;

        if (healthbar != null)
        {
            healthbar.SetHealth(health);
        }

        Debug.Log("Player HP: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Died");

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            attackPoint.position,
            attackRange);

        Vector3 leftBoundary =
            Quaternion.Euler(0, 0, attackAngle * 0.5f) *
            attackPoint.up *
            attackRange;

        Vector3 rightBoundary =
            Quaternion.Euler(0, 0, -attackAngle * 0.5f) *
            attackPoint.up *
            attackRange;

        Gizmos.DrawLine(
            attackPoint.position,
            attackPoint.position + leftBoundary);

        Gizmos.DrawLine(
            attackPoint.position,
            attackPoint.position + rightBoundary);
    }
}