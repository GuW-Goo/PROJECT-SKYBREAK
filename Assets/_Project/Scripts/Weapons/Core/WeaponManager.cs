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

        if (currentWeaponData != null &&
            currentWeaponData.Type == weaponType)
        {
            currentWeapon.SetLevel(runtimeData.GetWeaponLevel(weaponType));
        }
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
    }

    private void UpdatePlayerStats()
    {
        RefreshWeaponStats();
    }

    private void RefreshWeaponStats()
    {
        if (currentWeapon == null || currentWeaponData == null)
            return;

        int attackPower = currentWeaponData.AttackPower
            + (int)playerStats.CheckStat(
                StatType.AttackPower,
                WeaponType.None
            );

        float fireInterval = currentWeaponData.FireInterval
            - playerStats.CheckStat(
                StatType.FireInterval,
                WeaponType.None
            );

        float projectileSpeed = currentWeaponData.ProjectileSpeed
            + playerStats.CheckStat(
                StatType.ProjectileSpeed,
                WeaponType.None
            );

        currentWeapon.UpdateStats(attackPower, fireInterval, projectileSpeed);
    }
}
