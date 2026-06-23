using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public float damage = 10f;

    private bool canDamage;
    private Collider2D swordCollider;
    public void EnableDamage()
    {
        canDamage = true;
        swordCollider.enabled = true;
    }

   public void DisableDamage()
{
    canDamage = false;
    swordCollider.enabled = false;
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
private void Start()
    {
        swordCollider = GetComponent<Collider2D>();
        swordCollider.enabled = false;
    }
    
}
