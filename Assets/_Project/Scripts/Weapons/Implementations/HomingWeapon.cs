using UnityEngine;

public class HomingWeapon : BaseWeapon
{
    protected override void Fire()
    {
        SpawnHomingProjectile(
            firePoint.position,
            firePoint.forward,
            runtimeStats.AttackPower,
            runtimeStats.DetectionRange
        );
    }
}
