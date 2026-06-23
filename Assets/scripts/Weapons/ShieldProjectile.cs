using System.Collections.Generic;
using UnityEngine;

public class ShieldProjectile : MonoBehaviour
{
    
    private float damage;

    private float ricochetRadius;

    private float returnSpeed;

    private int maxRicochets;

    private int currentRicochets;

    private bool returning;

    private Rigidbody2D rb;

    private Transform player;

    private ReturningShield shieldData;
    

    private readonly List<Combat_enemy>
        enemiesHit =
            new List<Combat_enemy>();

    public void Initialize(
        float shieldDamage,
        float shieldRicochetRadius,
        int shieldMaxRicochets,
        float shieldReturnSpeed,
        ReturningShield shield)
    {
        damage =
            shieldDamage;

        ricochetRadius =
            shieldRicochetRadius;

        maxRicochets =
            shieldMaxRicochets;

        returnSpeed =
            shieldReturnSpeed;

        shieldData =
            shield;

        rb =
            GetComponent<Rigidbody2D>();

        player =
            FindFirstObjectByType<PlayerCombat>()
            .transform;
    }

    private void Update()
    {
        if (!returning)
            return;

        Vector2 direction =
            (
                player.position -
                transform.position
            ).normalized;

        rb.linearVelocity =
            direction *
            returnSpeed;
    }

    private void OnTriggerEnter2D(
        Collider2D other)
    {
        if (returning)
        {
            PlayerCombat playerCombat =
                other.GetComponent<PlayerCombat>();

            if (playerCombat != null)
            {
                shieldData.shieldOut =
                    false;

                Destroy(gameObject);
            }

            return;
        }

        Combat_enemy enemy =
            other.GetComponent<Combat_enemy>();

      if (enemy != null)
{
    if (enemiesHit.Contains(enemy))
        return;

    enemy.TakeDamage(
        damage);

    enemiesHit.Add(enemy);

    Ricochet(enemy);

    return;
}

        StartReturn();
    }

    private void Ricochet(
        Combat_enemy currentEnemy)
    {
        if (currentRicochets >=
            maxRicochets)
        {
            StartReturn();
            return;
        }

        Collider2D[] nearby =
            Physics2D.OverlapCircleAll(
                transform.position,
                ricochetRadius);
Debug.Log(
    "Nearby Count = " +
    nearby.Length);
        Combat_enemy closestEnemy =
            null;

        float closestDistance =
            Mathf.Infinity;

foreach (Collider2D collider in nearby)
{
    Combat_enemy enemy =
        collider.GetComponent<Combat_enemy>();

    if (enemy == null)
        continue;

    if (enemy == currentEnemy)
        continue;

    if (enemiesHit.Contains(enemy))
        continue;

    Debug.Log("Checking: " + enemy.name);

    Vector2 direction =
        enemy.transform.position -
        transform.position;
RaycastHit2D[] hits =
    Physics2D.RaycastAll(
        transform.position,
        direction.normalized,
        direction.magnitude);

    bool blocked = false;

foreach (RaycastHit2D hit in hits)
{
    if (hit.collider == null)
        continue;

    if (hit.collider.gameObject ==
        gameObject)
        continue;

    Combat_enemy hitEnemy =
        hit.collider.GetComponent<Combat_enemy>();

    if (hitEnemy == enemy)
        break;

    blocked = true;
    break;
}

if (blocked)
{
    Debug.Log(
        "Blocked: " +
        enemy.name);

    continue;
}

    float distance =
        direction.magnitude;

    if (distance < closestDistance)
    {
        closestDistance =
            distance;

        closestEnemy =
            enemy;
    }
}
        if (closestEnemy == null)
        {
            StartReturn();
            return;
        }
Debug.Log(
    "Ricocheting To: " +
    closestEnemy.name);
        currentRicochets++;

        Vector2 ricochetDirection =
            (
                closestEnemy.transform.position -
                transform.position
            ).normalized;

        rb.linearVelocity =
            ricochetDirection *
            rb.linearVelocity.magnitude;
    }

    private void StartReturn()
    {
        returning = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color =
            Color.cyan;

        Gizmos.DrawWireSphere(
            transform.position,
            ricochetRadius);
    }
}