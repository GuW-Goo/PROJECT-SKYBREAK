using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRuntimeData : MonoBehaviour
{
    [Header("Player Level")]
    [SerializeField] private int maxWeaponLevel = 3;

    [Header("Additional Stats")]
    [SerializeField] private int bonusAttackPower;
    [SerializeField] private float bonusFireRate;

    public int BonusAttackPower => bonusAttackPower;
    public float BonusFireRate => bonusFireRate;

    public event Action<WeaponType, int> OnWeaponLevelChanged;


    private Dictionary<WeaponType, int> weaponLevels = new Dictionary<WeaponType, int>();

    public int GetWeaponLevel(WeaponType weaponType)
    {
        return weaponLevels[weaponType];
    }

    public void LevelUP(WeaponType weaponType)
    {
        if (weaponLevels[weaponType] >= maxWeaponLevel)
            return;

        weaponLevels[weaponType]++;

        OnWeaponLevelChanged?.Invoke(weaponType, weaponLevels[weaponType]);

        foreach(WeaponType type in Enum.GetValues(typeof(WeaponType)))
        {
            Debug.Log($"{type} : lv.{weaponLevels[type]}");
        }
    }

    private void Awake()
    {
        CreateDictionary();
    }

    private void CreateDictionary()
    {
        foreach(WeaponType type in Enum.GetValues(typeof(WeaponType)))
        {
            weaponLevels.Add(type, 1);
        }
    }
}
