using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class PoolTextSpawn : MonoBehaviour
{
    private PoolManager poolManager;

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();
    }

    public void SpawnDamageText(string name, Vector2 pos, float damage)
    {
        DamageText damageText = poolManager.GetFromPool<DamageText>(name);
        damageText.DAMAGE = damage;
        pos += new Vector2(0f, 0.4f);
        damageText.transform.position = pos;
    }
    public void ReturnDamageText(DamageText text)
    {
        poolManager.TakeToPool<DamageText>(text.POOLNAME, text);
    }

    public void SpawnMarkText(string name, Vector2 pos)
    {
        ExclamationMarkText damageText = poolManager.GetFromPool<ExclamationMarkText>(name);
        pos += new Vector2(0f, 0.4f);
        damageText.transform.position = pos;
    }
    public void ReturnMarkText(ExclamationMarkText text)
    {
        poolManager.TakeToPool<ExclamationMarkText>(text.POOLNAME, text);
    }

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
}
