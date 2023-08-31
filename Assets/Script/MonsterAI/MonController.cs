using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;

public class MonController : FSM<MonController>
{
    private Animator anim;

    Transform targetTrans;
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

    private float patrolSpeed;
    private float chaseSpeed;
    [SerializeField]
    private LayerMask targetLayer;
    private DrawGizmo basicPivot;

    private MonsterManager monsterManager;

    // Start is called before the first frame update
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
    }

    private void MonInit()
    {
        target = false;
        //currPoint = pointB.transform;
        chaseSpeed = 4f;
        patrolSpeed = 2f;
        basicAttackCool = true;
        basicTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        FSMUpdate();
    }

    public void AnimRun(bool value)
    {
        if (value)
            chaseSpeed = 4f;
        else
            chaseSpeed = 0;
        anim.SetBool("Run", value);
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
        if(!target)
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

        if (target) // 플레이어가 MonScan범위안에 들어오면 쫒아가는 상태로 바꿔주는 함수 
        {
            if(!mark)
            {
                monsterManager.TargetDetectionManager();
            }
        }

        if (pointA == null || pointB == null)
            return;
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
        if (Vector2.Distance(transform.position, targetTrans.position) > 1f && target && !same) // 플레이어가 범위 안에 있으면 쫒아가기
        {
            if (anim.GetBool("Run") == false)
                anim.SetBool("Run", true);

            transform.position = Vector2.MoveTowards(transform.position, targetTrans.position, chaseSpeed * Time.deltaTime);
            LookAtDir();
        }
        else if(same)// 범위 밖으로 나가면 집으로 돌아가기
        {
            LookAtDir();
            if (anim.GetBool("Run") == true)
                anim.SetBool("Run", false);
            /*transform.position = Vector2.MoveTowards(transform.position, idlePoint.transform.position, 2f * Time.deltaTime);
            if (transform.position == idlePoint.transform.position) // 집에 도착했으면 다시 경비상태로!*/
            //PatrolState();
        }

        if (basicTarget && target)
            ChangeState(MonAttack.Instance);
    }

    private float standDir = 0.1f;
    private bool targetDistancRight;
    private void LookAtDir()
    {
        Vector2 targetDistance = targetTrans.position - transform.position;

        targetDistancRight = targetDistance.x > standDir;
        if(targetDistancRight)
            transform.localScale = new Vector3(-0.7f, 0.7f, 1);
        else
            transform.localScale = new Vector3(0.7f, 0.7f, 1);

    }

    public void Attack()
    {
        if(target)
        {
            //Debug.Log(basicAttack + " " + basicTarget);
            if(basicAttackCool && basicTarget)
            {
                LookAtDir();
                basicAttackCool = false;
                StartCoroutine(CoolTimeBasic(2));
                anim.SetTrigger("BasicAttack");
            }
            else if(!basicTarget)
            {
                basicPivotArr = null;
                ChangeState(MonChase.Instance);
            }
        }
        else
        {
            basicPivotArr = null;
            ChangeState(MonChase.Instance);
        }
    }

    private void BasicAttack()
    {

        basicPivotArr =
            Physics2D.OverlapBoxAll(basicPivot.transform.position,
                                 basicPivot.size,
                                 0,
                                 targetLayer);

        foreach (Collider2D enemy in basicPivotArr)
        {
            if (enemy.TryGetComponent<Controller>(out Controller controller))
            {
                controller.TakeDamage(1);
            }
        }
    }

    

    private void MonsterDirection()
    {
        
            
    }

    private bool basicAttackCool;

    IEnumerator CoolTimeBasic(float cool)
    {
        yield return YieldInstructionCache.WaitForSeconds(cool);
        basicAttackCool = true;
    }

    private float currHP = 10;

    public void TakeDamage(float damage)
    {
        anim.SetTrigger("TakeDamage");
        currHP -= damage;
        poolTextSpawn.SpawnDamageText("DamageText", transform.position, damage);
        if(currHP <= 0)
        {
            GameObject.Destroy(gameObject);
            //StartCoroutine(OnDie());
        }
        else
        {
            Debug.Log(transform.name + "몬스터 체력 : " + currHP);
        }
    }

    IEnumerator OnDie()
    {
        Debug.Log("주금!");
        anim.SetTrigger("Die");
        yield return YieldInstructionCache.WaitForSeconds(2f);
        GameObject.Destroy(gameObject);
    }
}
