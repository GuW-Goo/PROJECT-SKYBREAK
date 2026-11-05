using UnityEngine;

public class StraightWeapon : BaseWeapon
{
    [SerializeField] private float projectileSpacing = 0.5f;

    protected override void Fire()
    {
        Debug.Log("직선형 무기");

        int count = weaponData.GetProjectileCount(level);

        // 가장 왼쪽의 탄막 위치 계산
        float startOffset = -(count - 1) * projectileSpacing / 2.0f;

        // 가장 왼쪽부터 오른쪽으로 하나씩 탄막 소환
        for(int i = 0; i < count; i++)
        {
            float offset = startOffset + projectileSpacing * i;

            Vector3 spawnPosition =
                firePoint.position + firePoint.right * offset;

            SpawnProjectile(spawnPosition, firePoint.forward);
        }
    }

    
}
