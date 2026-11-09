using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private PlayerBomb playerBomb;


    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            weaponManager.OnFirePressed();
        }

        if (Keyboard.current.spaceKey.isPressed)
        {
            weaponManager.OnFireHeld();
        }

        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            weaponManager.OnFireReleased();
        }

        if (Keyboard.current.leftCtrlKey.wasPressedThisFrame)
        {
            playerBomb.UseBomb();
        }
    }
}
