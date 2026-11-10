using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataSO", menuName = "SKYBREAK/Weapon/Weapon Data")]
public class WeaponDataSO : ScriptableObject
{
    [Header("Weapon Info")]
    [SerializeField] private string weaponName;
    [SerializeField] private WeaponType weaponType;

    [Header("Level")]
    [SerializeField] private int[] projectileCounts = { 1, 3, 5 };

    [Header("Combat")]
    [SerializeField] private int attackPower;
    [SerializeField] private float fireInterval;

    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private float projectileSpeed;

    [Header("Straight")]
    [SerializeField] private float projectileSpacing;

    [Header("Spread")]
    [SerializeField] private float spreadAngle;

    [Header("Charge")]
    [SerializeField] private float minChargeTime;
    [SerializeField] private float[] maxChargeTime = { 1.5f, 1.0f, 1.0f };

    [SerializeField] private int minChargeAttackPower;
    [SerializeField] private int maxChargeAttackPower;

    [SerializeField] private float minProjectileScale;
    [SerializeField] private float maxProjectileScale;

    [Header("ChargeSplash")]
    [SerializeField] private float[] splashRadiusByLevel = { 1.2f, 1.2f, 2.5f };

    [Header("Homing")]
    [SerializeField] private float homingTurnSpeed;
    [SerializeField] private float detectionRange;

    [Header("Laser")]
    [SerializeField] private float laserWidth;
    [SerializeField] private float laserDuration;

    // 공통
    public string WeaponName => weaponName;
    public WeaponType Type => weaponType;
    public GameObject ProjectilePrefab => projectilePrefab;
    public GameObject HitEffectPrefab => hitEffectPrefab;
    public int AttackPower => attackPower;
    public float FireInterval => fireInterval;

    // Level
    public int GetProjectileCount(int level)
    {
        int index = Mathf.Clamp(level - 1, 0, projectileCounts.Length - 1);

        return projectileCounts[index];
    }

    // 기본
    public float ProjectileSpeed => projectileSpeed;

    // 직선형
    public float ProjectileSpacing => projectileSpacing;

    // 방사형
    public float SpreadAngle => spreadAngle;

    // 차지형
    public float MinChargeTime => minChargeTime;
    public float GetMaxChargeTime(int level)
    {
        int index = Mathf.Clamp(level - 1, 0, maxChargeTime.Length - 1);
        return maxChargeTime[index];
    }
    public int MinChargeAttackPower => minChargeAttackPower;
    public int MaxChargeAttackPower => maxChargeAttackPower;
    public float MinProjectileScale => minProjectileScale;
    public float MaxProjectileScale => maxProjectileScale;

    public float GetSplashRadius(int level)
    {
        int index = Mathf.Clamp(level - 1, 0, splashRadiusByLevel.Length - 1);
        return splashRadiusByLevel[index];
    }

    // 유도형
    public float HomingTurnSpeed => homingTurnSpeed;
    public float DetectionRange => detectionRange;

    // 레이저
    public float LaserWidth => laserWidth;
    public float LaserDuration => laserDuration;
}
