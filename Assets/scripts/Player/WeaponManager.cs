using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon currentMeleeWeapon = null;
    public Weapon currentRangedWeapon = null;

    private PlayerAnimationController animationController;

    private Sword sword;
    private Hammer hammer;
    private Trident trident;

    private Bow bow;
    private Gun gun;
    private Shuriken shuriken;

    private Spear spear;
    private ReturningShield returningShield;

    private void Start()
    {
        sword = new Sword();
        hammer = new Hammer();
        trident = new Trident();

        bow = new Bow();
        gun = new Gun();
        shuriken = new Shuriken();

        spear = new Spear();
        returningShield = new ReturningShield();

        currentMeleeWeapon = sword;
        currentRangedWeapon = bow;

        animationController =
            FindFirstObjectByType<PlayerAnimationController>();

        animationController.UpdateWeapon(
            currentMeleeWeapon.weaponID,
            currentRangedWeapon.weaponID);
    }

    public void EquipWeapon(
        WeaponID weaponID)
    {
        switch (weaponID)
        {
            case WeaponID.Sword:
                currentMeleeWeapon = sword;
                break;

            case WeaponID.Hammer:
                currentMeleeWeapon = hammer;
                break;

            case WeaponID.Trident:
                currentMeleeWeapon = trident;
                break;

            case WeaponID.Bow:
                currentRangedWeapon = bow;
                break;

            case WeaponID.Gun:
                currentRangedWeapon = gun;
                break;

            case WeaponID.Shuriken:
                currentRangedWeapon = shuriken;
                break;

            case WeaponID.Spear:
                currentMeleeWeapon = spear;
                currentRangedWeapon = spear;
                break;

            case WeaponID.ReturningShield:
                currentMeleeWeapon = returningShield;
                currentRangedWeapon = returningShield;
                break;
        }

        animationController.UpdateWeapon(
            currentMeleeWeapon.weaponID,
            currentRangedWeapon.weaponID);

        Debug.Log(
            "Melee: " +
            currentMeleeWeapon.weaponID +
            " | Ranged: " +
            currentRangedWeapon.weaponID);
    }

    public void SetMeleeWeapon(
        WeaponID weaponID)
    {
        switch (weaponID)
        {
            case WeaponID.Sword:
                currentMeleeWeapon = sword;
                break;

            case WeaponID.Hammer:
                currentMeleeWeapon = hammer;
                break;

            case WeaponID.Trident:
                currentMeleeWeapon = trident;
                break;

            case WeaponID.Spear:
                currentMeleeWeapon = spear;
                break;

            case WeaponID.ReturningShield:
                currentMeleeWeapon = returningShield;
                break;
        }

        animationController.UpdateWeapon(
            currentMeleeWeapon.weaponID,
            currentRangedWeapon.weaponID);

        Debug.Log(
            "Melee Equipped: " +
            currentMeleeWeapon.weaponID);
    }

    public void SetRangedWeapon(
        WeaponID weaponID)
    {
        switch (weaponID)
        {
            case WeaponID.Bow:
                currentRangedWeapon = bow;
                break;

            case WeaponID.Gun:
                currentRangedWeapon = gun;
                break;

            case WeaponID.Shuriken:
                currentRangedWeapon = shuriken;
                break;

            case WeaponID.Spear:
                currentRangedWeapon = spear;
                break;

            case WeaponID.ReturningShield:
                currentRangedWeapon = returningShield;
                break;
        }

        animationController.UpdateWeapon(
            currentMeleeWeapon.weaponID,
            currentRangedWeapon.weaponID);

        Debug.Log(
            "Ranged Equipped: " +
            currentRangedWeapon.weaponID);
    }
}