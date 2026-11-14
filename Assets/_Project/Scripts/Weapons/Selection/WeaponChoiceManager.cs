using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponChoiceManager : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] WeaponManager weaponManager;

    [Header("Player")]
    [SerializeField] PlayerCollision playerCollision;
    [SerializeField] PlayerStats playerStats;

    [Header("All SO list")]
    [SerializeField] List<WeaponDataSO> allWeaponDataSOList;
    [SerializeField] List<StatDataSO> allStatDataSOList;

    private List<ChoiceData> choices;

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
        List<WeaponDataSO> weapons = GetRandomWeaponChoices(3);
        List<StatDataSO> stats = GetRandomStatChoices(3);

        choices = new List<ChoiceData>();

        for (int i = 0; i < Mathf.Min(weapons.Count, stats.Count); i++)
        {
            choices.Add(new ChoiceData(weapons[i], stats[i]));
        }

        if (choices.Count == 0)
        {
            Debug.LogWarning("선택 가능한 무기가 없습니다.");
            return;
        }

        Time.timeScale = 0f;

        for (int i = 0; i < choices.Count; i++)
        {
            Debug.Log(
                $"{i + 1}번 선택지: {choices[i].WeaponData.WeaponName} + {choices[i].StatData.StatName}"
            );
        }

        isSelecting = true;
    }

    private void Update()
    {
        if (!isSelecting || Keyboard.current == null)
            return;

        if (Keyboard.current.digit1Key.wasPressedThisFrame) SelectChoice(0);
        if (Keyboard.current.digit2Key.wasPressedThisFrame) SelectChoice(1);
        if (Keyboard.current.digit3Key.wasPressedThisFrame) SelectChoice(2);
    }

    private void SelectChoice(int index)
    {
        if (index < 0 || index >= choices.Count)
            return;

        ChoiceData selected = choices[index];

        // 무기 교체 또는 강화
        if (weaponManager.CurrentWeaponType == selected.WeaponData.Type)
        {
            weaponManager.LevelUpWeapon(selected.WeaponData.Type);
        }
        else
        {
            weaponManager.ChangeWeapon(selected.WeaponData);
        }

        // 능력치 적용
        playerStats.ApplyStat(selected.StatData);

        WeaponType targetWeapon = selected.StatData.TargetWeapons.Length == 0
            ? WeaponType.None
            : selected.WeaponData.Type;

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

    private List<StatDataSO> GetRandomStatChoices(int count)
    {
        List<StatDataSO> candidates = new List<StatDataSO>(allStatDataSOList);
        List<StatDataSO> selected = new();

        count = Mathf.Max(0, count);

        for(int i = 0; i < count; i++)
        {
            if(candidates.Count == 0) break;

            int randomIndex = Random.Range(0, candidates.Count);
            selected.Add(candidates[randomIndex]);
            candidates.RemoveAt(randomIndex);
        }

        return selected;
    }


}
