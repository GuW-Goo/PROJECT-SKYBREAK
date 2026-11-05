using UnityEngine;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private BaseWeapon currentWeapon;
    [SerializeField] private WeaponDataSO weaponData;
    [SerializeField] private Transform firePoint;

    private void Awake()
    {
        currentWeapon.Initialize(weaponData, firePoint);
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            currentWeapon.TryFire();
        }
    }
}
