using UnityEngine;

public class Player_Test : MonoBehaviour, IDamageable
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(int damage)
    {
        Debug.Log("PlayerHit");
    }

}

