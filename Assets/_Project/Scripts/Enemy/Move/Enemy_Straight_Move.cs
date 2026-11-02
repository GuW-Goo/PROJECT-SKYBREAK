using UnityEngine;

public class Enemy_Straight_Move : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Enemy_Move_Speed speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0,speed.move_Speed * Time.deltaTime);
    }
}
