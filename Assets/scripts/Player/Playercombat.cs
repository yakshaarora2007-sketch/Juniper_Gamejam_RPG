using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform hammerPoint;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform sword;
    private float nextAttackTime;

   [Header("Trident")]
[SerializeField] private Transform tridentLeftPoint;
[SerializeField] private Transform tridentCenterPoint;
[SerializeField] private Transform tridentRightPoint;

[Header("Projectiles")]
[SerializeField] private GameObject arrowPrefab;
[SerializeField] private GameObject bulletPrefab;
[SerializeField] private GameObject shurikenPrefab;
[SerializeField] private GameObject spearPrefab;
[SerializeField] private GameObject shieldPrefab;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Player Health")]
    public float health;
    public float maxhealth = 100f;
    public HealthBarscript_Player healthbar;

private Coroutine reloadRoutine;
    private SwordHitbox swordHitbox;
    private bool gunTriggerHeld;
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

       
    }

    public void MeleeAttack(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        Debug.Log(
            "Current Weapon = " +
            weaponManager.currentMeleeWeapon);

       switch (weaponManager.currentMeleeWeapon.weaponID)
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
private void DamageTridentPoint(
    Vector2 point,
    float radius,
    float damage)
{
    Collider2D[] enemies =
        Physics2D.OverlapCircleAll(
            point,
            radius,
            enemyLayer);

    foreach (Collider2D enemy in enemies)
    {
        Combat_enemy combatEnemy =
            enemy.GetComponent<Combat_enemy>();

        if (combatEnemy != null)
        {
            combatEnemy.TakeDamage(
                damage);
        }
    }
}
    public void RangedAttack(InputAction.CallbackContext context)
    {
        

        switch (weaponManager.currentRangedWeapon.weaponID)
        {
            case WeaponID.Bow:
                BowShoot(context);
                break;

            case WeaponID.Gun:

    if (context.started)
    {
        gunTriggerHeld = true;
    }

    if (context.canceled)
    {
        gunTriggerHeld = false;
    }

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
    Sword swordWeapon =
        weaponManager.currentMeleeWeapon
        as Sword;

    if (swordWeapon == null)
        return;

    StartCoroutine(
        SwordDamageWindow(
            swordWeapon.swingDuration));
}
private void BowShoot(
    InputAction.CallbackContext context)
{
    Bow bow =
        weaponManager.currentRangedWeapon
        as Bow;

    if (bow == null)
        return;

    if (context.started)
    {
        bow.isDrawing = true;
        bow.bowReady = false;
        bow.drawStartTime = Time.time;

        Debug.Log("Started Drawing");
    }

    if (context.canceled)
    {
        bow.isDrawing = false;

        if (!bow.bowReady)
        {
            Debug.Log("Released Too Early");
            return;
        }

        GameObject arrow =
            Instantiate(
                arrowPrefab,
                firePoint.position,
                firePoint.rotation);

        Rigidbody2D rb =
            arrow.GetComponent<Rigidbody2D>();

        rb.linearVelocity =
            -firePoint.up *
            bow.projectileSpeed;

        bow.bowReady = false;

        Debug.Log("Arrow Fired");
    }
}
private void HammerAttack()
{
    Hammer hammer =
        weaponManager.currentMeleeWeapon
        as Hammer;

    if (hammer == null)
        return;

    if (Time.time < nextAttackTime)
        return;

    nextAttackTime =
        Time.time + hammer.cooldown;

    Collider2D[] hitEnemies =
        Physics2D.OverlapCircleAll(
            hammerPoint.position,
            hammer.impactRadius,
            enemyLayer);

    Debug.Log(
        "Hammer Hit Count = " +
        hitEnemies.Length);

    foreach (Collider2D enemy in hitEnemies)
    {
        Combat_enemy combatEnemy =
            enemy.GetComponent<Combat_enemy>();

        if (combatEnemy != null)
        {
            combatEnemy.TakeDamage(
                hammer.damage);
        }
    }
}
private void TridentAttack()
{
    Trident trident =
        weaponManager.currentMeleeWeapon
        as Trident;

    if (trident == null)
        return;

    DamageTridentPoint(
        tridentLeftPoint.position,
        trident.pointRadius,
        trident.damage);

    DamageTridentPoint(
        tridentCenterPoint.position,
        trident.pointRadius,
        trident.damage);

    DamageTridentPoint(
        tridentRightPoint.position,
        trident.pointRadius,
        trident.damage);
}
private void SpearSlash()
{
    Spear spear =
        weaponManager.currentMeleeWeapon
        as Spear;

    if (spear == null)
        return;
        if (spear.currentSpears <= 0)
{
    Debug.Log(
        "No Spears Available");

    return;
}

    RaycastHit2D[] hits =
        Physics2D.BoxCastAll(
            firePoint.position,
            new Vector2(
                spear.pokeWidth,
                spear.range),
            firePoint.eulerAngles.z,
            firePoint.up,
            0f,
            enemyLayer);

    foreach (RaycastHit2D hit in hits)
    {
        Combat_enemy enemy =
            hit.collider.GetComponent<Combat_enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(
                spear.damage);
        }
    }
}

   private void ShieldBash()
{
    ReturningShield shield =
        weaponManager.currentMeleeWeapon
        as ReturningShield;

    if (shield == null)
        return;

    if (Time.time < nextAttackTime)
        return;

    nextAttackTime =
        Time.time + shield.cooldown;

    Collider2D[] hitEnemies =
        Physics2D.OverlapCircleAll(
    firePoint.position,
    shield.bashRange,
    enemyLayer);

    Debug.Log(
        "Shield Hit Count = " +
        hitEnemies.Length);

    foreach (Collider2D enemy in hitEnemies)
    {
      Combat_enemy combatEnemy =
    enemy.GetComponentInParent<Combat_enemy>();

        if (combatEnemy != null)
        {
            combatEnemy.TakeDamage(
                shield.bashDamage);
        }
        Debug.Log(
    "Shield damaged " +
    enemy.name);
    }
}

   private void ShurikenThrow()
{
    Shuriken shuriken =
        weaponManager.currentRangedWeapon
        as Shuriken;

    if (shuriken == null)
        return;

    if (Time.time < shuriken.nextThrowTime)
        return;

    shuriken.nextThrowTime =
        Time.time + shuriken.cooldown;

    float angleStep = 0f;

if (shuriken.projectileCount > 1)
{
    angleStep =
        shuriken.spreadAngle /
        (shuriken.projectileCount - 1);
}

    float startAngle =
        -shuriken.spreadAngle / 2f;

    for (int i = 0;
         i < shuriken.projectileCount;
         i++)
    {
        float angle =
            startAngle +
            angleStep * i;

        Quaternion rotation =
            firePoint.rotation *
            Quaternion.Euler(
                0,
                0,
                angle);

        GameObject projectile =
            Instantiate(
                shurikenPrefab,
                firePoint.position,
                rotation);

        Rigidbody2D rb =
            projectile.GetComponent<Rigidbody2D>();

        Vector2 direction =
            rotation * Vector2.down;

        rb.linearVelocity =
            direction *
            shuriken.projectileSpeed;
    }
}

private void ThrowSpear()
{
    Spear spear =
        weaponManager.currentRangedWeapon
        as Spear;

    if (spear == null)
        return;

    if (Time.time < nextAttackTime)
        return;

    nextAttackTime =
        Time.time + spear.cooldown;

    if (spear.currentSpears <= 0)
        return;

    spear.currentSpears--;

    Debug.Log(
        "Spears Left = " +
        spear.currentSpears);

    GameObject projectile =
        Instantiate(
            spearPrefab,
            firePoint.position,
            firePoint.rotation);

    SpearProjectile spearProjectile =
        projectile.GetComponent<SpearProjectile>();

    if (spearProjectile != null)
    {
        spearProjectile.Initialize(
            spear.damage);
    }

    Rigidbody2D rb =
        projectile.GetComponent<Rigidbody2D>();

    rb.linearVelocity =
        -firePoint.up *
        spear.projectileSpeed;
}
private void ThrowShield()
{
    Debug.Log("Shield Thrown");
    ReturningShield shield =
        weaponManager.currentRangedWeapon
        as ReturningShield;

    if (shield == null)
        return;

    if (shield.shieldOut)
        return;

    shield.shieldOut = true;

    GameObject projectile =
        Instantiate(
            shieldPrefab,
            firePoint.position,
            firePoint.rotation);

    ShieldProjectile shieldProjectile =
        projectile.GetComponent<ShieldProjectile>();

    if (shieldProjectile != null)
    {
        shieldProjectile.Initialize(
            shield.damage,
            shield.ricochetRadius,
            shield.maxRicochets,
            shield.returnSpeed,
            shield);
    }

    Rigidbody2D rb =
        projectile.GetComponent<Rigidbody2D>();

    rb.linearVelocity =
        -firePoint.up *
        shield.projectileSpeed;
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
       
        if (firePoint != null)
{
    Gizmos.color = Color.yellow;

    Gizmos.DrawWireSphere(
        firePoint.position,
        1.5f);
}

        Gizmos.color = Color.blue;

if (tridentLeftPoint != null)
{
    Gizmos.DrawWireSphere(
        tridentLeftPoint.position,
        0.25f);
}

if (tridentCenterPoint != null)
{
    Gizmos.DrawWireSphere(
        tridentCenterPoint.position,
        0.25f);
}

if (tridentRightPoint != null)
{
    Gizmos.DrawWireSphere(
        tridentRightPoint.position,
        0.25f);
}
       if (hammerPoint != null)
{
    Gizmos.color = Color.red;

    Gizmos.DrawWireSphere(
        hammerPoint.position,
        0.5f);
}
    }

   private void Update()
   
{
    Gun gun =
    weaponManager.currentRangedWeapon
    as Gun;

if (gun != null)
{
    if (Keyboard.current.rKey.wasPressedThisFrame)
    {
        if (!gun.isReloading &&
            gun.currentAmmo <
            gun.magazineSize)
        {
            reloadRoutine =
                StartCoroutine(
                    ReloadGun(gun));
        }
    }
}
    UpdateGun();
    UpdateBow();
    if (Keyboard.current.digit1Key.wasPressedThisFrame)
        weaponManager.EquipWeapon(
            WeaponID.Sword);

    if (Keyboard.current.digit2Key.wasPressedThisFrame)
        weaponManager.EquipWeapon(
            WeaponID.Hammer);

    if (Keyboard.current.digit3Key.wasPressedThisFrame)
        weaponManager.EquipWeapon(
            WeaponID.Trident);

    if (Keyboard.current.digit4Key.wasPressedThisFrame)
        weaponManager.EquipWeapon(
            WeaponID.Bow);

    if (Keyboard.current.digit5Key.wasPressedThisFrame)
        weaponManager.EquipWeapon(
            WeaponID.Gun);

    if (Keyboard.current.digit6Key.wasPressedThisFrame)
        weaponManager.EquipWeapon(
            WeaponID.Shuriken);

    if (Keyboard.current.digit7Key.wasPressedThisFrame)
        weaponManager.EquipWeapon(
            WeaponID.Spear);

    if (Keyboard.current.digit8Key.wasPressedThisFrame)
        weaponManager.EquipWeapon(
            WeaponID.ReturningShield);
}

private IEnumerator SwordDamageWindow(
    float duration)
{
    swordHitbox.EnableDamage();

    yield return new WaitForSeconds(
        duration);

    swordHitbox.DisableDamage();
}

private void UpdateBow()
{
    Bow bow =
        weaponManager.currentRangedWeapon
        as Bow;

    if (bow == null)
        return;

    if (!bow.isDrawing)
        return;

    if (!bow.bowReady &&
        Time.time - bow.drawStartTime >= bow.drawTime)
    {
        bow.bowReady = true;

        Debug.Log("Bow Ready");
    }
}
private IEnumerator ReloadGun(
    Gun gun)
{
    gun.isReloading = true;

    Debug.Log("Reloading...");

    yield return new WaitForSeconds(
        gun.reloadTime);

    gun.currentAmmo =
        gun.magazineSize;

    gun.isReloading = false;

    Debug.Log("Reload Complete");
}
private void UpdateGun()
{
    Gun gun =
        weaponManager.currentRangedWeapon
        as Gun;

    if (gun == null)
        return;

    if (!gunTriggerHeld)
        return;

    if (gun.isReloading)
        return;

    if (gun.currentAmmo <= 0)
        return;

    if (Time.time < gun.nextShotTime)
        return;

    gun.nextShotTime =
        Time.time + gun.fireRate;

    gun.currentAmmo--;

    GameObject bullet =
        Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation);

    Rigidbody2D rb =
        bullet.GetComponent<Rigidbody2D>();

    rb.linearVelocity =
        -firePoint.up *
        gun.projectileSpeed;

    Debug.Log(
        "Ammo: " +
        gun.currentAmmo +
        "/" +
        gun.magazineSize);
}
}