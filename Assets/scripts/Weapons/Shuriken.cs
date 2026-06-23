public class Shuriken : RangedWeapon
{
    public int projectileCount;

    public float spreadAngle;

    public float cooldown;

    public float nextThrowTime;

    public Shuriken()
    {
        weaponID = WeaponID.Shuriken;

        damage = 15f;

        projectileSpeed = 12f;

        projectileCount = 3;

        spreadAngle = 30f;

        cooldown = 0.4f;
    }
}