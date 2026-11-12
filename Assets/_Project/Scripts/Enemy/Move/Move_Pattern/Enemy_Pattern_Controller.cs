using UnityEngine;

public class Enemy_Pattern_Controller : MonoBehaviour
{
    [SerializeField] GameObject enemy; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //패턴 추가될떄 
    public void selectPattern(int number)
    {
        if (number == 0)
        {
            enemy.GetComponent<Enemy_Move_Pattern_1>().enabled = true;
            enemy.GetComponent<Enemy_Move_Pattern_2>().enabled = false;
            enemy.GetComponent<Enemy_Move_Pattern_3>().enabled = false;
        }
        else if (number == 1)
        {
            enemy.GetComponent<Enemy_Move_Pattern_1>().enabled = false;
            enemy.GetComponent<Enemy_Move_Pattern_2>().enabled = true;
            enemy.GetComponent<Enemy_Move_Pattern_3>().enabled = false;
        }
        else if (number == 2)
        {
            enemy.GetComponent<Enemy_Move_Pattern_1>().enabled = false;
            enemy.GetComponent<Enemy_Move_Pattern_2>().enabled = false;
            enemy.GetComponent<Enemy_Move_Pattern_3>().enabled = true;
        }

    }
}
