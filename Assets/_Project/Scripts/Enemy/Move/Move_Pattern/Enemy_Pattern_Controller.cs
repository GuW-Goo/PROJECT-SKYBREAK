using UnityEngine;

public class Enemy_Pattern_Controller : MonoBehaviour
{
    //추상 리스트로 패턴들 저장
    private Enemy_Move_Pattern[] patterns;
    private void Awake()
    {
        patterns = GetComponents<Enemy_Move_Pattern>();
    }
    public void selectPattern(int patternIndex)
    {
        //일단 모든 패턴 비활성화
        foreach(var pattern in patterns)
        {
            pattern.enabled = false;
        }
        //범위에 맞으면 해당 패턴 활성화
        if(patternIndex >= 0 && patternIndex < patterns.Length)
        {
            patterns[patternIndex].enabled = true;
        }
    }
}
