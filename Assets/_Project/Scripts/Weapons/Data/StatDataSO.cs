using UnityEngine;

[CreateAssetMenu(fileName = "NewStatData", menuName = "SKYBREAK/Game/Stat Data")]
public class StatDataSO : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string statName;
    [SerializeField, TextArea] private string description;

    [Header("Stat Info")]
    [SerializeField] private StatType statType;
    [SerializeField] private float increaseValue;

    [Header("Target Weapon")]
    [SerializeField] private WeaponType[] targetWeapons;

    public string StatName => statName;
    public string Description => description;
    public StatType StatType => statType;
    public float IncreaseValue => increaseValue;
    public WeaponType[] TargetWeapons => targetWeapons;
}
