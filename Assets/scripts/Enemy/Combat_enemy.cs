using UnityEngine;

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
    [Header("KnockBack")]

    [SerializeField] float knockbackDistance = 2f;

    private float nextDamageTime;

    private void Start()
    {
        health = maxhealth;

        if (healthbar != null)
        {
            healthbar.SetMaxHealth(maxhealth);
        }
    }

    public void TakeDamage(float damage)
{
    Debug.Log("TAKE DAMAGE CALLED!");

    health -= damage;

    Debug.Log(gameObject.name + " HP: " + health);

    if (healthbar != null)
    {
        healthbar.SetHealth(health);
    }

    if (health <= 0)
    {
        Die();
    }
    Debug.Log("Healthbar is null? " + (healthbar == null));
}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Time.time < nextDamageTime)
            return;

        if (collision.gameObject.layer ==
            LayerMask.NameToLayer("Player"))
        {
            PlayerCombat player =
                collision.gameObject.GetComponent<PlayerCombat>();

            if (player != null)
            {
                player.TakeDamage(collisionDamage);

                        player.ApplyKnockback(
                            transform.position,
                            knockbackDistance);
                nextDamageTime =
                    Time.time + damageCooldown;
            }
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " Died");

        Destroy(gameObject);
    }

    
    
}