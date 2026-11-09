using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponChoiceTest : MonoBehaviour
{
    [SerializeField] WeaponManager weaponManager;
    [SerializeField] WeaponChoiceManager weaponChoiceManager;

    private List<WeaponDataSO> choices;

    private void Update()
    {
        if (Keyboard.current.deleteKey.wasPressedThisFrame)
        {
            choices = weaponChoiceManager.GetRandomWeaponChoices(3);

            for (int i = 0; i < choices.Count; i++)
            {
                Debug.Log($"{i + 1}번 선택지 : {choices[i].WeaponName}");
            }
        }

        if (Keyboard.current.digit1Key.wasPressedThisFrame) SelectWeapon(0);
        if (Keyboard.current.digit2Key.wasPressedThisFrame) SelectWeapon(1);
        if (Keyboard.current.digit3Key.wasPressedThisFrame) SelectWeapon(2);
    }

    public void SelectWeapon(int index)
    {
        if(index < 0 || index >= choices.Count)
            return;

        WeaponDataSO selectedWeapon = choices[index];

        Debug.Log($"선택한 무기 : {selectedWeapon.WeaponName}");

        weaponManager.ChangeWeapon(selectedWeapon);
    }
    
}
