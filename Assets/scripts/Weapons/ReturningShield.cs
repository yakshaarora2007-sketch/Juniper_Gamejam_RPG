using UnityEngine;

public class ReturningShield : HybridWeapon
{
    public float returnSpeed;

    public bool shieldOut;

    public ReturningShield()
    {
        weaponID = WeaponID.ReturningShield;

        damage = 12f;

        range = 1f;

        projectileSpeed = 12f;

        returnSpeed = 15f;

        shieldOut = false;

        cooldown = 0.2f;
    }
}