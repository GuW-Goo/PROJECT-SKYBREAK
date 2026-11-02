using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy_Bullet : MonoBehaviour
{
    public Enemy_Move_Speed moveSpeed;
    private Vector3 playerTransform;
    private Vector3 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
            transform.position += direction * moveSpeed.move_Speed * Time.deltaTime;
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
}
