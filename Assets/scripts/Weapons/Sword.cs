using UnityEngine;

public class Sword : MeleeWeapon
{
    public float swingDuration;

    public Sword()
    {
        weaponID = WeaponID.Sword;

        damage = 10f;

        range = 1f;

       swingDuration = 0.2f;

        cooldown = 0.3f;
    }
}