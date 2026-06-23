public class Trident : MeleeWeapon
{
    public float pointRadius;

    public Trident()
    {
        weaponID = WeaponID.Trident;

        damage = 8f;

        range = 1f;

        pointRadius = 0.3f;

        cooldown = 0.25f;
    }
}