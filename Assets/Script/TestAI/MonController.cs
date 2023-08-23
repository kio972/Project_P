using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonController : FSM<MonController>
{
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

    private float patrolSpeed;

    // Start is called before the first frame update
    void Start()
    {
        InitState(this, MonIdle.Instance);
    }

    private void Awake()
    {
        targetTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        MonInit();
    }

    public void MonInit()
    {
        target = false;
        currPoint = pointB.transform;
        patrolSpeed = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        FSMUpdate();
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

    public void Chase() // �i�ư��� ����
    {
        if (Vector2.Distance(transform.position, targetTrans.position) > 1f && target) // �÷��̾ ���� �ȿ� ������ �i�ư���
            transform.position = Vector2.MoveTowards(transform.position, targetTrans.position, 4f * Time.deltaTime);
        else // ���� ������ ������ ������ ���ư���
        {
            transform.position = Vector2.MoveTowards(transform.position, idlePoint.transform.position, 2f * Time.deltaTime);
            if (transform.position == idlePoint.transform.position) // ���� ���������� �ٽ� �����·�!
                PatrolState();
        }
        
        if(Vector2.Distance(transform.position, targetTrans.position) <= 1f && target) // Ÿ���� ���ݹ��� �ȿ� ������!
        {
            ChangeState(MonAttack.Instance);
        }
    }

    public void Attack()
    {
        if(target)
        {
            Debug.Log("����!");
        }
        else
        {
            ChangeState(MonChase.Instance);
        }
    }
}
