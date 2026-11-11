using UnityEngine;
using System.Collections.Generic;

public class ChargeProjectile : BaseProjectile
{
    private float splashRadius;
    private bool canSplash;
    private bool hasImpacted;

    public void Initialize(int damage, float moveSpeed, float lifeTime, float splashRadius, bool canSplash, GameObject explosionEffect)
    {
        base.Initialize(
        damage,
        moveSpeed,
        lifeTime,
        explosionEffect);

        this.splashRadius = splashRadius;
        this.canSplash = canSplash;

        hasImpacted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasImpacted)
            return;

        if (other.TryGetComponent<IDamageable>(out var target))
        {
            hasImpacted = true;
            GameObject effect = Instantiate(explosionEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

            if (canSplash)
            {
                effect.transform.localScale *= splashRadius;
                Explode();
            }
            else
            {
                target.TakeDamage(damage);
            }

            poolManager.ReturnProjectile(this);
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
