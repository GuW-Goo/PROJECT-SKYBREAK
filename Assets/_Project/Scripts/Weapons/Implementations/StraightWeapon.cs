using UnityEngine;

public class StraightWeapon : BaseWeapon
{
    protected override void Fire()
    {
        int count = weaponData.GetProjectileCount(level);

        // 가장 왼쪽의 탄막 위치 계산
        float startOffset = -(count - 1) * weaponData.ProjectileSpacing / 2.0f;

        // 가장 왼쪽부터 오른쪽으로 하나씩 탄막 소환
        for(int i = 0; i < count; i++)
        {
            float offset = startOffset + weaponData.ProjectileSpacing * i;

            Vector3 spawnPosition =
                firePoint.position + firePoint.right * offset;

            SpawnProjectile(spawnPosition, firePoint.forward);
        }
    }

    
}
