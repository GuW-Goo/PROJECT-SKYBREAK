using UnityEngine;

public class Projectile : BaseProjectile
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var target))
        {
            Instantiate(explosionEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
