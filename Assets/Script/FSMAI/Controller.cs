using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;

public class Controller : FSM<Controller>
{
    private Animator anim;

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
        anim = GetComponentInChildren<Animator>();
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

    private Vector3 RIGHT = new Vector3(0.55f, 0.55f, 1);
    private Vector3 LEFT = new Vector3(-0.55f, 0.55f, 1);

    public void Movement()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(x, y, 0);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        anim.SetBool("Run", moveDirection != Vector3.zero);
        
        if (x > 0 && transform.localScale != RIGHT)
            transform.GetChild(0).localScale = RIGHT;
        else if (x < 0 && transform.localScale != LEFT)
            transform.GetChild(0).localScale = LEFT;
    }

    public void BasicAttackAnim()
    {
        basicAttackCool = false;
        anim.SetTrigger("Attack");
        StartCoroutine(CoolTimeBasic(2.0f));
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
