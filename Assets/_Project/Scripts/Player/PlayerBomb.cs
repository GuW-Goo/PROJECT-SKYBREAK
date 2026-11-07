using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    [SerializeField] PlayerDataSO playerData;

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
            return;

        currentBomb--;

        // Todo: 추후 Enemy 제거 및 탄막 제거 로직 추가..
        //       폭탄 데미지 ( 화면에 존재하는 모든 Enemy를 Destroy할지, 일정 데미지를 줄지 )
    }

    public void TakeBomb()
    {
        if (currentBomb == maxBomb) 
            return;

        currentBomb++;
    }
}
