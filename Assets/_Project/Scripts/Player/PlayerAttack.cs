using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private BaseWeapon currentWeapon;
    [SerializeField] private WeaponDataSO weaponData;
    [SerializeField] private Transform firePoint;
    [SerializeField] private PlayerBomb playerBomb;

    private void Awake()
    {
        currentWeapon.Initialize(weaponData, firePoint);
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            currentWeapon.OnFirePressed();
        }

        if (Keyboard.current.spaceKey.isPressed)
        {
            currentWeapon.OnFireHeld();
        }

        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            currentWeapon.OnFireReleased();
        }

        if (Keyboard.current.leftCtrlKey.wasPressedThisFrame)
        {
            playerBomb.UseBomb();
        }
    }
}
