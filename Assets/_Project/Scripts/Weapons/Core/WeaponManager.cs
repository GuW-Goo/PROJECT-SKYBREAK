using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] Transform firePoint;
    [SerializeField] ProjectilePoolManager projectilePoolManager;
    [SerializeField] WeaponRuntimeData runtimeData;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] WeaponDataSO defaultWeaponData;

    private BaseWeapon currentWeapon;
    private WeaponDataSO currentWeaponData;

    public WeaponType CurrentWeaponType => currentWeaponData.Type;

    public void OnFirePressed()
    {
        currentWeapon?.OnFirePressed();
    }

    public void OnFireHeld()
    {
        currentWeapon?.OnFireHeld();
    }

    public void OnFireReleased()
    {
        currentWeapon?.OnFireReleased();
    }

    // 기존 무기의 사용을 종료하고 새 무기로 교체
    public void ChangeWeapon(WeaponDataSO data)
    {
        if (data == null)
            return;

        // 장착 무기가 비었는지 확인
        if (currentWeapon != null)
        {
            currentWeapon.OnUnequip();
            currentWeapon.enabled = false;

            Destroy(currentWeapon);
            currentWeapon = null;
        }

        // 무기 컴포넌트 장착
        switch(data.Type)
        {
            case WeaponType.Straight:
                currentWeapon = gameObject.AddComponent<StraightWeapon>();
                break;

            case WeaponType.Spread:
                currentWeapon = gameObject.AddComponent<SpreadWeapon>();
                break;

            case WeaponType.Charge:
                currentWeapon = gameObject.AddComponent<ChargeWeapon>();
                break;
            
            case WeaponType.Homing:
                currentWeapon = gameObject.AddComponent<HomingWeapon>();
                break;

            case WeaponType.Laser:
                currentWeapon = gameObject.AddComponent<LaserWeapon>();
                break;
        }

        if (currentWeapon == null)
            return;

        currentWeapon.Initialize(
            data,
            firePoint,
            projectilePoolManager,
            runtimeData.GetWeaponLevel(data.Type)
        );

        currentWeaponData = data;
        RefreshWeaponStats();
    }

    public void LevelUpWeapon(WeaponType weaponType)
    {
        runtimeData.LevelUP(weaponType);
    }

    private void Start()
    {
        ChangeWeapon(defaultWeaponData);
    }

    // 무기 레벨 및 플레이어 능력치 변경을 감지하기 위한 이벤트 구독
    private void OnEnable()
    {
        runtimeData.OnWeaponLevelChanged += UpdateWeaponLevel;
        playerStats.OnStatsChanged += UpdatePlayerStats;
    }

    // 비활성화 시 이벤트 구독 해제
    private void OnDisable()
    {
        runtimeData.OnWeaponLevelChanged -= UpdateWeaponLevel;
        playerStats.OnStatsChanged -= UpdatePlayerStats;
    }

    // 무기 단계 변경 이벤트 발생시
    private void UpdateWeaponLevel(WeaponType weaponType, int level)
    {
        if(currentWeapon == null) 
            return;
        // 현재 장착중인 무기와 단계 변경한 무기가 같은지 확인
        if (currentWeaponData.Type != weaponType)
            return;

        currentWeapon?.SetLevel(level);
        RefreshWeaponStats();
    }

    // 무기 수치 변경 이벤트 발생시
    private void UpdatePlayerStats()
    {
        RefreshWeaponStats();
    }

    /// <summary>
    /// SO의 기본 능력치와 PlayerStats의 추가 능력치를 합산하여
    /// 현재 무기에 적용할 최종 능력치를 계산 후 새로운 능력치를 전달합니다.
    /// </summary>
    private void RefreshWeaponStats()
    {
        if (currentWeapon == null || currentWeaponData == null)
            return;

        WeaponRuntimeStats stats = new WeaponRuntimeStats();

        // 공통 능력치
        stats.AttackPower = currentWeaponData.AttackPower
            + (int)playerStats.CheckStat(StatType.AttackPower, WeaponType.None);
        stats.FireInterval = currentWeaponData.FireInterval
            - playerStats.CheckStat(StatType.FireInterval, WeaponType.None);
        stats.ProjectileSpeed = currentWeaponData.ProjectileSpeed
            + playerStats.CheckStat(StatType.ProjectileSpeed, WeaponType.None);

        // 무기별 능력치
        stats.AdditionalProjectiles = currentWeaponData.GetProjectileCount(runtimeData.GetWeaponLevel(currentWeaponData.Type))
            + (int)playerStats.CheckStat(StatType.AdditionalProjectiles, currentWeaponData.Type);

        stats.SpreadAngle = currentWeaponData.SpreadAngle
            + playerStats.CheckStat(StatType.SpreadAngle, currentWeaponData.Type);

        if (currentWeaponData.Type == WeaponType.Charge)
        {
            stats.ChargeSpeed = currentWeaponData.GetMaxChargeTime(
                runtimeData.GetWeaponLevel(currentWeaponData.Type))
                - playerStats.CheckStat(StatType.ChargeSpeed, currentWeaponData.Type
            );
            stats.ExplodeRange = currentWeaponData.GetSplashRadius(
                runtimeData.GetWeaponLevel(currentWeaponData.Type))
                + playerStats.CheckStat(StatType.ExplodeRange, currentWeaponData.Type
            );
        }

        stats.DetectionRange = currentWeaponData.DetectionRange
            + playerStats.CheckStat(StatType.DetectionRange, currentWeaponData.Type);

        stats.LaserWidth = currentWeaponData.LaserWidth
            + playerStats.CheckStat(StatType.LaserWidth, currentWeaponData.Type);

        currentWeapon.UpdateStats(stats);
    }
}
