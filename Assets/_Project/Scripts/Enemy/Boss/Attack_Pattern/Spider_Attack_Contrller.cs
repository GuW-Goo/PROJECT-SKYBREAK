using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spider_Attack_Controller : MonoBehaviour
{
    // 발사 위치 리스트
    [Header("총구 위치")]
    [SerializeField] protected List<Transform> BShoter = new List<Transform>(); // 연속으로 탄막 발싸
    [SerializeField] protected List<Transform> SShoter = new List<Transform>(); // 단발 탄막 
    
    [Header("총알")]
    [SerializeField] protected List<GameObject> Bullet_List = new List<GameObject>(); // 탄막 Prefab

    [Header("패턴 쿨타임")]
    [SerializeField] float[] pattern_CoolTime;

    // 다음 패턴 공격 시간
    float[] next_AttackTime;

    // 사용가능 패턴 List
    List<int> available_PatternList = new List<int>();

    // 상속받은 cs리스트
    private Attack_Pattern[] attack_PatternList;

    void Start()
    {
        // Components로 상속받은 모든 놈들 불러오기
        attack_PatternList = GetComponents<Attack_Pattern>();

        // 쿨타임 배열 초기화
        next_AttackTime = new float[attack_PatternList.Length];

        // Attack_Pattern에 Controller패치하기
        foreach (Attack_Pattern pattern in attack_PatternList)
        {
            pattern.SetController(this);
        }
        // 공격 시작
        StartCoroutine(attack_Routine());
    }

    // 3초마다 공격하도록 설정
    private IEnumerator attack_Routine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            Pattern_Select();
        }
    }
    // 시간마다 패턴 선택
    public void Pattern_Select()
    {
        //일단 현재 사용가능 패턴 초기화
        available_PatternList.Clear();

        // 사용 가능한 패턴 Index 수집
        for (int i = 0; i < attack_PatternList.Length; i++)
        {
            if (Time.time >= next_AttackTime[i])
            {
                // 시간에 맞으면 사용가능 패턴 리스트에 추가
                available_PatternList.Add(i);
            }
        }

        // 사용 가능한 패턴이 없는 경우
        if (available_PatternList.Count == 0)
        {
            Debug.Log("사용 가능한 패턴 없음");
            return;
        }

        // 사용 가능한 Index 중 랜덤 선택
        int randomIndex = Random.Range(
            0, available_PatternList.Count
        );

        int selectedIndex = available_PatternList[randomIndex];

        // 패턴 실행
        attack_PatternList[selectedIndex].Attack();

        // 해당 패턴 쿨타임 적용
        next_AttackTime[selectedIndex] =
            Time.time + pattern_CoolTime[selectedIndex];
    }
    
    //Pattern_Controller에 몇번째 불릿인지 전달
    public GameObject GetBullet(int index)
    {
        return Bullet_List[index];
    }
    //Pattern_Controller에 발사점 전달
    public Transform GetSShoter(int index)
    {
        return SShoter[index];
    }
    public Transform GetBShoter(int index)
    {
        return SShoter[index];
    }
    // 죽었을때 공격패턴 정지
    public void StopAttack()
    {
        StopCoroutine(attack_Routine());
    }
}
