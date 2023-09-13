using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Redcode.Pools;

public class DamageText : MonoBehaviour, IPoolObject
{
    [SerializeField]
    private string poolName;
    public string POOLNAME
    {
        get => poolName;
    }

    private float damage;
    public float DAMAGE
    {
        set => damage = value;
    }

    private TextMeshProUGUI text;
    private PoolTextSpawn spawn;

    private void Init()
    {
        text = GetComponent<TextMeshProUGUI>();
        spawn = GameObject.Find("PoolTextSpawn").GetComponent<PoolTextSpawn>();
    }

    private void Update()
    {
        text.text = "-" + ((int)damage).ToString();
        transform.Translate(transform.up * Time.deltaTime * 2f);
    }

    public void OnCreatedInPool() // ó�� �������� ��
    {
        Init();
    }

    public void OnGettingFromPool() // ��� �� �� 
    {

        //MoveText();
        Invoke("Return", 1f);
    }

    public void Return() // ������Ʈ ����
    {
        spawn.ReturnDamageText(this);
    }
}
