using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int damage;
    private float moveSpeed;

    public void Initialize(int damage, float moveSpeed)
    {
        this.damage = damage;
        this.moveSpeed = moveSpeed;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var target))
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
