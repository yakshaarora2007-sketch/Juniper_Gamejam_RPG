using UnityEngine;

public class Spear : HybridWeapon
{
    public int maxSpears = 2;

    public int currentSpears = 2;

    public int pierceCount = 999;

    public float throwDuration = 8f;

    public float pickupLifetime = 8f;
public float pokeWidth = 0.3f;
    public Spear()
    {
        weaponID = WeaponID.Spear;

        damage = 18f;

        range = 1.5f;

        projectileSpeed = 20f;

        cooldown = 0.3f;
    }
}