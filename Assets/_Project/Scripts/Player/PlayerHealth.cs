using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerDataSO playerData;

    private int maxHealth;
    private int currentHealth;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    private void Awake()
    {
        maxHealth = playerData.MaxHealth;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log($"PlayerHealth.TakeDamage(), currentHealth = {currentHealth}");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("PlayerHealth.Die()");
    }
}
