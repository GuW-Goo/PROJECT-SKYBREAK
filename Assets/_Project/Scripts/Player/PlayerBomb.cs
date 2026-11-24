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
            Debug.Log("폭탄을 갖고있지 않습니다.");
            return;
        }

        currentBomb--;

        // 모든 적 탄막 제거
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Enemy_Bullet");
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }

        // 모든 적 데미지
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
            Debug.Log("폭탄이 최대입니다.");
            return;
        }
        
        currentBomb++;

        Debug.Log($"TakeBomb, currentBomb : {currentBomb}");
    }
}
