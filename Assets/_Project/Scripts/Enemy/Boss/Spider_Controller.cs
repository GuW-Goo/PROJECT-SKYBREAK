using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Spider_Controller : MonoBehaviour, IDamageable
{
    
    // Animator 변수 컨트롤하기 위해 필요
    [SerializeField] private Animator animator;
    // 총 체력
    private int hp;

    void Start()
    {
        hp = 20000;
    }

    void Update()
    {
        
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
    }
}
