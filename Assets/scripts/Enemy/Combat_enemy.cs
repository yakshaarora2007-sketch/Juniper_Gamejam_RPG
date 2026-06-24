using UnityEngine;
using System.Collections;

public class Combat_enemy : MonoBehaviour
{
    [Header("Health")]
    public float health;
    public float maxhealth = 30f;

    [Header("Health Bar")]
    public EnemyHealthbar healthbar;

    [Header("Contact Damage")]
    [SerializeField] private float collisionDamage = 10f;
    [SerializeField] private float damageCooldown = 1f;

    [Header("Tank Knockback")]
    [SerializeField] private bool applyKnockback = false;
    [SerializeField] private float knockbackForce = 6f;
    [SerializeField] private float knockbackDuration = 0.25f;

    private float nextDamageTime;

    private void Start()
    {
        health = maxhealth;

        if (healthbar != null)
            healthbar.SetMaxHealth(maxhealth);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("TAKE DAMAGE CALLED!");

        health -= damage;

        Debug.Log(gameObject.name + " HP: " + health);

        if (healthbar != null)
            healthbar.SetHealth(health);

        if (health <= 0)
            Die();

        Debug.Log("Healthbar is null? " + (healthbar == null));
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Time.time < nextDamageTime)
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerCombat player = collision.gameObject.GetComponent<PlayerCombat>();

            if (player != null)
            {
                player.TakeDamage(collisionDamage);

                if (applyKnockback)
                {
                    Rigidbody2D playerRb =
                        collision.gameObject.GetComponent<Rigidbody2D>();
                    Movement playerMovement =
                        collision.gameObject.GetComponent<Movement>();

                    if (playerRb != null)
                        StartCoroutine(Knockback(playerRb, playerMovement, collision.transform));
                }

                nextDamageTime = Time.time + damageCooldown;
            }
        }
    }

    private IEnumerator Knockback(Rigidbody2D rb, Movement playerMovement, Transform playerTransform)
    {
        // Disable player movement so it can't overwrite knockback velocity
        if (playerMovement != null)
            playerMovement.enabled = false;

        Vector2 dir = ((Vector2)playerTransform.position - (Vector2)transform.position).normalized;
        rb.linearVelocity = dir * knockbackForce;

        yield return new WaitForSeconds(knockbackDuration);

        rb.linearVelocity = Vector2.zero;

        // Re-enable player movement
        if (playerMovement != null)
            playerMovement.enabled = true;
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " Died");
        Destroy(gameObject);
    }
}