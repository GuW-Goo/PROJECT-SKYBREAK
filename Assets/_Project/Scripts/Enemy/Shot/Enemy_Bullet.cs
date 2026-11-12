using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy_Bullet : MonoBehaviour
{
    [SerializeField] private Enemy_Move_Speed moveSpeed;//탄속
    [SerializeField] private GameObject explosion_effect;//폭발이펙트
    private Vector3 playerTransform;//플레이어 위치 추적하기위한 변속
    private Vector3 direction;//날아가는 방향

    void Start()
    {
        findPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어 방향으로 날아가도록 설정
        if (playerTransform != null)
        {
            transform.position += direction * moveSpeed.move_Speed * 2 * Time.deltaTime;
        }
    }
    //플레이어 위치로 발사하기위해 플레이어 위치 찾기
    public void findPlayer()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform.position;
            direction = (playerTransform - transform.position).normalized;
        }
        else//플레이어 리스폰 중일때 3초 뒤에 실행하도록 하기
        {
            StartCoroutine(WaitAndExcute(3.0f));
        }
    }
    //3초후에 다시 플레이어 찾기
    IEnumerator WaitAndExcute(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        findPlayer();
    }
    private void OnCollisionEnter(Collision collision)
    {
        //폭발 이펙트 생성(이펙트는 종료되면 자동삭제됨)
        Instantiate(explosion_effect,new Vector3(transform.position.x,transform.position.y,transform.position.z),Quaternion.identity);
        //오브젝트 삭제
        Destroy(gameObject);
    }
}
