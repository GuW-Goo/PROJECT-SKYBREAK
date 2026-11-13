using System;
using System.Collections.Generic;
using UnityEngine;

public struct StatKey
{
    public StatType StatType { get; }
    public WeaponType WeaponType { get; }

    public StatKey(StatType statType, WeaponType weaponType)
    {
        StatType = statType;
        WeaponType = weaponType;
    }
}

public class PlayerStats : MonoBehaviour
{

    private Dictionary<StatKey, float> playerStats = new Dictionary<StatKey, float>();

    public event Action OnStatsChanged;

    public void ApplyStat(StatDataSO data)
    {
        if (data.TargetWeapons.Length == 0)
        {
            AddStat(data.StatType, WeaponType.None, data.IncreaseValue);
            return;
        }

        foreach (WeaponType weaponType in data.TargetWeapons)
        {
            AddStat(data.StatType, weaponType, data.IncreaseValue);
        }
    }

    public float CheckStat(StatType statType, WeaponType weaponType)
    {
        StatKey key = new StatKey(statType, weaponType);

        if (!playerStats.TryGetValue(key, out float value))
            return 0f;

        return value;
    }

    private void AddStat(StatType statType, WeaponType weaponType, float stat)
    {
        StatKey key = new StatKey(statType, weaponType);

        if (!playerStats.TryGetValue(key, out float value))
        {
            playerStats.Add(key, stat);
        }
        else
        {
            playerStats[key] = value + stat;
        }

        OnStatsChanged.Invoke();
    }
}
