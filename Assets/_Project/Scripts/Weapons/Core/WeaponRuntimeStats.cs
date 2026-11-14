public struct WeaponRuntimeStats
{
    // 공통 능력치
    public int AttackPower;
    public float FireInterval;
    public float ProjectileSpeed;

    // 직선형 / 방사형
    public int AdditionalProjectiles;

    // 방사형
    public float SpreadAngle;

    // 차지형
    public float ChargeSpeed;
    public float ExplodeRange;

    // 유도형
    public float DetectionRange;

    // 레이저
    public float LaserWidth;
}