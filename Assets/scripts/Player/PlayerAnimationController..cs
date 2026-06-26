using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    #region Movement

    public void UpdateMovement(float speed)
    {
        animator.SetFloat("MoveSpeed", speed);
    }

    #endregion

    #region Weapon

    // Kept because WeaponManager calls this.
    // Your Animator no longer uses a Weapon parameter,
    // so this intentionally does nothing.
    public void UpdateWeapon(WeaponID meleeWeapon,
                             WeaponID rangedWeapon)
    {
    }

    #endregion

    #region Sword

    public void PlaySwordAttack()
    {
        animator.SetTrigger("SwordAttack");
    }

    #endregion

    #region Hammer

    public void PlayHammerAttack()
    {
        animator.SetTrigger("HammerAttack");
    }

    #endregion

    #region Trident

    public void PlayTridentAttack()
    {
        animator.SetTrigger("TridentAttack");
    }

    #endregion

    #region Bow

    public void StartBowDraw()
    {
        animator.SetTrigger("BowStart");
    }

    // BowHold is entered automatically after BowStart.
    // Combat code still calls this, so leave it as a no-op.
    public void BowReady()
    {
    }

    public void ReleaseBow()
    {
        animator.SetTrigger("BowRelease");
    }

    #endregion

    #region Gun

    public void RaiseGun()
    {
        animator.SetTrigger("GunRaise");
    }

    public void FireGun()
    {
        animator.SetTrigger("GunFire");
    }

    #endregion

    #region Shuriken

    public void PlayShurikenAttack()
    {
        animator.SetTrigger("ShurikenAttack");
    }

    #endregion

    #region Spear

    public void PlaySpearMelee()
    {
        animator.SetTrigger("SpearMelee");
    }

    public void PlaySpearThrow()
    {
        animator.SetTrigger("SpearThrow");
    }

    #endregion

    #region Shield

    public void PlayShieldMelee()
    {
        animator.SetTrigger("ShieldMelee");
    }

    public void PlayShieldThrow()
    {
        animator.SetTrigger("ShieldThrow");
    }

    #endregion

    #region Death

    public void PlayDeath()
    {
        animator.SetTrigger("Dead");
    }

    #endregion

    #region Compatibility

    // Legacy method kept so older code won't break.
    public void PlayAttack()
    {
        animator.SetTrigger("Attack");
    }

    #endregion

    #region Animation Events

    public void DestroyPlayer()
    {
        Destroy(gameObject);
    }

    #endregion
}