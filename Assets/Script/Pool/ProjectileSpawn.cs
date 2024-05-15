using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class ProjectileSpawn : MonoBehaviour
{
    private PoolManager poolManager;

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();
    }

    public void SpawnProjectile(string name, float damae, float scale)
    {
        Projectile projectile = poolManager.GetFromPool<Projectile>(name);
        projectile.DAMAGE = damae;
        projectile.SCALE = scale;
    }

    public void ButtervelyProjectile(string name, float damae, float scale, Vector3 vec)
    {
        Projectile projectile = poolManager.GetFromPool<Projectile>(name);
        projectile.DAMAGE = damae;
        projectile.SCALE = scale;
        //projectile.VEC = vec += new Vector3(0f, 1f, 0f);
        projectile.transform.position = vec += new Vector3(0f, 1.5f, 0f);
        Debug.Log(vec);
        projectile.transform.parent = null;
    }

    public void ReturnProjectile(Projectile projectile)
    {
        poolManager.TakeToPool<Projectile>(projectile.POOLNAME, projectile);
    }

}
