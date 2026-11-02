using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy_Shot : MonoBehaviour
{
    public GameObject shoter;
    public GameObject bullet;

    private Transform playerTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //발사 테스트
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            fire();
        }
        
    }
    
    //투사체 발사
    public void fire()
    {
        Instantiate(bullet, new Vector3(shoter.transform.position.x, shoter.transform.position.y,shoter.transform.position.z),Quaternion.identity);
    }
}
