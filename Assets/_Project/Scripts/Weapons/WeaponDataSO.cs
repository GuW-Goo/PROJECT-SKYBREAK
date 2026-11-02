using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataSO", menuName = "SKYBREAK/Weapon/Weapon Data")]
public class WeaponDataSO : ScriptableObject
{
    [Header("Weapon Info")]
    [SerializeField] private string weaponName;

    [Header("Combat")]
    [SerializeField] private int attackPower;
    [SerializeField] private float fireInterval;

    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;

    [Header("Spread")]
    [SerializeField] private int projectileCount = 1;
    [SerializeField] private float spreadAngle;

    [Header("Charge")]
    [SerializeField] private float minChargeTime;
    [SerializeField] private float maxChargeTime;
    [SerializeField] private int maxChargeAttackPower;

    [Header("Homing")]
    [SerializeField] private float homingTurnSpeed;
    [SerializeField] private float detectingRange;

    [Header("Laser")]
    [SerializeField] private float laserWidth;
    [SerializeField] private float laserDuration;

    // 공통
    public int AttackPower => attackPower;
    public float FireInterval => fireInterval;

    // 기본형
    public float ProjectileSpeed => projectileSpeed;
    public float ProjectileCount => projectileCount;

    // 방사형
    public float SpotAngle => spreadAngle;

    // 차지형
    public float MinChargeTime => minChargeTime;
    public float MaxChargeTime => maxChargeTime;
    public int MaxChargeAttackPower => maxChargeAttackPower;

    // 유도형
    public float HomingTurnSpeed => homingTurnSpeed;
    public float DetectingRange => detectingRange;

    // 레이저
    public float LaserWidth => laserWidth;
    public float LaserDuration => laserDuration;
}
