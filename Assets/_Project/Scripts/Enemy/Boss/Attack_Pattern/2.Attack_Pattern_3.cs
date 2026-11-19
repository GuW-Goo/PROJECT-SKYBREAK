using System.Collections;
using UnityEngine;

public class Attack_Pattern_3 : Attack_Pattern
{
    // 양쪽으로 산탄 3번씩 발사
    public override void Attack()
    {
        StartCoroutine(Attack_Routine());
    }
    private IEnumerator Attack_Routine()
    {
        for (int i = 0; i <= 2; i++)
        {
            Instantiate(controller.GetBullet(2), controller.GetBShoter(0).position, Quaternion.identity);
            Instantiate(controller.GetBullet(2), controller.GetBShoter(1).position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
