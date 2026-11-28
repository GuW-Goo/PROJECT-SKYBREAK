using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button choiceButton;
    [SerializeField] private TMP_Text weaponName;
    [SerializeField] private TMP_Text weaponLevel;
    [SerializeField] private TMP_Text statName;
    [SerializeField] private TMP_Text increaseStat;

    public event Action<int> OnChoiceSelected;

    private int index;

    private void Awake()
    {
        choiceButton.onClick.AddListener(() => OnChoiceSelected?.Invoke(index));
    }

    public void Initialize(int index)
    {
        this.index = index;
    }

    public void SetChoiceUI(
        string name,
        string level,
        string stat,
        string increase)
    {
        weaponName.text = name;
        weaponLevel.text = level;
        statName.text = stat;
        increaseStat.text = increase;
    }
}
