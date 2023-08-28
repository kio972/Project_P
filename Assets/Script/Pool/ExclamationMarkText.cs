using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class ExclamationMarkText : MonoBehaviour, IPoolObject
{
    [SerializeField]
    private string poolName;
    public string POOLNAME
    {
        get => poolName;
    }

    private PoolTextSpawn spawn;

    private void Init()
    {
        spawn = GameObject.Find("PoolTextSpawn").GetComponent<PoolTextSpawn>();
    }

    /*private void Update()
    {
        transform.Translate(transform.up * Time.deltaTime * 2f);
    }*/

    public void OnCreatedInPool()
    {
        Init();
    }

    public void OnGettingFromPool()
    {
        Invoke("Return", 1f);
    }

    public void Return()
    {
        spawn.ReturnMarkText(this);
    }
}
