using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject explosionEffect;
    private int damage;
    private float moveSpeed;

    public void Initialize(int damage, float moveSpeed, GameObject explosionEffect)
    {
        this.damage = damage;
        this.moveSpeed = moveSpeed;
        this.explosionEffect = explosionEffect;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

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
