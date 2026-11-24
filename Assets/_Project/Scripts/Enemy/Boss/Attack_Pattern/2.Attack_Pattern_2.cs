using UnityEngine;

public class Attack_Pattern_2 : Attack_Pattern
{
    // 양쪽으로 산탄 1번씩 발사
    public override void Attack()
    {
        Debug.Log("2");
        Instantiate(controller.GetBullet(2), controller.GetBShoter(0).position,Quaternion.identity);
        Instantiate(controller.GetBullet(2), controller.GetBShoter(1).position, Quaternion.identity);
    }
}
