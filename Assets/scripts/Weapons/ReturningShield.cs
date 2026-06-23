using UnityEngine;

public class ReturningShield : HybridWeapon
{
    public float bashDamage;
    
public float bashRange;
public bool shieldOut;

public float returnSpeed = 16f;

public int maxRicochets = 3;

public float ricochetRadius = 50f;

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