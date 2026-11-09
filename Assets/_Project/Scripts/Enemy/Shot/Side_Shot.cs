using System;
using UnityEngine;

public class Side_Shot : MonoBehaviour
{
    [SerializeField] private Enemy_Move_Speed moveSpeed;//탄속
    [SerializeField] private GameObject explosion_effect;//폭발이펙트
    [SerializeField] private bool leftOrRight;
    int value;//bool값을 int형으로 변경하기 위한 변수
    private void Start()
    {
        value = leftOrRight ? 1 : -1; // 좌우를 1과 -1중에 결정됨
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(value*moveSpeed.move_Speed*Time.deltaTime, 0, -moveSpeed.move_Speed * 2 * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //폭발 이펙트 생성(이펙트는 종료되면 자동삭제됨)
        Instantiate(explosion_effect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        //오브젝트 삭제
        Destroy(gameObject);
    }
}
