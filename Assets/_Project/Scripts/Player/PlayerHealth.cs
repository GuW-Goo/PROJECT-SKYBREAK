using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerDataSO playerData;
    [SerializeField] private Transform modelRoot;
    [SerializeField] private float invincibilityDuration = 1f;  // 무적시간
    [SerializeField] private float blickInterval = 0.1f;        // 깜빡이는 간격

    private int maxHealth;
    private int currentHealth;
    private bool isInvincible = false;
    private Renderer[] playerRenderers;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public event Action OnPlayerDied;

    private void Awake()
    {
        maxHealth = playerData.MaxHealth;
        currentHealth = maxHealth;
        playerRenderers = modelRoot.GetComponentsInChildren<Renderer>(true);
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        Debug.Log($"PlayerHealth.TakeDamage(), currentHealth = {currentHealth}");

        if(currentHealth <= 0)
        {
            Die();
        }

        isInvincible = true;
        StartCoroutine(Blink());
    }

    private void Die()
    {
        OnPlayerDied.Invoke();
    }

    // 무적시간동안 Player가 깜빡임
    IEnumerator Blink()
    {
        float elapsedTime = 0f;

        while(elapsedTime < invincibilityDuration)
        {
            foreach(Renderer renderer in playerRenderers)
            {
                renderer.enabled = !renderer.enabled;
            }

            yield return new WaitForSeconds(blickInterval);

            elapsedTime += blickInterval;
        }

        foreach (Renderer renderer in playerRenderers)
        {
            renderer.enabled = true;
        }

        // 다시 Damage를 받을 수 있도록 초기화
        isInvincible = false;
    }
}
