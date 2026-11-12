using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    //적들 prefab리스트
    [SerializeField] private List<GameObject> enemy_List = new List<GameObject>();
    
    //패턴 리스트 확인하기 위한 변수
    private int pattern_List;
    
    //시간당 적들 생성하기위한 초 카운트
    private float spawn_Count;
    //랜덤 변수
    private int random_Number;
    // 적 패턴 조절하기위한 변수
    private int upgrade;
    //초기화
    void Start()
    {
        upgrade = 0;
        // 5초당 한번 생성 하도록 설정
        spawn_Count = 5;
        // 일반 초기화
        random_Number = 0;
        // 갯수확인 어차피 적 패턴을 다가지고 있을테니 0번째 오브젝트에서 갯수 확인
        pattern_List = enemy_List[0].GetComponents<Enemy_Move_Pattern>().Length;
        // 패턴 변수 증가 
        StartCoroutine(PatternUpgrade());
        // 생성 코루틴 시작
        StartCoroutine(SpawnRoutine());
        
    }
    // 패턴 증가
    private IEnumerator PatternUpgrade()
    {
        while (true)
        {
            yield return new WaitForSeconds(60);//60초 간격으로 출현적 종류 증가
            if (upgrade == pattern_List)
            {
                StopCoroutine(PatternUpgrade());//코루틴 멈추기
                break;//반복문 종료
            }
            upgrade++;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawn_Count);
            SpawnEnemy();
        }
    }
    
    //적 생성
    private void SpawnEnemy()
    {
        //생성
        GameObject spawnedEnemy = Instantiate(enemy_List[Random.Range(0,upgrade)]);
        //생성한 적들의 패턴 선택
        Enemy_Pattern_Controller patternController = spawnedEnemy.GetComponent<Enemy_Pattern_Controller>();
        patternController.selectPattern(Random.Range(0,upgrade));
    }
}
