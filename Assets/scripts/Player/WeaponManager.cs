using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponID currentMeleeWeapon;
    public WeaponID currentRangedWeapon;

    private void Start()
    {
        currentMeleeWeapon = WeaponID.Sword;
        currentRangedWeapon = WeaponID.Bow;
    }
public void EquipMeleeWeapon(WeaponID weapon)
{
    currentMeleeWeapon = weapon;

    Debug.Log("Melee Weapon Equipped: " + currentMeleeWeapon);
}

public void EquipRangedWeapon(WeaponID weapon)
{
    currentRangedWeapon = weapon;

    Debug.Log("Ranged Weapon Equipped: " + currentRangedWeapon);
}

    public bool IsHybrid(WeaponID weapon)
    {
        return weapon == WeaponID.Spear ||
               weapon == WeaponID.ReturningShield;
    }

    public void SetRoundWeapons(
        WeaponID meleeResult,
        WeaponID rangedResult)
    {
        if (IsHybrid(meleeResult))
        {
            currentMeleeWeapon = meleeResult;
            currentRangedWeapon = meleeResult;
            return;
        }

        if (IsHybrid(rangedResult))
        {
            currentMeleeWeapon = rangedResult;
            currentRangedWeapon = rangedResult;
            return;
        }

        
    }
    
}