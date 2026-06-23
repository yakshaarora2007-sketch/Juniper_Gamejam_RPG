public class Bow : RangedWeapon
{
    public float drawTime;

    public bool isDrawing;

    public bool bowReady;

    public float drawStartTime;

    public Bow()
    {
        weaponID = WeaponID.Bow;

        damage = 20f;

        drawTime = 1f;

        projectileSpeed = 15f;
    }
}