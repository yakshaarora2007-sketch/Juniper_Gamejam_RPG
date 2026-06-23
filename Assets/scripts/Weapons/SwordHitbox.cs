using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public float damage = 10f;

    private bool canDamage;

    public void EnableDamage()
    {
        canDamage = true;
    }

    public void DisableDamage()
    {
        canDamage = false;
    }

private void OnTriggerStay2D(Collider2D other)
{
    if (!canDamage)
        return;

    Combat_enemy enemy =
        other.GetComponent<Combat_enemy>();

    if (enemy != null)
    {
        enemy.TakeDamage(damage);

        Debug.Log("Sword damaged " + other.name);

        canDamage = false;
    }
}
    
}
