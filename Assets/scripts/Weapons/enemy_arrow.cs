using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float lifetime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.name);
        PlayerCombat player =
            other.GetComponentInParent<PlayerCombat>();

        Debug.Log(player != null);
        if(player != null)
        {
            player.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}