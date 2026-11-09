using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponChoiceManager : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] WeaponManager weaponManager;

    [Header("Player")]
    [SerializeField] PlayerCollision playerCollision;

    [Header("All weapon SO list")]
    [SerializeField] List<WeaponDataSO> allWeaponDataSOList;

    private List<WeaponDataSO> choices;

    private bool isSelecting = false;

    private void OnEnable()
    {
        playerCollision.OnWeaponItemCollected += HandleWeaponItemCollected;
    }

    private void OnDisable()
    {
        playerCollision.OnWeaponItemCollected -= HandleWeaponItemCollected;
    }

    private void HandleWeaponItemCollected()
    {
        choices = GetRandomWeaponChoices(3);

        if (choices.Count == 0)
        {
            Debug.LogWarning("선택 가능한 무기가 없습니다.");
            return;
        }

        Time.timeScale = 0f;

        for(int i = 0; i < choices.Count; i++)
        {
            Debug.Log($"{i + 1} 번째 무기: {choices[i].WeaponName}");
        }

        isSelecting = true;
    }

    private void Update()
    {
        if (!isSelecting || Keyboard.current == null)
            return;

        if (Keyboard.current.digit1Key.wasPressedThisFrame) SelectWeapon(0);
        if (Keyboard.current.digit2Key.wasPressedThisFrame) SelectWeapon(1);
        if (Keyboard.current.digit3Key.wasPressedThisFrame) SelectWeapon(2);
    }

    private void SelectWeapon(int index)
    {
        if (index < 0 || index >= choices.Count)
            return;

        WeaponDataSO selectedWeapon = choices[index];

        Debug.Log($"선택한 무기 : {selectedWeapon.WeaponName}");

        weaponManager.ChangeWeapon(selectedWeapon);

        isSelecting = false;

        Time.timeScale = 1f;
    }

    /// <summary>
    /// 무기의 모든 종류 중 int count (양수) 만큼 뽑아서 return 합니다.
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    private List<WeaponDataSO> GetRandomWeaponChoices(int count)
    {
        List<WeaponDataSO> candidates = new List<WeaponDataSO>(allWeaponDataSOList);
        List<WeaponDataSO> selected = new();

        count = Mathf.Max(0, count);

        for (int i = 0; i < count; i++)
        {
            if (candidates.Count == 0) break;

            int randomIndex = Random.Range(0, candidates.Count);
            selected.Add(candidates[randomIndex]);
            candidates.RemoveAt(randomIndex);
        }

        return selected;
    }
}
