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

    public void PatrolState() // 경비상태로 바꾸는 함수
    {
        ChangeState(MonPatrol.Instance);
    }

    public void Patrol() // 경비상태
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

        if (target) // 플레이어가 MonScan범위안에 들어오면 쫒아가는 상태로 바꿔주는 함수 
        {
            Debug.Log("타겟 타겟!");
            ChangeState(MonChase.Instance);
        }
           
    }

    private void Flip() // 바라보는 방향을 역으로 바꿔주는 함수
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Chase() // 쫒아가는 상태
    {
        if (Vector2.Distance(transform.position, targetTrans.position) > 1f && target) // 플레이어가 범위 안에 있으면 쫒아가기
            transform.position = Vector2.MoveTowards(transform.position, targetTrans.position, 4f * Time.deltaTime);
        else // 범위 밖으로 나가면 집으로 돌아가기
        {
            transform.position = Vector2.MoveTowards(transform.position, idlePoint.transform.position, 2f * Time.deltaTime);
            if (transform.position == idlePoint.transform.position) // 집에 도착했으면 다시 경비상태로!
                PatrolState();
        }
        
        if(Vector2.Distance(transform.position, targetTrans.position) <= 1f && target) // 타겟이 공격범위 안에 들어오면!
        {
            ChangeState(MonAttack.Instance);
        }
    }

    public void Attack()
    {
        if(target)
        {
            Debug.Log("공격!");
        }
        else
        {
            ChangeState(MonChase.Instance);
        }
    }
}
