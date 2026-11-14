using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private PlayerBomb playerBomb;
    private WeaponManager weaponManager;

    public event Action OnWeaponItemCollected;
    
    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerBomb = GetComponent<PlayerBomb>();
        weaponManager = GetComponentInChildren<WeaponManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            playerHealth.TakeDamage(1);
        }
        else if(collision.gameObject.CompareTag("BombItem"))
        {
            playerBomb.TakeBomb();
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("WeaponItem"))
        {
            OnWeaponItemCollected.Invoke();
            Destroy(collision.gameObject);
        }
    }
}
