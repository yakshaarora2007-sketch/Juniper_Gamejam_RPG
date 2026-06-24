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
        health -= damage;

        if (health < 0)
            health = 0;

        if (healthbar != null)
        {
            healthbar.SetHealth(health);
        }

        Debug.Log(gameObject.name + " HP: " + health);

        if (health <= 0)
        {
            Die();
        }
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
    
 private float timer;

private void Update()
{
    timer += Time.deltaTime;

    if(timer > 2f)
    {
        TakeDamage(10);
        timer = 0f;
    }
}
}