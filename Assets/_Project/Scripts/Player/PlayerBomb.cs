using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    [SerializeField] PlayerDataSO playerData;
    [SerializeField] int bombDamage = 50;

    private int maxBomb;
    private int currentBomb;

    public int MaxBomb => maxBomb;
    public int CurrentBomb => currentBomb;
    
    private void Awake()
    {
        maxBomb = playerData.MaxBombCount;
        currentBomb = maxBomb;
    }

    public void UseBomb()
    {
        if (currentBomb <= 0)
        {
            Debug.Log("∆¯≈∫¿ª ∞Æ∞Ì¿÷¡ˆ æ Ω¿¥œ¥Ÿ.");
            return;
        }

        currentBomb--;

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Enemy_Bullet");
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }

        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject target in targets)
        {
            if (target.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(bombDamage);
            }
        }

        Debug.Log($"UseBomb, currentBomb : {currentBomb}");
    }

    public void TakeBomb()
    {
        if (currentBomb == maxBomb)
        {
            Debug.Log("∆¯≈∫¿Ã √÷¥Î¿‘¥œ¥Ÿ.");
            return;
        }
        
        currentBomb++;

        Debug.Log($"TakeBomb, currentBomb : {currentBomb}");
    }
}
