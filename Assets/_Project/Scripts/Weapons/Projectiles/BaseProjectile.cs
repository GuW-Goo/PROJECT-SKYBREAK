using System.Collections;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    [Header("Margin")]
    [SerializeField] protected float margin;

    protected int damage;
    protected float moveSpeed;
    protected float lifeTime;
    protected bool isDestroyed;

    protected GameObject explosionEffect;
    protected Camera mainCamera;
    protected GameObject originalPrefab;
    protected ProjectilePoolManager poolManager;

    public void SetOriginalPrefab(GameObject prefab)
    {
        originalPrefab = prefab;
    }

    public void SetPoolManager(ProjectilePoolManager pool)
    {
        poolManager = pool;
    }

    public GameObject OriginalPrefab => originalPrefab;

    public virtual void Initialize(
        int damage,
        float moveSpeed,
        float lifeTime,
        GameObject explosionEffect)
    {
        this.damage = damage;
        this.moveSpeed = moveSpeed;
        this.lifeTime = lifeTime;
        this.explosionEffect = explosionEffect;
        mainCamera = Camera.main;
    }

    public void Activate()
    {
        isDestroyed = false;

        gameObject.SetActive(true);

        StartCoroutine(LifeTimeRoutine());
    }

    protected virtual void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // 화면 밖이면 탄막 삭제
        RemoveIfOutOfBounds();
    }

    protected void RemoveProjectile()
    {
        if (isDestroyed)
            return;

        isDestroyed = true;
        poolManager.ReturnProjectile(this);
    }

    protected void RemoveIfOutOfBounds()
    {
        Vector3 currentPosition = mainCamera.WorldToViewportPoint(transform.position);

        // 화면 안에 탄막이 존재하는지 확인
        if (currentPosition.x < -margin ||
            currentPosition.x > 1 + margin ||
            currentPosition.y < -margin ||
            currentPosition.y > 1 + margin ||
            currentPosition.z < 0)
        {
            RemoveProjectile();
        }
    }

    IEnumerator LifeTimeRoutine()
    {
        yield return new WaitForSeconds(lifeTime);

        RemoveProjectile();
    }
}
