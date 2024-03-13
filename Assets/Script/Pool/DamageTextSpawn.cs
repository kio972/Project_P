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

    // DamageText�� PoolManager���� �������� Spawn��ġ �� Damage�� �������ִ� �Լ�! 
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
    // ���� DamageText�� ���ִ� �Լ�! 
    public void ReturnDamageText(DamageText text)
    {
        poolManager.TakeToPool<DamageText>(text.POOLNAME, text);
    }
    // markText�� PoolManager���� �������� Spawn��ġ�� �������ִ� �Լ�!
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
    // ���� markText�� ���ִ� �Լ�! 
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
