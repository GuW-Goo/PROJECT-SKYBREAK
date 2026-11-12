using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    //적들 prefab리스트
    [SerializeField] private List<GameObject> enemy_List = new List<GameObject>();
    [SerializeField] private List<Vector3> spawn_point_List = new List<Vector3>();
    //시간당 어떤 리스트를 증가시킬지 카운트 하기위한 변수
    private int pattern_count;
    //시간당 적들 생성하기위한 초 카운트
    private float spawn_Count;
    //랜덤 변수
    private int random_Number;

    
    void Start()
    {
        pattern_count = 0;
        spawn_Count = 0;
        random_Number = 0;
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            random_Number = Random.Range(0, pattern_count);
            yield return new WaitForSeconds(5);
            SpawnEnemy(random_Number);
        }
    }
    private void SpawnEnemy(int random)
    {
        GameObject spawnedEnemy = Instantiate(enemy_List[random], spawn_point_List[random], Quaternion.identity);
        Enemy_Pattern_Controller patternController = spawnedEnemy.GetComponent<Enemy_Pattern_Controller>();
        patternController.selectPattern(random);
    }
}
