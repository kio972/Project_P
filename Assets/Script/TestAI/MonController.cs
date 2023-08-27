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

    private float patrolSpeed;
    private float chaseSpeed;
    [SerializeField]
    private LayerMask targetLayer;
    private DrawGizmo basicPivot;

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
    }

    private void MonInit()
    {
        target = false;
        currPoint = pointB.transform;
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

    public void PatrolState() // �����·� �ٲٴ� �Լ�
    {
        ChangeState(MonPatrol.Instance);
    }

    public void Patrol() // ������
    {
        if(currPoint == pointB.transform)
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

        if (target) // �÷��̾ MonScan�����ȿ� ������ �i�ư��� ���·� �ٲ��ִ� �Լ� 
        {
            Debug.Log("Ÿ�� Ÿ��!");
            ChangeState(MonChase.Instance);
        }
    }

    private void Flip() // �ٶ󺸴� ������ ������ �ٲ��ִ� �Լ�
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public Collider2D[] basicPivotArr;

    public void Chase() // �i�ư��� ����
    {
        if (Vector2.Distance(transform.position, targetTrans.position) > 1f && target) // �÷��̾ ���� �ȿ� ������ �i�ư���
        {
            transform.position = Vector2.MoveTowards(transform.position, targetTrans.position, chaseSpeed * Time.deltaTime);
            LookAtDir();
        }
        else // ���� ������ ������ ������ ���ư���
        {
            /*transform.position = Vector2.MoveTowards(transform.position, idlePoint.transform.position, 2f * Time.deltaTime);
            if (transform.position == idlePoint.transform.position) // ���� ���������� �ٽ� �����·�!*/
            PatrolState();
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
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);

    }

    public void Attack()
    {
        if(target)
        {
            //Debug.Log(basicAttack + " " + basicTarget);
            if(basicAttackCool && basicTarget)
                BasicAttack();
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
        LookAtDir();
        basicAttackCool = false;
        StartCoroutine(CoolTimeBasic(2));
        anim.SetTrigger("BasicAttack");

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
        if(currHP <= 0)
        {
            StartCoroutine(OnDie());
        }
        else
        {
            Debug.Log(transform.name + "���� ü�� : " + currHP);
        }
    }

    IEnumerator OnDie()
    {
        Debug.Log("�ֱ�!");
        anim.SetTrigger("Die");
        yield return YieldInstructionCache.WaitForSeconds(2f);
        GameObject.Destroy(gameObject);
    }
}
