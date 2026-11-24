using UnityEngine;
using UnityEngine.InputSystem.XR;
using System.Collections;
public class Attack_Pattern_4 : Attack_Pattern
{
    [SerializeField] Animator Animator;
    
    // ³»·ÁÂï´Â °ø°Ý¿¡¼­ 10°³ÀÇ ·£´ý Åº¸· »Ñ¸®±â 
    public override void Attack()
    {
        Animator.SetBool("Jump", true);
        StartCoroutine(Attack_Routine());
    }

    private IEnumerator Attack_Routine()
    {
        yield return new WaitForSeconds(2.3f);
        for (int i = 0; i <= 6; i++)
        {
            Instantiate(controller.GetBullet(3), controller.transform.position, Quaternion.identity);
            Instantiate(controller.GetBullet(3), controller.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
        StopCoroutine(Attack_Routine());
    }
}
