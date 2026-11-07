using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    private float nextFireTime;

    protected WeaponDataSO weaponData;
    protected Transform firePoint;
    protected int level;

    protected bool IsFireReady => Time.time >= nextFireTime;

    public void Initialize(WeaponDataSO data, Transform point)
    {
        weaponData = data;
        firePoint = point;
        level = 1;
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

    public void TryFire()
    {
        if (Time.time < nextFireTime)
            return;

        Fire();

        nextFireTime = Time.time + weaponData.FireInterval;
    }

    protected void SpawnProjectile(Vector3 position, Vector3 direction)
    {
        GameObject projectileObject = Instantiate(
            weaponData.ProjectilePrefab,
            position,
            Quaternion.LookRotation(direction)
        );

        Projectile projectile = projectileObject.GetComponent<Projectile>();

        projectile.Initialize(
            weaponData.AttackPower,
            weaponData.ProjectileSpeed
        );
    }

    protected void SpawnChargeProjectile(
        Vector3 position,
        Vector3 direction,
        int damage,
        float projectileScale,
        float splashRadius,
        bool canSplash)
    {
        GameObject projectileObject = Instantiate(
            weaponData.ProjectilePrefab,
            position,
            Quaternion.LookRotation(direction)
        );

        projectileObject.transform.localScale =
            Vector3.one * projectileScale;

        ChargeProjectile projectile =
            projectileObject.GetComponent<ChargeProjectile>();

        projectile.Initialize(
            damage,
            weaponData.ProjectileSpeed,
            splashRadius,
            canSplash
        );
    }

    // 무기별 공격 방식 구현
    protected abstract void Fire();
}
