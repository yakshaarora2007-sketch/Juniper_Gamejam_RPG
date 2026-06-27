using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [Header("Current Weapon UI")]

    [SerializeField] private Image meleeWeaponImage;
    [SerializeField] private TMP_Text meleeWeaponName;

    [SerializeField] private Image rangedWeaponImage;
    [SerializeField] private TMP_Text rangedWeaponName;

    [SerializeField] private TMP_Text ammoText;

    [Header("Weapon Icons")]

    [SerializeField] private Sprite swordIcon;
    [SerializeField] private Sprite hammerIcon;
    [SerializeField] private Sprite tridentIcon;
    [SerializeField] private Sprite bowIcon;
    [SerializeField] private Sprite gunIcon;
    [SerializeField] private Sprite shurikenIcon;
    [SerializeField] private Sprite spearIcon;
    [SerializeField] private Sprite shieldIcon;

    private void Start()
    {
        ammoText.gameObject.SetActive(false);
    }

    public void SetMeleeWeapon(WeaponID weapon)
    {
        meleeWeaponName.text = weapon.ToString();
        meleeWeaponImage.sprite = GetWeaponSprite(weapon);
    }

    public void SetRangedWeapon(WeaponID weapon)
    {
        rangedWeaponName.text = weapon.ToString();
        rangedWeaponImage.sprite = GetWeaponSprite(weapon);

        switch (weapon)
        {
            case WeaponID.Bow:
                ammoText.gameObject.SetActive(true);
                ammoText.text = "Ammo : ∞";
                break;

            case WeaponID.Gun:
            case WeaponID.Shuriken:
            case WeaponID.Spear:
                ammoText.gameObject.SetActive(true);
                break;

            default:
                ammoText.gameObject.SetActive(false);
                break;
        }
    }

public void UpdateAmmo(int currentAmmo, int maxAmmo)
{
    ammoText.gameObject.SetActive(true);

    if (currentAmmo < 0 || maxAmmo < 0)
    {
        ammoText.text = "Ammo : ∞";
        return;
    }

    ammoText.text =
        "Ammo : " +
        currentAmmo +
        " / " +
        maxAmmo;
}

    public void HideAmmo()
    {
        ammoText.gameObject.SetActive(false);
    }

    private Sprite GetWeaponSprite(WeaponID weapon)
    {
        switch (weapon)
        {
            case WeaponID.Sword:
                return swordIcon;

            case WeaponID.Hammer:
                return hammerIcon;

            case WeaponID.Trident:
                return tridentIcon;

            case WeaponID.Bow:
                return bowIcon;

            case WeaponID.Gun:
                return gunIcon;

            case WeaponID.Shuriken:
                return shurikenIcon;

            case WeaponID.Spear:
                return spearIcon;

            case WeaponID.ReturningShield:
                return shieldIcon;

            default:
                return null;
        }
    }
}