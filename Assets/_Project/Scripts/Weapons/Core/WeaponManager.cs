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

    public void ChangeWeapon(WeaponDataSO data)
    {
        if (data == null)
            return;

        // 아직 구현하지 않은 기능
        if (data.Type == WeaponType.Homing || data.Type == WeaponType.Laser)
            return;

        if (currentWeapon != null)
        {
            currentWeapon.OnUnequip();
            currentWeapon.enabled = false;

            Destroy(currentWeapon);
            currentWeapon = null;
        }

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
            /*
            case WeaponType.Homing:
                currentWeapon = gameObject.AddComponent<HomingWeapon>();
                break;

            case WeaponType.Laser:
                currentWeapon = gameObject.AddComponent<LaserWeapon>();
                break;
            */
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

    private void OnEnable()
    {
        runtimeData.OnWeaponLevelChanged += UpdateWeaponLevel;
        playerStats.OnStatsChanged += UpdatePlayerStats;
    }

    private void OnDisable()
    {
        runtimeData.OnWeaponLevelChanged -= UpdateWeaponLevel;
        playerStats.OnStatsChanged -= UpdatePlayerStats;
    }

    private void UpdateWeaponLevel(WeaponType weaponType, int level)
    {
        if(currentWeapon == null) 
            return;
        if (currentWeaponData.Type != weaponType)
            return;

        currentWeapon?.SetLevel(level);
        RefreshWeaponStats();
    }

    private void UpdatePlayerStats()
    {
        RefreshWeaponStats();
    }

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

        

        currentWeapon.UpdateStats(stats);
    }
}
