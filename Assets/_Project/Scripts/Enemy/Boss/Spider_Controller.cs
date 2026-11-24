using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Spider_Controller : Spider_Attack_Controller, IDamageable
{
    // Animator 변수 컨트롤하기 위해 필요
    [SerializeField] private Animator animator;
    // 총 체력
    [SerializeField] private float hp;

    void Start()
    {
        hp = 20000;
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            Dead();
    }

    private void Dead()
    {
        animator.SetBool("IsDead", true);
        StopAttack();
    }
}
