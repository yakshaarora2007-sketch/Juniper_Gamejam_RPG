using UnityEngine;

[System.Serializable]
public class Gun : RangedWeapon
{
    public int magazineSize = 30;

    public int currentAmmo = 30;

    public float fireRate = 0.1f;

    public float reloadTime = 2f;

    [HideInInspector]
    public float nextShotTime;

    [HideInInspector]
    public bool isReloading;

    public Gun()
    {
        weaponID = WeaponID.Gun;

        damage = 5f;

        projectileSpeed = 20f;
    }
}