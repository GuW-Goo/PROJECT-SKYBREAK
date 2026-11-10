using System;
using UnityEngine;

public class WeaponRuntimeData : MonoBehaviour
{
    [Header("Player Level")]
    [SerializeField] private int maxPlayerLevel = 3;
    [SerializeField] private int playerLevel = 1;

    [Header("Additional Stats")]
    [SerializeField] private int bonusAttackPower;
    [SerializeField] private float bonusFireRate;

    public int PlayerLevel => playerLevel;
    public int BonusAttackPower => bonusAttackPower;
    public float BonusFireRate => bonusFireRate;

    public event Action<int> OnPlayerLevelChanged;

    public void LevelUP()
    {
        if (playerLevel >= maxPlayerLevel)
            return;

        playerLevel++;

        OnPlayerLevelChanged?.Invoke(playerLevel);
    }
}
