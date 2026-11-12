using System.Collections;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour, IDamageable
{
    [SerializeField] private Enemy_Shot enemy_shoter;

    protected Camera mainCamera;

    //체력
    public int hp = 30;
    //발사 딜레이 시간
    float attack_Timing;
    void Start()
    {
        //일단 3초 간격 발사하도록 설정
        attack_Timing = 3;
        //enemy_shot.cs 초기화
        enemy_shoter.GetComponent<Enemy_Shot>();
        StartCoroutine(attack_Routine());
    }
    private IEnumerator attack_Routine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attack_Timing);
            enemy_shoter.fire();
        }
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if(hp <= 0)
            Dead();
    }
    // 체력이 전부 달았을때 사라짐
    public void Dead()
    {
        //점수증가 시켜야함
        GameObject.Destroy(gameObject);
    }
    // 플레이어 접촉했을때 터지게 만듬
    // 접촉해서 사라졌을 경우 점수 증가하지 않음
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    // 화면 밖으로 나가면 사라지게 만듬
    // 이 경우도 점수 증가 하지 않음
    private void OnBecameInvisible()
    {
        // 카메라 화면에서 완전히 보이지 않게 되면 자동 삭제
        Destroy(gameObject);
    }
}
