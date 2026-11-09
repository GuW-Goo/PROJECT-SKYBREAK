using UnityEngine;

public class Nomal_Shot : MonoBehaviour
{
    [SerializeField] private Enemy_Move_Speed moveSpeed;//탄속
    [SerializeField] private GameObject explosion_effect;//폭발이펙트

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, -moveSpeed.move_Speed * 2 * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //폭발 이펙트 생성(이펙트는 종료되면 자동삭제됨)
        Instantiate(explosion_effect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        //오브젝트 삭제
        Destroy(gameObject);
    }
}
