public class Hammer : MeleeWeapon
{
    public float impactRadius;

    public Hammer()
    {
        weaponID = WeaponID.Hammer;

        damage = 25f;

        range = 0.5f;

        impactRadius = 0.5f;

        windupTime = 0.2f;

        cooldown = 0.5f;
    }
}