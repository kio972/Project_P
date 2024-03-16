using JinWon;
using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YeongJun;
public enum Items
{
    gold,
    ston,
}

public class DropItem : MonoBehaviour, IPoolObject
{
    [SerializeField]
    private string poolName;
    public string POOLNAME
    {
        get => poolName;
    }

    private PoolTextSpawn spawn;

    private BattleSceneManager bsm;

    private void Init()
    {
        spawn = GameObject.Find("PoolTextSpawn").GetComponent<PoolTextSpawn>();
        bsm = GameObject.Find("BattleSceneManager").GetComponent<BattleSceneManager>();
    }

    [SerializeField]
    private Items item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.Inst.PlaySFX("Coin_Drop");
            switch (item)
            {
                case Items.gold:
                    {
                        bsm.ItemDrop(Items.gold);
                        break;
                    }
                case Items.ston:
                    {
                        bsm.ItemDrop(Items.ston);
                        break;
                    }
            }

            Return();
        }
    }

    public void OnCreatedInPool()
    {
        Init();
    }

    public void OnGettingFromPool()
    {
        
    }

    public void Return()
    {
        spawn.ReturnGoldItem(this);
    }
}
