using UnityEngine;

public class Projectile : BaseProjectile
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var target))
        {
            Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
            target.TakeDamage(damage);
            poolManager.ReturnProjectile(this);
        }
    }
}
