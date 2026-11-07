using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Propeller : MonoBehaviour
{
    //앞날개 뒷날개 체크용 체크하면 윗날개
    public bool upDown;
    //회전 속도
    float speed = 1000;
    void Update()
    {
        if(upDown == false)
        {
            transform.Rotate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime)); 
        }
    }
}
