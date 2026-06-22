using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float arrowDamage = 7.5f;
    [SerializeField] private float lifetime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Arrow hit " + other.name);

        Combat_enemy enemy =
            other.GetComponent<Combat_enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(arrowDamage);
        }

        Destroy(gameObject);
    }
}