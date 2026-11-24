using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoolManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int intialPoolSize = 10;
    [SerializeField] private Transform projectileContainer;

    Dictionary<GameObject, Queue<BaseProjectile>> pools 
        = new Dictionary<GameObject, Queue<BaseProjectile>>();

    // 풀에서 탄막을 대여
    public BaseProjectile GetProjectile(GameObject projectilePrefab)
    {
        if (!pools.TryGetValue(projectilePrefab, out var pool))
        {
            pool = CreatePool(projectilePrefab);
        }

        if (pool.Count <= 0) 
        {
            pool.Enqueue(CreateProjectile(projectilePrefab));
        }

        return pool.Dequeue();
    }

    // 탄막을 풀에 반환
    public void ReturnProjectile(BaseProjectile projectile)
    {
        if(pools.TryGetValue(projectile.OriginalPrefab, out var pool))
        {
            projectile.gameObject.SetActive(false);
            pool.Enqueue(projectile);
        }
    }

    // 풀 생성
    private Queue<BaseProjectile> CreatePool(GameObject projectilePrefab)
    {
        Queue<BaseProjectile> newPool = new Queue<BaseProjectile>();
        pools.Add(projectilePrefab, newPool);

        for(int i = 0; i < intialPoolSize; i++)
        {
            newPool.Enqueue(CreateProjectile(projectilePrefab));
        }

        return newPool;
    }

    // 탄막 생성
    private BaseProjectile CreateProjectile(GameObject projectilePrefab)
    {
        BaseProjectile projectile;

        if (projectileContainer == null)
        {
            projectile = Instantiate(projectilePrefab).GetComponent<BaseProjectile>();
        }
        else
        {
            projectile = Instantiate(projectilePrefab, projectileContainer).GetComponent<BaseProjectile>();
        }

        projectile.SetOriginalPrefab(projectilePrefab);
        projectile.SetPoolManager(this);
        projectile.gameObject.SetActive(false);

        return projectile;
    }
}
