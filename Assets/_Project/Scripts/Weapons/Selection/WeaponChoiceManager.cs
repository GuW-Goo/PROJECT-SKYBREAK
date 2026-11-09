using System.Collections.Generic;
using UnityEngine;

public class WeaponChoiceManager : MonoBehaviour
{
    [Header("All weapon SO list")]
    [SerializeField] List<WeaponDataSO> allWeaponDataSOList;

    /// <summary>
    /// 무기의 모든 종류 중 int count (양수) 만큼 뽑아서 return 합니다.
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public List<WeaponDataSO> GetRandomWeaponChoices(int count)
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
