using UnityEngine;
using System.Collections.Generic;

public class ChargeProjectile : MonoBehaviour
{
    private GameObject explosionEffect;

    private int damage;
    private float moveSpeed;
    private float splashRadius;
    private bool canSplash;

    private bool hasImpacted;

    public void Initialize(int damage, float moveSpeed, float splashRadius, bool canSplash, GameObject explosionEffect)
    {
        this.damage = damage;
        this.moveSpeed = moveSpeed; 
        this.splashRadius = splashRadius;
        this.canSplash = canSplash;
        this.explosionEffect = explosionEffect;
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
