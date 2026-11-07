using UnityEngine;

public class Enemy_Move_Pattern2 : MonoBehaviour
{
    [SerializeField] private Enemy_Move_Speed speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.rotation = Quaternion.Euler(0,135,0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed.move_Speed * Time.deltaTime, 0, -(speed.move_Speed * Time.deltaTime));
    }
}
