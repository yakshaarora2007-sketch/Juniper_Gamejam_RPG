using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform hammerPoint;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform sword;

    [Header("Hammer")]
    [SerializeField] private float hammerRange = 0.5f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Sword Swing")]
    [SerializeField] private float swingAngle = 75f;
    [SerializeField] private float swingDuration = 0.12f;

    [Header("Arrow")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowSpeed = 10f;
    [SerializeField] private float arrowWindupTime = 0.5f;

    [Header("Player Health")]
    public float health;
    public float maxhealth = 100f;
    public HealthBarscript_Player healthbar;

    private bool isSwinging;
    private float nextArrowTime;

    private Quaternion swordDefaultRotation;

    private SwordHitbox swordHitbox;
    private WeaponManager weaponManager;

    private void Start()
    {
        weaponManager =
            GetComponentInChildren<WeaponManager>();

        swordHitbox =
            sword.GetComponent<SwordHitbox>();

        health = maxhealth;

        if (healthbar != null)
        {
            healthbar.SetMaxHealth(maxhealth);
        }

        if (sword != null)
        {
            swordDefaultRotation =
                sword.localRotation;
        }
    }

    public void MeleeAttack(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        Debug.Log(
            "Current Weapon = " +
            weaponManager.currentMeleeWeapon);

        switch (weaponManager.currentMeleeWeapon)
        {
            case WeaponID.Sword:
                SwordAttack();
                break;

            case WeaponID.Hammer:
                HammerAttack();
                break;

            case WeaponID.Trident:
                TridentAttack();
                break;

            case WeaponID.Spear:
                SpearSlash();
                break;

            case WeaponID.ReturningShield:
                ShieldBash();
                break;
        }
    }

    public void RangedAttack(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        switch (weaponManager.currentRangedWeapon)
        {
            case WeaponID.Bow:
                BowShoot();
                break;

            case WeaponID.Gun:
                GunShoot();
                break;

            case WeaponID.Shuriken:
                ShurikenThrow();
                break;

            case WeaponID.Spear:
                ThrowSpear();
                break;

            case WeaponID.ReturningShield:
                ThrowShield();
                break;
        }
    }

    private void SwordAttack()
    {
        if (isSwinging)
            return;

       StartCoroutine(SwordDamageWindow());
    }

    private void BowShoot()
    {
        if (Time.time < nextArrowTime)
            return;

        nextArrowTime =
            Time.time + arrowWindupTime;

        GameObject arrow =
            Instantiate(
                arrowPrefab,
                firePoint.position,
                firePoint.rotation);

        Rigidbody2D rb =
            arrow.GetComponent<Rigidbody2D>();

        rb.linearVelocity =
            -firePoint.up * arrowSpeed;
    }

    private void HammerAttack()
    {
        Debug.Log("Hammer Attack");
    }

    private void TridentAttack()
    {
        Debug.Log("Trident Attack");
    }

    private void SpearSlash()
    {
        Debug.Log("Spear Slash");
    }

    private void ShieldBash()
    {
        Debug.Log("Shield Bash");
    }

    private void GunShoot()
    {
        Debug.Log("Gun Shoot");
    }

    private void ShurikenThrow()
    {
        Debug.Log("Shuriken Throw");
    }

    private void ThrowSpear()
    {
        Debug.Log("Throw Spear");
    }

    private void ThrowShield()
    {
        Debug.Log("Throw Shield");
    }

    
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health < 0)
            health = 0;

        if (healthbar != null)
        {
            healthbar.SetHealth(health);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (hammerPoint == null)
            return;

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            hammerPoint.position,
            hammerRange);
    }

    private void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            weaponManager.EquipMeleeWeapon(
                WeaponID.Sword);
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            weaponManager.EquipMeleeWeapon(
                WeaponID.Hammer);
        }

        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            weaponManager.EquipMeleeWeapon(
                WeaponID.Trident);
        }
    }
    private IEnumerator SwordDamageWindow()
{
    swordHitbox.EnableDamage();

    yield return new WaitForSeconds(0.2f);

    swordHitbox.DisableDamage();
}
}