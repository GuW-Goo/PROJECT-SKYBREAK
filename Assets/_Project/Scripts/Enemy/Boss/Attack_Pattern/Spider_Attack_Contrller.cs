using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider_Attack_Controller : MonoBehaviour
{
    // 발사 위치 리스트
    [Header("총구 위치")]
    [SerializeField] protected List<Transform> BShoter = new List<Transform>(); // 연속으로 탄막 발싸
    [SerializeField] protected List<Transform> SShoter = new List<Transform>(); // 단발 탄막 
    
    [Header("총알")]
    [SerializeField] protected List<GameObject> Bullet_List = new List<GameObject>(); // 탄막 Prefab

    // 상속받은 cs리스트
    private Attack_Pattern[] attack_PatternList;
    void Start()
    {
        //Components로 상속받은 모든 놈들 불러오기
        attack_PatternList = GetComponents<Attack_Pattern>();
        StartCoroutine(attack_Routine());
        foreach (Attack_Pattern pattern in attack_PatternList)
            pattern.SetController(this);
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
    // 랜덤 변수 뽑아서 자식의 Attack()호출하는 함수
    private void Pattern_Select()
    {
        Debug.Log(Random.Range(0,attack_PatternList.Length));
        attack_PatternList[Random.Range(0,attack_PatternList.Length)].Attack();
    }
    public GameObject GetBullet(int index)
    {
        return Bullet_List[index];
    }
    public Transform GetSShoter(int index)
    {
        return SShoter[index];
    }
    public Transform GetBShoter(int index)
    {
        return SShoter[index];
    }
}
