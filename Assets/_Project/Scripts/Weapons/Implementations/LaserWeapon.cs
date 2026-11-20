using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class LaserWeapon : BaseWeapon
{
    private float laserRange = 200f;    // 직선으로 쏘아질 거리
    private float laserHeight = 2f;     // 레이저의 높이 (3D 기반이라 지정)
    private int canPiercingLevel;

    private LineRenderer laserLine;

    public override void Initialize(
        WeaponDataSO data,
        Transform firePoint,
        ProjectilePoolManager projectilePoolManager,
        int weaponLevel)
    {
        base.Initialize(data, firePoint, projectilePoolManager, weaponLevel);

        laserLine = firePoint.GetComponentInChildren<LineRenderer>(true);
        canPiercingLevel = weaponData.PiercingLevel;
    }

    protected override void Fire()
    {
        // 전체 사거리의 절반
        float halfRange = laserRange * 0.5f;

        // 상자 중심을 전방으로 이동
        Vector3 center = firePoint.position
            + firePoint.forward * halfRange;

        // 상자 크기의 절반
        Vector3 halfExtents = new Vector3(
            runtimeStats.LaserWidth * 0.5f,
            laserHeight * 0.5f,
            halfRange
        );

        Collider[] colliders = Physics.OverlapBox(
            center,
            halfExtents,
            firePoint.rotation
        );

        if(level >= canPiercingLevel)
        {
            AttackPiercing(colliders);
        }
        else
        {
            AttackNonPiercing(colliders);
        }
    }

    private void AttackNonPiercing(Collider[] colliders)
    {
        float closestDistance = float.MaxValue;
        IDamageable closestTarget = null;
        Transform closestTransform = null;

        foreach (Collider collider in colliders)
        {
            if (!collider.TryGetComponent<IDamageable>(out var target))
                continue;

            float distance = (
                collider.transform.position.z - firePoint.position.z
            ) * Mathf.Sign(firePoint.forward.z);

            if (distance < 0f)
                continue;

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target;
                closestTransform = collider.transform;
            }
        }

        if(closestTarget != null)
        {
            closestTarget.TakeDamage(runtimeStats.AttackPower);

            Vector3 endPosition = 
                new Vector3(firePoint.position.x, 0f, closestTransform.position.z);

            DrawLaser(firePoint.position, endPosition);
            Instantiate(
                weaponData.HitEffectPrefab,
                closestTransform.position,
                Quaternion.identity
            );
        }
        else
        {
            Vector3 endPosition = firePoint.position + firePoint.forward * laserRange;
            DrawLaser(firePoint.position, endPosition);
        }
    }

    private void AttackPiercing(Collider[] colliders)
    {
        HashSet<IDamageable> hitTargets = new HashSet<IDamageable>();

        foreach (Collider collider in colliders)
        {
            if (!collider.TryGetComponent<IDamageable>(out var target))
                continue;

            if(!hitTargets.Add(target))
                continue;

            target.TakeDamage(runtimeStats.AttackPower);
            Instantiate(
                weaponData.HitEffectPrefab,
                collider.transform.position,
                Quaternion.identity
            );
        }

        Vector3 endPosition = firePoint.position + firePoint.forward * laserRange;
        DrawLaser(firePoint.position, endPosition);
    }

    private void DrawLaser(Vector3 startPosition, Vector3 endPosition)
    {
        laserLine.SetPosition(0, startPosition);
        laserLine.SetPosition(1, endPosition);
        laserLine.startWidth = runtimeStats.LaserWidth;
        laserLine.endWidth = runtimeStats.LaserWidth;

        StartCoroutine(LaserDuration());
    }

    IEnumerator LaserDuration()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(weaponData.LaserDuration);
        laserLine.enabled = false;
    }
}
