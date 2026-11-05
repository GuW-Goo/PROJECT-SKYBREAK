using UnityEngine;

public class Enemy_Move_Pattern_1 : MonoBehaviour
{
    //이동속도
    public Enemy_Move_Speed speed;
    //꺽을 타이밍 재기위한 변수
    float turn_Time = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    //추가 할것
    //직선으로 가다가 대각선으로 꺽기
    // Update is called once per frame
    void Update()
    {
        turn_Time = Time.time;
        if(turn_Time<2.5)
        {
            //turn_Time += Time.deltaTime;
            Debug.Log("a");
            transform.position += new Vector3(speed.move_Speed * Time.deltaTime, 0, speed.move_Speed * Time.deltaTime);
        }
        else
        {
            //turn_Time -= Time.deltaTime;
            transform.position += new Vector3(-speed.move_Speed * Time.deltaTime, 0, speed.move_Speed * Time.deltaTime);
        }
    }
}
