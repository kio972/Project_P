using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class PoolTextSpawn : MonoBehaviour
{
    private PoolManager poolManager;

    private Vector2 damageAlphaVec = new Vector3(0f, 1f);
    private Vector2 markAlphaVec = new Vector3(0f, 1.5f);

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();
    }

    // DamageText를 PoolManager에서 꺼내오고 Spawn위치 및 Damage를 지정해주는 함수! 
    public void SpawnDamageText(string name, Vector2 pos, float damage)
    {
        DamageText damageText = poolManager.GetFromPool<DamageText>(name);
        damageText.DAMAGE = damage;
<<<<<<< HEAD
        pos += damageAlphaVec;
=======
        pos += new Vector2(0f, 0.4f);
>>>>>>> Jun
        damageText.transform.position = pos;
    }
    // 사용된 DamageText를 꺼주는 함수! 
    public void ReturnDamageText(DamageText text)
    {
        poolManager.TakeToPool<DamageText>(text.POOLNAME, text);
    }
    // markText를 PoolManager에서 꺼내오고 Spawn위치를 지정해주는 함수!
    public void SpawnMarkText(string name, Vector2 pos)
    {
<<<<<<< HEAD
        ExclamationMarkText markText = poolManager.GetFromPool<ExclamationMarkText>(name);
        pos += markAlphaVec;
        markText.transform.position = pos;
=======
        ExclamationMarkText damageText = poolManager.GetFromPool<ExclamationMarkText>(name);
        pos += new Vector2(0f, 0.4f);
        damageText.transform.position = pos;
>>>>>>> Jun
    }
    // 사용된 markText를 꺼주는 함수! 
    public void ReturnMarkText(ExclamationMarkText text)
    {
        poolManager.TakeToPool<ExclamationMarkText>(text.POOLNAME, text);
    }
<<<<<<< HEAD
=======

    public void SpawnDropItem(string name, Vector2 pos)
    {
        DropItem dropItem = poolManager.GetFromPool<DropItem>(name);
        pos += new Vector2(0f, -1.5f);
        dropItem.transform.position = pos;
    }

    public void ReturnGoldItem(DropItem item)
    {
        poolManager.TakeToPool<DropItem>(item.POOLNAME, item);
    }
>>>>>>> Jun
}
