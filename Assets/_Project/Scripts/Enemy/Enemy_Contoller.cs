using UnityEngine;

public class Enemy_Contoller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int hp = 30;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void getDamage(int damage)
    {
        hp-=damage;
    }
    public void Dead()
    {
        GameObject.Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
           
        }
    }

}
