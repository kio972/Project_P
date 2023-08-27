using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;

public class Controller : FSM<Controller>
{
    private Animator anim;

    private DrawGizmo attackPivot;
    [SerializeField]
    private LayerMask targetLayer;

    private float maxHP;
    private float currHP;

    private float moveSpeed;
    private Vector3 moveDirection = Vector3.zero;

    float x;
    float y;

    public bool basicAttackCool;

    void Awake()
    {
        InitState(this, FSMStateEx.Instance);
        Transform t = transform.Find("BasicPivot");
        if (t != null)
            attackPivot = t.GetComponent<DrawGizmo>();
        anim = GetComponent<Animator>();

        CharInit();
    }

    public void CharInit()
    {
        moveSpeed = 5f;
        basicAttackCool = true;
        maxHP = currHP = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        FSMUpdate();
    }

    private Vector3 RIGHT = new Vector3(1, 1, 1);
    private Vector3 LEFT = new Vector3(-1, 1, 1);

    public void Movement()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(x, y, 0);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        anim.SetBool("Run", moveDirection != Vector3.zero);

        if (x > 0 && transform.localScale != RIGHT)
            transform.localScale = RIGHT;
        else if (x < 0 && transform.localScale != LEFT)
            transform.localScale = LEFT;
    }

    public void BasicAttack()
    {
        basicAttackCool = false;
        anim.SetTrigger("Attack");
        StartCoroutine(CoolTimeBasic(2.0f));

        Collider2D[] enemyArr =
            Physics2D.OverlapBoxAll(attackPivot.transform.position,
                                 attackPivot.size,
                                 0,
                                 targetLayer);

        foreach (Collider2D enemy in enemyArr)
        {
            if(enemy.TryGetComponent<MonController>(out MonController monController))
            {
                monController.TakeDamage(1);
            }
        }
    }

    IEnumerator CoolTimeBasic(float cool)
    {
        yield return YieldInstructionCache.WaitForSeconds(cool);
        basicAttackCool = true;
    }

    public void TakeDamage(float damage)
    {
        anim.SetTrigger("TakeDamage");
        currHP -= damage;
        if (currHP <= 0)
        {
            StartCoroutine(OnDie());
        }
        else
        {
            Debug.Log(transform.name + " 플레이어 체력 : " + currHP);
        }
    }

    IEnumerator OnDie()
    {
        Debug.Log("플레이어 주금!");
        anim.SetTrigger("Die");
        yield return YieldInstructionCache.WaitForSeconds(2f);
        GameObject.Destroy(gameObject);
    }
}
