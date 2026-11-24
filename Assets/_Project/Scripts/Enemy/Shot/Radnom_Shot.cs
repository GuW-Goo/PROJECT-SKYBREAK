using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class Random_Shot : MonoBehaviour
{
    [SerializeField] private Enemy_Move_Speed moveSpeed;//탄속
    [SerializeField] private GameObject explosion_effect;//폭발이펙트

    Rigidbody Rigidbody;
    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.AddForce(new Vector3(Random.Range(-42, 42) * moveSpeed.move_Speed *2 , 0, -moveSpeed.move_Speed *2* 100));
    }
   
    private void OnCollisionEnter(Collision collision)
    {
        //폭발 이펙트 생성(이펙트는 종료되면 자동삭제됨)
        Instantiate(explosion_effect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        //오브젝트 삭제
        Destroy(gameObject);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
