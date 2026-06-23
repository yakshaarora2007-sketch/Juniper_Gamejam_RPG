using UnityEngine;

public class SpearProjectile : MonoBehaviour
{
    private float damage;

    private int enemiesHit;

    private bool canBePickedUp;

    private bool hasLanded;

    [SerializeField] private float throwDuration = 8f;

    [SerializeField] private float pickupLifetime = 8f;

    [SerializeField] private int pierceCount = 999;

    public void Initialize(float spearDamage)
    {
        damage = spearDamage;

        Debug.Log("Spear Initialized");

        Invoke(
            nameof(Land),
            throwDuration);
    }

    private void OnTriggerEnter2D(
        Collider2D other)
    {
        if (hasLanded)
            return;

        // Ignore player
        if (other.GetComponent<PlayerCombat>() != null)
            return;

        Debug.Log(
            "Spear Hit: " +
            other.name);

        Combat_enemy enemy =
            other.GetComponent<Combat_enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(
                damage);

            enemiesHit++;

            if (enemiesHit >= pierceCount)
            {
                Land();
            }

            return;
        }

        // Any non-enemy object
        // (walls, borders, obstacles)
        Land();
    }

    private void Land()
    {
        if (hasLanded)
            return;

        Debug.Log(
            "Spear Landed");

        hasLanded = true;

        Rigidbody2D rb =
            GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity =
                Vector2.zero;

            rb.angularVelocity =
                0f;

            rb.bodyType =
                RigidbodyType2D.Kinematic;
        }

        canBePickedUp = true;

        Invoke(
            nameof(Expire),
            pickupLifetime);
    }

    private void Expire()
    {
        Destroy(gameObject);
    }

    public bool CanBePickedUp()
    {
        return canBePickedUp;
    }
}