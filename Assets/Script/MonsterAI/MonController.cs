using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;

public enum MonsterType
{
    Mospy_Spear,
    Mospy_Bow,
}



public class MonsterInfo
{
    public float Mospy_Spear_Damage = 4;
    public float Mospy_Spear_HP = 33;
    public float Mospy_Spear_MP;

    public float Mospy_Bow_Damage = 8;
    public float Mospy_Bow_HP = 50;
    public float Mospy_Bow_MP;
}

public abstract class MonController : FSM<MonController>
{
    [SerializeField]
    private MonsterType monsterType;

    private MonsterInfo monsterInfo;

    protected Animator anim;

    protected Transform targetTrans;
    public Transform TargetTrans
    {
        get { return targetTrans; }
    }

    Rigidbody2D rb;

    [SerializeField]
    private GameObject pointA;
    [SerializeField]
    private GameObject pointB;

    [SerializeField]
    private GameObject idlePoint;
    [SerializeField]
    private Transform currPoint;

    private PoolTextSpawn poolTextSpawn;

    private bool target;
    public bool Target
    {
        get { return target; }
        set { target = value; }
    }

    private bool basicTarget;
    public bool BasicTarget
    {
        get { return basicTarget; }
        set { basicTarget = value; }
    }

    private bool same;
    public bool Same
    {
        get { return same; }
        set { same = value; }
    }

    [SerializeField]
    private bool moveType;
    [SerializeField]
    private float patrolSpeed;
    [SerializeField]
    protected float chaseSpeed;
    [SerializeField]
    private LayerMask targetLayer;
    private DrawGizmo basicPivot;

    private MonsterManager monsterManager;

    private float currHP;
    private float maxHP;
    private float damage;

    public void Init()
    {
        InitState(this, MonIdle.Instance);
        MonInit();
    }

    private void Awake()
    {
        targetTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb  = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Transform t = transform.Find("BasicPivot");
        if (t != null)
            basicPivot = t.GetComponent<DrawGizmo>();
        poolTextSpawn = GameObject.Find("PoolTextSpawn").GetComponent<PoolTextSpawn>();
        monsterManager = GetComponentInParent<MonsterManager>();
        monsterInfo = new MonsterInfo();
    }

    private void MonInit()
    {
        MonStat();
        target = false;
        if(pointB != null)
            currPoint = pointB.transform;
        //chaseSpeed = 2f;
        //patrolSpeed = 1f;
        basicAttackCool = true;
        basicTarget = false;

    }

    private void MonStat()
    {
        switch(monsterType)
        {
            case MonsterType.Mospy_Bow:
                {
                    maxHP = currHP = monsterInfo.Mospy_Bow_HP;
                    damage = monsterInfo.Mospy_Bow_Damage;
                    break;
                }
            case MonsterType.Mospy_Spear:
                {
                    maxHP = currHP = monsterInfo.Mospy_Spear_HP;
                    damage = monsterInfo.Mospy_Spear_Damage;
                    break;
                }
        }
    }

    void Update()
    {
        FSMUpdate();
    }

    public void AnimRun(bool value)
    {
        if(moveType)
        {
            if (value)
                chaseSpeed = 2f;
            else
                chaseSpeed = 0;
            anim.SetBool("Run", value);
        }
    }

    public void PatrolState() // 경비상태로 바꾸는 함수
    {
        ChangeState(MonPatrol.Instance);
    }

    public void ChaseState()
    {
        ChangeState(MonChase.Instance);
    }

    bool mark = false;

    public void Patrol() // 경비상태
    {
        if(pointA != null && pointB != null)
        {
            if (!anim.GetBool("Run"))
                anim.SetBool("Run", true);

            if (!target)
            {
                if (currPoint == pointB.transform)
                    transform.position = Vector2.MoveTowards(transform.position, pointB.transform.position, patrolSpeed * Time.deltaTime);
                else
                    transform.position = Vector2.MoveTowards(transform.position, pointA.transform.position, patrolSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, currPoint.position) < 0.5f && currPoint == pointB.transform)
                {
                    Flip();
                    currPoint = pointA.transform;
                    //transform.localScale = new Vector3(-1f, 0f, 0f);
                }
                if (Vector2.Distance(transform.position, currPoint.position) < 0.5f && currPoint == pointA.transform)
                {
                    Flip();
                    currPoint = pointB.transform;
                    //transform.LookAt(pointB.transform);
                }
            }
        }

        if (target) // 플레이어가 MonScan범위안에 들어오면 쫒아가는 상태로 바꿔주는 함수 
        {
            if (!mark)
            {
                TargetDetection();
                //monsterManager.TargetDetectionManager();
            }
            
        }

        /*if (pointA == null || pointB == null)
            return;*/
    }

    public void PatrolInit()
    {
        if (target) // 플레이어가 MonScan범위안에 들어오면 쫒아가는 상태로 바꿔주는 함수 
        {
            if (mark)
            {
                ChaseState();
            }

        }
    }

    public void TargetDetection() // 처음 타겟을 감지했을 때.
    {
        mark = true;
        target = true;
        poolTextSpawn.SpawnMarkText("ExclamationMark", transform.position);
        AnimRun(false);
        Invoke("ChaseState", 1.0f);
    }

    private void Flip() // 바라보는 방향을 역으로 바꿔주는 함수
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public Collider2D[] basicPivotArr;

    public void Chase() // 쫒아가는 상태
    {
        if(targetTrans != null && !pause)
        {
            Vector2 targetPosition = new Vector2(targetTrans.position.x, transform.position.y);
            if (Vector2.Distance(transform.position, targetPosition) > 0.5f && target && !same) // 플레이어가 범위 안에 있으면 쫒아가기 targetTrans.position
            {
                if (anim.GetBool("Run") == false)
                    AnimRun(true);

                //Vector2 targetPosition = new Vector2(targetTrans.position.x, transform.position.y);

                transform.position = Vector2.MoveTowards(transform.position, targetPosition, chaseSpeed * Time.deltaTime);
                //transform.position = Vector2.MoveTowards(transform.position, targetTrans.position, chaseSpeed * Time.deltaTime);
                
                LookAtDir();
            }
            else if (same)// 범위 밖으로 나가면 집으로 돌아가기
            {
                LookAtDir();
                if (anim.GetBool("Run") == true)
                    AnimRun(false);
                /*transform.position = Vector2.MoveTowards(transform.position, idlePoint.transform.position, 2f * Time.deltaTime);
                if (transform.position == idlePoint.transform.position) // 집에 도착했으면 다시 경비상태로!*/
                //PatrolState();
            }

            if (basicTarget && target && !pause)
                ChangeState(MonAttack.Instance);
        }
    }

    private bool pause = false;

    public void Pause(bool state)
    {
        if (state)
            pause = true;
        else
            pause = false;
    }

    private float standDir = 0.1f;
    private bool targetDistancRight;
    protected void LookAtDir()
    {
        Vector2 targetDistance = targetTrans.position - transform.position;

        targetDistancRight = targetDistance.x > standDir;
        if(targetDistancRight)
            transform.localScale = new Vector3(1f, 1f, 1f); // -0.7f, 0.7f, 1 이였음
        else
            transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    public void Attack()
    {
        if(target)
        {
            //Debug.Log(basicAttack + " " + basicTarget);
            if(basicAttackCool && basicTarget)
            {
                AttackBasic();

                /*LookAtDir();
                basicAttackCool = false;
                chaseSpeed = 0f;
                anim.SetBool("Run", false);
                StartCoroutine(CoolTimeBasic(3f));
                anim.SetTrigger("BasicAttack");
                SoundManager.Inst.PlaySFX("Spear_Day_Sting");*/
            }
            else if(!basicTarget)
            {
                basicPivotArr = null;
                StartCoroutine(CoolTimeMove(1.5f));
            }
        }
        else
        {
            basicPivotArr = null;
            StartCoroutine(CoolTimeMove(1.5f));
            //ChangeState(MonChase.Instance);
        }
    }

    IEnumerator CoolTimeMove(float cool)
    {
        //target = false;
        yield return YieldInstructionCache.WaitForSeconds(cool);
        ChangeState(MonChase.Instance);
    }

    public abstract void AttackBasic();

    private int basicDamage;

    private void BasicAttack()
    {
        basicDamage = Random.Range(2, 7);
        basicPivotArr =
            Physics2D.OverlapBoxAll(basicPivot.transform.position,
                                 basicPivot.size,
                                 0,
                                 targetLayer);

        foreach (Collider2D enemy in basicPivotArr)
        {
            if (enemy.TryGetComponent<Controller>(out Controller controller))
            {
                controller.TakeDamage(basicDamage, transform.position);
            }
        }
    }

    

    private void MonsterDirection()
    {
        
            
    }

    protected bool basicAttackCool;

    protected IEnumerator CoolTimeBasic(float cool)
    {
        yield return YieldInstructionCache.WaitForSeconds(cool);
        basicAttackCool = true;
        if(moveType)
            chaseSpeed = 2f;
    }

    public void TakeDamage(float damage, Transform target)
    {
        targetTrans = target;
        anim.SetTrigger("TakeDamage");
        currHP -= damage;
        poolTextSpawn.SpawnDamageText("DamageText", transform.position, damage);
        SoundManager.Inst.PlaySFX("Smash_TakeDamage");
        if (currHP <= 0)
        {
            if(!die)
                StartCoroutine(OnDie());
            //transform.gameObject.SetActive(false);
            //GameObject.Destroy(gameObject);
            //StartCoroutine(OnDie());
        }
        else
        {
            float x = transform.position.x - target.position.x;
            if (x < 0)
                x = 1;
            else
                x = -1;
            //StartCoroutine(Knockback(x));
            Debug.Log(transform.name + "몬스터 체력 : " + currHP);
        }

    }

    private bool isKnockback;

    IEnumerator Knockback(float dir)
    {
        isKnockback = true;
        float ctime = 0;
        while (ctime < 0.2f)
        {
            if (transform.rotation.y == 0)
            {
                transform.Translate(Vector2.left * 3f * Time.deltaTime * dir);
                //transform.Translate(Vector2.up * 3f * Time.deltaTime * dir);
                Debug.Log("왼쪽");
            }
            else
            {
                transform.Translate(Vector2.left * 3f * Time.deltaTime * -1f * dir);

                Debug.Log("오른쪽");
            }

            transform.Translate(Vector2.up * 3f * Time.deltaTime);

            ctime += Time.deltaTime;
            yield return null;
        }
        isKnockback = false;
    }

    private bool die;
    public bool Die
    {
        get { return die; }
    }

    private int whatItem;

    IEnumerator OnDie()
    {
        die = true;
        monsterManager.MonsterDie();
        anim.SetTrigger("Die");

        whatItem = Random.Range(0, 2);
        if (whatItem > 0)
            poolTextSpawn.SpawnDropItem("Gold", transform.position);
        else
            poolTextSpawn.SpawnDropItem("Ston", transform.position);

        yield return YieldInstructionCache.WaitForSeconds(0.2f);
        transform.gameObject.SetActive(false);
    }
}
