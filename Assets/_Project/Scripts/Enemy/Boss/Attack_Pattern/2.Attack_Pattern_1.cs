using UnityEngine;

public class Attack_Pattern_1 : Attack_Pattern
{
    // 직선 탄막 1개씩 발사
    public override void Attack()
    {
        Debug.Log("1");
        Instantiate(controller.GetBullet(0), controller.GetSShoter(0).position, Quaternion.identity);
        Instantiate(controller.GetBullet(0), controller.GetSShoter(1).position, Quaternion.identity);
    }
}
