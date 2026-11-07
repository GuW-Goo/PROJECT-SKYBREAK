using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    private float nextFireTime;

    protected WeaponDataSO weaponData;
    protected Transform firePoint;
    protected int level;

    public void TryFire()
    {
        if (Time.time < nextFireTime)
            return;

        Fire();

        nextFireTime = Time.time + weaponData.FireInterval;
    }

    public void Initialize(WeaponDataSO data, Transform point)
    {
        weaponData = data;
        firePoint = point;
        level = 1;
    }

    protected void SpawnProjectile(Vector3 position, Vector3 direction)
    {
        GameObject projectileObject = Instantiate(
        weaponData.ProjectilePrefab,
        position,
        Quaternion.LookRotation(direction)
        );

        Projectile projectile =
            projectileObject.GetComponent<Projectile>();

        projectile.Initialize(
            weaponData.AttackPower,
            weaponData.ProjectileSpeed
        );
    }

    // 무기별 공격 방식 구현
    protected abstract void Fire();
}
