using UnityEngine;
using EasyUI.PickerWheelUI;

public class WeaponWheelManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private PickerWheel meleeWheel;

    [SerializeField]
    private PickerWheel rangedWheel;

    [SerializeField]
    private WeaponManager weaponManager;

    private WheelPiece selectedMelee;
    private WheelPiece selectedRanged;

    private void Start()
    {   
         meleeWheel.gameObject.SetActive(true);
    rangedWheel.gameObject.SetActive(false);

    meleeWheel.OnSpinEnd(
        OnMeleeSelected);

    rangedWheel.OnSpinEnd(
        OnRangedSelected);

    StartMeleeWheel();
    }

   public void StartMeleeWheel()
{
    RoundManager.RoundRunning = false;

    meleeWheel.gameObject.SetActive(true);
    rangedWheel.gameObject.SetActive(false);

    meleeWheel.Spin();
}

    private void OnMeleeSelected(
        WheelPiece piece)
    {
        selectedMelee = piece;

        Debug.Log(
            "Melee Selected: " +
            piece.Label);

      
        rangedWheel.gameObject.SetActive(true);
        meleeWheel.gameObject.SetActive(false);

        StartRangedWheel();
    }

    private void StartRangedWheel()
    {
        rangedWheel.Spin();
    }

   private void OnRangedSelected(WheelPiece piece)
{
    selectedRanged = piece;

    EquipWeapons();

    meleeWheel.gameObject.SetActive(false);
    rangedWheel.gameObject.SetActive(false);

    RoundManager.Instance.StartCountdown();
}

    private void EquipWeapons()
    {
        WeaponID meleeID =
            GetWeaponID(
                selectedMelee.Label);

        WeaponID rangedID =
            GetWeaponID(
                selectedRanged.Label);

        bool meleeHybrid =
            meleeID == WeaponID.Spear ||
            meleeID == WeaponID.ReturningShield;

        bool rangedHybrid =
            rangedID == WeaponID.Spear ||
            rangedID == WeaponID.ReturningShield;

        if (rangedHybrid)
        {
            weaponManager.EquipWeapon(
                rangedID);

            Debug.Log(
                "Hybrid Override: " +
                rangedID);

            return;
        }

        if (meleeHybrid)
        {
            weaponManager.EquipWeapon(
                meleeID);

            Debug.Log(
                "Hybrid Override: " +
                meleeID);

            return;
        }

        weaponManager.SetMeleeWeapon(
            meleeID);

        weaponManager.SetRangedWeapon(
            rangedID);

        Debug.Log(
            "Melee = " +
            meleeID +
            " | Ranged = " +
            rangedID);
    }

    private WeaponID GetWeaponID(
        string label)
    {
        switch (label)
        {
            case "Sword":
                return WeaponID.Sword;

            case "Hammer":
                return WeaponID.Hammer;

            case "Trident":
                return WeaponID.Trident;

            case "Spear":
                return WeaponID.Spear;

            case "Shield":
                return WeaponID.ReturningShield;

            case "Bow":
                return WeaponID.Bow;

            case "Gun":
                return WeaponID.Gun;

            case "Shuriken":
                return WeaponID.Shuriken;
        }

        return WeaponID.Sword;
    }
public void StartWeaponSelection()
{
    RoundManager.RoundRunning = false;

    meleeWheel.gameObject.SetActive(true);
    rangedWheel.gameObject.SetActive(false);

    StartMeleeWheel();
}
}