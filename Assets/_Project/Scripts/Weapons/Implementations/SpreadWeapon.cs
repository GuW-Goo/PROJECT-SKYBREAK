using UnityEngine;

public class SpreadWeapon : BaseWeapon
{


    protected override void Fire()
    {
        int count = runtimeStats.AdditionalProjectiles;
        float totalAngle = runtimeStats.SpreadAngle;

        if(count == 1)
        {
            SpawnProjectile(firePoint.position, firePoint.forward);
            return;
        }

        // 첫번째 탄막 위치
        float startAngle = -(totalAngle / 2.0f);
        // 탄막 간 거리
        float angleStep = totalAngle / (count - 1);

        for(int i = 0; i < count; i++)
        {
            float angle = startAngle + (angleStep * i);

            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.up) * firePoint.forward;

            SpawnProjectile(firePoint.position, direction);
        }

    }
}
