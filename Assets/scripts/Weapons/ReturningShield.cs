using UnityEngine;

public class ReturningShield : HybridWeapon
{
    public float bashDamage = 5f;
    
public float bashRange = 100f;
public bool shieldOut;

public float returnSpeed = 16f;

public int maxRicochets = 3;

public float ricochetRadius = 50f;

    public ReturningShield()
    {
        weaponID = WeaponID.ReturningShield;

        damage = 12f;

        range = 5f;

        projectileSpeed = 12f;

        returnSpeed = 15f;

        shieldOut = false;

        cooldown = 0.2f;
    }
}