using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;
using JinWon;

public class Projectile : MonoBehaviour, IPoolObject
{
    private ProjectileSpawn spawn;
    private Rigidbody2D rigid2D;

    [SerializeField]
    private string TargetTag;

    [SerializeField]
    private float moveSpeed;

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

    private float time;
    public float Time
    {
        set => time = value;
    }

    private float scale;
    public float SCALE
    {
        set => scale = value;
    }

    private Vector3 vec;
    public Vector3 VEC
    {
        set => vec = value;
    }

    private bool isDamage;

    private void Awake()
    {
        spawn = GetComponentInParent<ProjectileSpawn>();
        rigid2D = GetComponent<Rigidbody2D>();
        
    }

    public void ProjectileInit()
    {
        spawn = GetComponentInParent<ProjectileSpawn>();
        rigid2D = GetComponent<Rigidbody2D>();
    }

    public void OnCreatedInPool()
    {
        parentObject = transform.parent.gameObject;
        //ProjectileInit();
    }

    private Vector2 parentVec;
    private GameObject parentObject;

    public void OnGettingFromPool()
    {
        //StartCoroutine(MoveProjectile());
        //ProjectileMove();
        isDamage = true;

        parentVec = transform.parent.position;
        transform.parent = null;

        if (scale != 0)
        {
            //transform.position = transform.parent.position;

            /*parentVec = transform.parent.position;
            transform.parent = null;*/
            transform.position = parentVec;
        }

        StartCoroutine(ReturnProjectile());
    }

    private void Update()
    {
        if(scale > 0)
        {
            rigid2D.velocity = transform.right * moveSpeed;
            //Debug.Log("ºÒ·¿ ¹«ºê+");
        }
        else if(scale < 0)
        {
            rigid2D.velocity = transform.right * -moveSpeed;
            //Debug.Log("ºÒ·¿ ¹«ºê- " + scale);
        }
        else if(scale == 0)
        {
            rigid2D.velocity = transform.up * -moveSpeed;
            //Debug.Log(scale);
        }

    }

    private void Return()
    {
        /*transform.parent = partentObject.transform;
        //transform.rotation = partentObject.transform.rotation;
        transform.position = parentVec;*/

        transform.parent = parentObject.transform;

        spawn.ReturnProjectile(this);
    }

    IEnumerator ReturnProjectile()
    {
        yield return YieldInstructionCache.WaitForSeconds(5f);
        //spawn.ReturnProjectile(this);
        Return();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isDamage)
        {
            if(collision.CompareTag(TargetTag) && collision.TryGetComponent<Controller>(out Controller player))
            {
                Debug.Log("ÇÃ·¹ÀÌ¾î¿Í Á¢ÃË µÊ");
                isDamage = false;
                player.TakeDamage(damage, transform.position);
                Return();
            }
        }
    }
}
