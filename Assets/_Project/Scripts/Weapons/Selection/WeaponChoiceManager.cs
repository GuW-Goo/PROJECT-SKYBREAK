using System.Collections.Generic;
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

    [Header("Choice UI")]
    [SerializeField] private GameObject UIcanvas;
    [SerializeField] private ChoiceUI[] choiceUIs;

    private List<ChoiceData> choices;

    private bool isSelecting = false;

    private void Awake()
    {
        for(int i = 0; i < choiceUIs.Length; i++)
        {
            choiceUIs[i].Initialize(i);
            choiceUIs[i].OnChoiceSelected += SelectChoice;
        }

        UIcanvas.SetActive(false);
    }

    private void OnEnable()
    {
        playerCollision.OnWeaponItemCollected += HandleWeaponItemCollected;
    }

    private void OnDisable()
    {
        playerCollision.OnWeaponItemCollected -= HandleWeaponItemCollected;
    }

    private void OnDestroy()
    {
        foreach(ChoiceUI choiceUI in choiceUIs)
        {
            choiceUI.OnChoiceSelected -= SelectChoice;
        }
    }

    private void HandleWeaponItemCollected()
    {
        List<WeaponDataSO> weapons = GetRandomWeaponChoices(3);
        List<StatDataSO> stats = GetRandomStatChoices(3);

        choices = new List<ChoiceData>();

        // 무기와 능력치 선택지 결합
        for (int i = 0; i < Mathf.Min(weapons.Count, stats.Count); i++)
        {
            choices.Add(new ChoiceData(weapons[i], stats[i]));
        }

        if (choices.Count == 0)
        {
            Debug.LogWarning("선택 가능한 무기가 없습니다.");
            return;
        }

        UpdateChoiceUI();

        UIcanvas.SetActive(true);

        GameManager.Instance.ChoosingWeapon();
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
        if (!isSelecting) return;

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

        UIcanvas.SetActive(false);

        isSelecting = false;
        GameManager.Instance.GameResume();
    }

    private void UpdateChoiceUI()
    {
        for(int i = 0; i < choices.Count; i++)
        {
            ChoiceData choice = choices[i];

            int currentLevel = weaponManager.GetWeaponLevel(choice.WeaponData.Type);

            string levelText;

            if (weaponManager.CurrentWeaponType == choice.WeaponData.Type)
            {
                levelText = $"lv.{currentLevel} → lv.{currentLevel + 1}";
            }
            else
            {
                levelText = $"lv.{currentLevel}";
            }

            choiceUIs[i].SetChoiceUI(
                choice.WeaponData.WeaponName,
                levelText,
                choice.StatData.StatName,
                $"+{choice.StatData.IncreaseValue:0.##}"
            );
        }
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

    /// <summary>
    /// 스탯의 모든 종류 중 int count (양수) 만큼 뽑아서 return 합니다.
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
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
