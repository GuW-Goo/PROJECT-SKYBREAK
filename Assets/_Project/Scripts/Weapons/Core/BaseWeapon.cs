using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    private float nextFireTime;

    protected WeaponDataSO weaponData;
    protected Transform firePoint;
    protected ProjectilePoolManager projectilePool;
    protected int level;

    protected int finalAttackPower;
    protected float finalFireInterval;
    protected float finalProjectileSpeed;

    protected bool IsFireReady => Time.time >= nextFireTime;

    public void Initialize(
        WeaponDataSO data,
        Transform point,
        ProjectilePoolManager poolManager,
        int playerLevel)
    {
        weaponData = data;
        firePoint = point;
        projectilePool = poolManager;
        level = playerLevel;
    }

    public void UpdateStats(int attackPower, float fireInterval, float projectileSpeed)
    {
        finalAttackPower = attackPower;
        finalFireInterval = fireInterval;
        finalProjectileSpeed = projectileSpeed;
    }

    public virtual void OnFirePressed()
    {
    }

    public virtual void OnFireHeld()
    {
        TryFire();
    }

    public virtual void OnFireReleased()
    {
    }

    public virtual void OnUnequip()
    {
    }

    public void TryFire()
    {
        if (Time.time < nextFireTime)
            return;

        Fire();

        nextFireTime = Time.time + finalFireInterval;
    }

    public void SetLevel(int weaponLevel)
    {
        Debug.Log("무기를 강화합니다. 현재 레벨 : " + weaponLevel);
        level = weaponLevel;
    }

    protected void SpawnProjectile(Vector3 position, Vector3 direction)
    {
        BaseProjectile projectile = projectilePool.GetProjectile(weaponData.ProjectilePrefab);

        projectile.transform.position = position;
        projectile.transform.rotation = Quaternion.LookRotation(direction);

        projectile.Initialize(
            finalAttackPower,
            finalProjectileSpeed,
            weaponData.LifeTime,
            weaponData.HitEffectPrefab
        );

        projectile.Activate();
    }

    protected void SpawnChargeProjectile(
        Vector3 position,
        Vector3 direction,
        int damage,
        float projectileScale,
        float splashRadius,
        bool canSplash)
    {
        BaseProjectile projectile = projectilePool.GetProjectile(weaponData.ProjectilePrefab);

        ChargeProjectile chargeProjectile = (ChargeProjectile)projectile;

        chargeProjectile.transform.position = position;
        chargeProjectile.transform.rotation = Quaternion.LookRotation(direction);
        chargeProjectile.transform.localScale = Vector3.one * projectileScale;

        chargeProjectile.Initialize(
            damage,
            weaponData.ProjectileSpeed,
            weaponData.LifeTime,
            splashRadius,
            canSplash,
            weaponData.HitEffectPrefab
        );

        chargeProjectile.Activate();
    }

    // 무기별 공격 방식 구현
    protected abstract void Fire();
}
