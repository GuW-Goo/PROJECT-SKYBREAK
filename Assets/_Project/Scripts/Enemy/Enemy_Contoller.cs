using System.Collections;
using UnityEngine;

public class Enemy_Contoller : MonoBehaviour, IDamageable
{
    [SerializeField] private Enemy_Shot enemy_shoter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //체력
    public int hp = 30;
    //발사 딜레이 시간
    float attack_Timing;
    void Start()
    {
        //일단 5초 간격 발사하도록 설정
        attack_Timing = 5;
        //enemy_shot.cs 초기화
        enemy_shoter.GetComponent<Enemy_Shot>();
        StartCoroutine(attack_Routine());
    }
    private IEnumerator attack_Routine()
    {
        while (true)
        {
            enemy_shoter.fire();
            yield return new WaitForSeconds(attack_Timing);
        }

    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if(hp <= 0)
            Dead();
    }
    public void Dead()
    {
        GameObject.Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
