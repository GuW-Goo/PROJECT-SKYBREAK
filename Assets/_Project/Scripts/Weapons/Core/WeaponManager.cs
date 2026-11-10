using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] Transform firePoint;
    [SerializeField] WeaponRuntimeData runtimeData;
    [SerializeField] WeaponDataSO defaultWeaponData;

    private BaseWeapon currentWeapon;
    private WeaponDataSO currentWeaponData;

    private void Awake()
    {
        ChangeWeapon(defaultWeaponData);
    }

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

        // 동일 무기 선택 시
        if(currentWeaponData == data)
        {
            currentWeapon.SetLevel(runtimeData.PlayerLevel);
            return;
        }

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

        currentWeapon.Initialize(data, firePoint, runtimeData.PlayerLevel);
        currentWeaponData = data;
    }

    private void OnEnable()
    {
        runtimeData.OnPlayerLevelChanged += UpdateWeaponLevel;

    }

    private void OnDisable()
    {
        runtimeData.OnPlayerLevelChanged -= UpdateWeaponLevel;
    }

    private void UpdateWeaponLevel(int level)
    {
        currentWeapon?.SetLevel(level);
    }
}
