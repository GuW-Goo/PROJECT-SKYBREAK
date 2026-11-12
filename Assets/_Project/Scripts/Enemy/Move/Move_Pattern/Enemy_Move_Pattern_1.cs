using UnityEngine;

public class Enemy_Move_Pattern_1 : MonoBehaviour
{
    //이동속도
    [SerializeField] private Enemy_Move_Speed speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.rotation = Quaternion.Euler(0,180,0);
    }
    //추가 할것
    //직선으로 가다가 대각선으로 꺽기
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, -(speed.move_Speed * Time.deltaTime));
    }
}
