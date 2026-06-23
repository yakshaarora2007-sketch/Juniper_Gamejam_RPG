using UnityEngine;

public class SpearPickup : MonoBehaviour
{
    private SpearProjectile spearProjectile;

    private void Start()
    {
        spearProjectile =
            GetComponent<SpearProjectile>();
    }

   private void OnTriggerEnter2D(
    Collider2D other)
{
    PlayerCombat player =
        other.GetComponentInParent<PlayerCombat>();

    if (player == null)
        return;

    if (!spearProjectile.CanBePickedUp())
        return;

    WeaponManager weaponManager =
        player.GetComponentInChildren<WeaponManager>();

    if (weaponManager == null)
        return;

    Spear spear =
        weaponManager.currentRangedWeapon
        as Spear;

    if (spear == null)
        return;

    if (spear.currentSpears >=
        spear.maxSpears)
        return;

    spear.currentSpears++;

    Debug.Log(
        "Picked Up Spear: " +
        spear.currentSpears +
        "/" +
        spear.maxSpears);

    Destroy(gameObject);
}
}