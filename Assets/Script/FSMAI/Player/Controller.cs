using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;

public class Controller : FSM<Controller>
{
    private bool controllMode;
    public bool ControllMode
    {
        get { return controllMode; }
        set { controllMode = value; }
    }

    private bool same;
    public bool Same
    {
        get { return same; }
        set { same = value; }
    }

    public ControllerManager controllerManager;



    public Animator anim;

    private float maxHP;
    private float currHP;

    private float moveSpeed;
    private Vector3 moveDirection = Vector3.zero;

    float x;

    public bool basicAttackCool;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GameObject.Find("GroundCheck").transform;
        controllerManager = GetComponentInParent<ControllerManager>();
        CharInit();
    }

    public void InitState(bool controller)
    {
        if(controller)
            InitState(this, FSMStateEx.Instance);
        else
            InitState(this, PlayerAI.Instance);
    }

    public void CharInit()
    {
        moveSpeed = 5f;
        basicAttackCool = true;
        maxHP = currHP = 15f;
        jumpPower = 8;
    }

    // Update is called once per frame
    void Update()
    {
        FSMUpdate();
    }

    private Vector3 RIGHT = new Vector3(0.55f, 0.55f, 1);
    private Vector3 LEFT = new Vector3(-0.55f, 0.55f, 1);

    Vector2 direction;
    public Rigidbody2D rb;
    [SerializeField]
    LayerMask groundLayer;
    Vector2 jump;

    [SerializeField]
    float jumpPower;

    private Transform groundCheck;

    public void ChangeMode()
    {
        if(controllMode)
            ChangeState(FSMStateEx.Instance);
        else
            ChangeState(PlayerAI.Instance);
    }

    public void Movement()
    {
        x = Input.GetAxisRaw("Horizontal");
        direction.x = x * moveSpeed;
        direction.y = rb.velocity.y;
        rb.velocity = direction;

        anim.SetBool("Run", direction != Vector2.zero);

        if(Input.GetKeyDown(KeyCode.C))
        {
            bool isGround =
                Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
            if (isGround)
            {
                jump = Vector2.up * jumpPower;
                rb.velocity = jump;
            }
        }

        /*x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(x, y, 0);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        anim.SetBool("Run", moveDirection != Vector3.zero);*/
        
        if (x > 0 && transform.localScale != RIGHT)
            transform.GetChild(0).localScale = RIGHT;
        else if (x < 0 && transform.localScale != LEFT)
            transform.GetChild(0).localScale = LEFT;
    }

    public void BasicAttackAnim()
    {
        basicAttackCool = false;
        anim.SetTrigger("BasicAttack");
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

    

    private float standDir = 0.1f;
    private bool targetDistancRight;
    public void LookAtPlayer(Transform t)
    {
        Vector2 targetDistance = t.position - transform.position;

        targetDistancRight = targetDistance.x > standDir;
        if (targetDistancRight)
            transform.GetChild(0).localScale = RIGHT;
        else
            transform.GetChild(0).localScale = LEFT;

    }
}
