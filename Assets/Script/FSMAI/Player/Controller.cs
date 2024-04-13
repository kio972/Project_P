using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;

public class Controller : FSM<Controller>
{
    [SerializeField]
    private PlayerType playerType;
    public PlayerType PlayerType
    {
        get { return playerType; }
    }

    private void PlayerInfo()
    {
        switch (playerType)
        {
            case PlayerType.Warrior:
                {
                    WarriorInfo playerInfo = new WarriorInfo();
                    currHP = GameManager.Inst.PlayerInfo.currHP;
                    maxHP = playerInfo.HP;
                    //currMP = maxHP = playerInfo.MP;
                    break;
                }
            case PlayerType.Priest:
                {
                    PriestInfo playerInfo = new PriestInfo();
                    currHP = maxHP = playerInfo.HP;
                    currHP = maxHP = playerInfo.MP;
                    break;
                }
            case PlayerType.Archer:
                {
                    ArcherInfo playerInfo = new ArcherInfo();
                    currHP = maxHP = playerInfo.HP;
                    currHP = maxHP = playerInfo.MP;
                    break;
                }
        }

        if(GameManager.Inst.PlayerInfo.charactorVec == "Left")
        {
            transform.position = new Vector3(-8f, -1.55f, 0f);
        }
        else if(GameManager.Inst.PlayerInfo.charactorVec == "Right")
        {
            transform.position = new Vector3(43f, -1.55f, 0f);
            transform.GetChild(0).localScale = LEFT;
            transformX = -1;
        }
    }

    private CharStateUI charStateUI;

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

    public bool basicAttackAI;
    private bool BasicAttackAI
    {
        set { basicAttackAI = value; }
    }

    public Animator anim;

    //[SerializeField]
    private float maxHP;
    //[SerializeField]
    private float currHP;
    public float _MaxHp { get => maxHP; }
    public float _CurrHp { get => currHP; }

    private float defaultSpeed;
    private float moveSpeed;
    private Vector3 moveDirection = Vector3.zero;

    private float x;

    private SpriteRenderer sr;

    public bool basicAttackCool;
    private bool BasicAttackCool
    {
        get { return basicAttackCool; }
    }

    public bool isGround;
    private bool jumpAttack;

    [SerializeField]
    private List<MonsterManager> msm = new List<MonsterManager>();

    void Awake()
    {
        charStateUI = GameObject.Find("MainChar (1)").GetComponent<CharStateUI>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GameObject.Find("GroundCheck").transform;
        jumpCheck = GameObject.Find("JumpCheck").transform;
        controllerManager = GetComponentInParent<ControllerManager>();
        sr = GetComponentInChildren<SpriteRenderer>();
        CharInit();
    }

    public void InitState(bool controller)
    {
        if(controller)
            InitState(this, FSMStateEx.Instance);
        else
            InitState(this, PlayerAI.Instance);
    }

    Color takeA = new Color(1, 1, 1, 0);
    Color fullA = new Color(1, 1, 1, 1);

    public void CharInit()
    {
        PlayerInfo();
        charStateUI.Init(this);
        defaultSpeed = moveSpeed = 3f;
        basicAttackCool = true;
        jumpPower = 6;
    }

    // Update is called once per frame
    void Update()
    {
        FSMUpdate();
    }

    private Vector3 RIGHT = new Vector3(1f, 1f, 1f); // 0.55f, 0.55f, 1이였음
    private Vector3 LEFT = new Vector3(-1f, 1f, 1f);

    Vector2 direction;
    public Rigidbody2D rb;
    [SerializeField]
    LayerMask groundLayer;
    Vector2 jump;

    [SerializeField]
    float jumpPower;

    private bool canDash = true;
    private bool isDashing = false;
    private float dashingPower = 10f;
    private float dashingTime = 0.2f;
    private float dashingCoolDown = 1f;

    private Transform groundCheck;
    private Transform jumpCheck;

    private bool isJump;

    public void ChangeMode()
    {
        if(controllMode)
            ChangeState(FSMStateEx.Instance);
        else
            ChangeState(PlayerAI.Instance);
    }

    private float transformX = 1f;

    private bool isMove = true;

    public void Movement()
    {
        if(isMove && !onDie)
        {
            if (isDashing)
            {
                return;
            }

            x = Input.GetAxisRaw("Horizontal");
            direction.x = x * moveSpeed;
            direction.y = rb.velocity.y;
            rb.velocity = direction;

            anim.SetBool("Run", direction != Vector2.zero);
            if (anim.GetBool("Run") && isGround)
                SoundManager.Inst.PlayerRun("Walk_1.4");

            if (Input.GetKeyDown(KeyCode.Space) && canDash)
            {
                StartCoroutine(Dash(transformX));
            }

            isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
            //isJump = Physics2D.OverlapCircle(jumpCheck.position, 0.1f, groundLayer);

            if (Input.GetKeyDown(KeyCode.C))
            {
                /*if (isGround)
                {
                    SoundManager.Inst.PlaySFX("Warrior_Jump");
                    jump = Vector2.up * jumpPower;
                    rb.velocity = jump;
                }*/

                SoundManager.Inst.PlaySFX("Warrior_Jump");
                jump = Vector2.up * jumpPower;
                rb.velocity = jump;
            }

            if(!jumpAttack)
            {
                if (!isGround && !onDie && !isDashing)
                    anim.SetBool("Jump", true);
                else
                    anim.SetBool("Jump", false);
            }
            

            /*x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");

            moveDirection = new Vector3(x, y, 0);
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            anim.SetBool("Run", moveDirection != Vector3.zero);*/

            if (x > 0 && transform.GetChild(0).localScale != RIGHT)
            {
                transform.GetChild(0).localScale = RIGHT;
                transformX = 1f;
            }
            else if (x < 0 && transform.GetChild(0).localScale != LEFT)
            {
                transform.GetChild(0).localScale = LEFT;
                transformX = -1f;
            }


            //Debug.Log(x + " 현재 로컬 스케일은? : " + transform.GetChild(0).localScale);
        }
    }

    IEnumerator Dash(float vec)
    {
        anim.SetTrigger("Dash");
        SoundManager.Inst.PlaySFX("Warrior_Dash");
        canDash = false;
        isDashing = true;
        float originalGrabity = rb.gravityScale;
        rb.gravityScale = 0f;
        if(vec > 0)
            rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        else
            rb.velocity = new Vector2(transform.localScale.x * -dashingPower, 0f);
        yield return YieldInstructionCache.WaitForSeconds(dashingTime);
        rb.gravityScale = originalGrabity;
        isDashing = false;
        yield return YieldInstructionCache.WaitForSeconds(dashingCoolDown);
        canDash = true;

    }

    public void BasicAttackAnim()
    {
        isMove = false;
        basicAttackCool = false;
        anim.SetTrigger("BasicAttack");
        StartCoroutine(CoolTimeMove(0.8f));
        StartCoroutine(CoolTimeBasic(1.0f));
    }

    public void JumpAttackAnim()
    {
        anim.SetBool("Jump", false);
        jumpAttack = true;
        isMove = false;
        basicAttackCool = false;
        anim.SetTrigger("BasicAttack");
        StartCoroutine(CoolTimeMove(0.8f));
        StartCoroutine(CoolTimeJumpAttack(1.0f));
    }

    IEnumerator CoolTimeMove(float cool)
    {
        direction.x = x * 0;
        direction.y = rb.velocity.y;
        rb.velocity = direction;
        anim.SetBool("Run", false);
        yield return YieldInstructionCache.WaitForSeconds(cool);
        isMove = true;
    }

    IEnumerator CoolTimeBasic(float cool)
    {
        yield return YieldInstructionCache.WaitForSeconds(0.3f);
        SoundManager.Inst.PlaySFX("Warrior_BasicAttack");
        yield return YieldInstructionCache.WaitForSeconds(cool - 0.3f);
        basicAttackCool = true;
    }

    IEnumerator CoolTimeJumpAttack(float cool)
    {
        yield return YieldInstructionCache.WaitForSeconds(0.3f);
        SoundManager.Inst.PlaySFX("Warrior_BasicAttack");
        yield return YieldInstructionCache.WaitForSeconds(cool - 0.3f);
        basicAttackCool = true;
        jumpAttack = false;
    }

    private bool onDie = false;

    public void TakeDamage(float damage, Vector2 pos)
    {
        currHP -= damage;
        GameManager.Inst.CharHPInit(currHP);
        SoundManager.Inst.PlaySFX("Spear_Sting_TakeDamage");
        if (currHP <= 0)
        {
            //anim.SetTrigger("Die");
            if (!onDie)
                StartCoroutine(OnDie());
        }
        else
        {
            anim.SetTrigger("TakeDamage");
            float x = transform.position.x - pos.x;
            if (x < 0)
                x = 1;
            else
                x = -1;
            StartCoroutine(TakeAlpha());
            StartCoroutine(Knockback(x));
            Debug.Log(transform.name + " 플레이어 체력 : " + currHP);
        }
    }

    private bool isKnockback;

    IEnumerator Knockback(float dir)
    {
        isKnockback = true;
        float ctime = 0;
        while(ctime < 0.2f)
        {
            if (transform.rotation.y == 0)
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime * dir);
                //transform.Translate(Vector2.up * 3f * Time.deltaTime * dir);
                Debug.Log("왼쪽");
            }
            else
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime * -1f * dir);
                
                Debug.Log("오른쪽");
            }

            transform.Translate(Vector2.up * 3f * Time.deltaTime);

            ctime += Time.deltaTime;
            yield return null;
        }
        isKnockback = false;
    }

    IEnumerator TakeAlpha()
    {
        for(int i = 0; i < 3; i++)
        {
            yield return YieldInstructionCache.WaitForSeconds(0.1f);
            sr.color = takeA;
            yield return YieldInstructionCache.WaitForSeconds(0.1f);
            sr.color = fullA;
        }
    }

    IEnumerator OnDie()
    {
        onDie = true;
        anim.SetTrigger("Die");

        for (int i = 0; i < msm.Count; i++)
        {
            msm[i].PauseMonster();
        }
        
        yield return YieldInstructionCache.WaitForSeconds(1f);
        GameManager.Inst.AsyncLoadNextScene("CalendarScene");
        GameManager.Inst.PlayerDataInit();
        //GameObject.Destroy(gameObject);
    }

    public void RunAnim(bool init)
    {
        if (init)
            anim.SetBool("Run", true);
        else
            anim.SetBool("Run", false);
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
