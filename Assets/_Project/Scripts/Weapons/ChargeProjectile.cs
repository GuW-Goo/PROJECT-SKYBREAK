using UnityEngine;
using System.Collections.Generic;

public class ChargeProjectile : MonoBehaviour
{
    private int damage;
    private float moveSpeed;
    private float splashRadius;
    private bool canSplash;

    private bool hasImpacted;

    public void Initialize(int damage, float moveSpeed, float splashRadius, bool canSplash)
    {
        this.damage = damage;
        this.moveSpeed = moveSpeed; 
        this.splashRadius = splashRadius;
        this.canSplash = canSplash;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasImpacted)
            return;

        if (other.TryGetComponent<IDamageable>(out var target))
        {
            hasImpacted = true;

            if (canSplash)
            {
                Explode();
            }
            else
            {
                target.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, splashRadius);

        HashSet<IDamageable> damagedTargets = new HashSet<IDamageable>();

        foreach (Collider hitCollider in hitColliders)
        {
            IDamageable target = hitCollider.GetComponent<IDamageable>();

            if (target != null && damagedTargets.Add(target))
            { 
                target.TakeDamage(damage);
            }
        }
    }
}
