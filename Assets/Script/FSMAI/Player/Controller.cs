using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;
using YeongJun;
using UnityEngine.UI;

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
                    maxHP = GameManager.Inst.PlayerInfo.currMaxHP;
                    currHP = GameManager.Inst.PlayerInfo.currHP;

                    maxMP = GameManager.Inst.PlayerInfo.currMaxMP;
                    currMP = GameManager.Inst.PlayerInfo.currMP;

                    def = GameManager.Inst.PlayerInfo.def;
                    basicDamage = (playerInfo.Str * 3) + (playerInfo.Dex * 2); // ���ݷ� = �� * 3 + �� * 2
                    cri = GameManager.Inst.PlayerInfo.cri;
                    Debug.Log("�÷��̾� ���ݷ���? : " + (int)basicDamage);
                    Debug.Log("�÷��̾��� ü����? : " + (int)currHP);
                    Debug.Log("�÷��̾��� ������? : " + (int)currMP);
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
            transform.position = bsm.playerLeftPos;
            //transform.position = new Vector3(-8f, -2.5f, 0f);
        }
        else if(GameManager.Inst.PlayerInfo.charactorVec == "Right")
        {
            transform.position = bsm.playerRightPos;
            //transform.position = new Vector3(43f, -2.5f, 0f);
            transform.GetChild(0).localScale = LEFT;
            transformX = -1;
        }
    }

    protected CharStateUI charStateUI;

    protected Calculate calculate = new Calculate();

    [SerializeField]
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

    private float basicDamage;

    //[SerializeField]
    private float maxHP;
    //[SerializeField]
    private float currHP;
    private float def;
    protected float cri;
    public float _MaxHp { get => maxHP; }
    public float _CurrHp { get => currHP; }

    public float maxMP;
    public float currMP;

    public float _MaxMP { get => maxMP; }
    public float _CurrMP { get => currMP; }

    private float defaultSpeed;
    private float moveSpeed;
    private Vector3 moveDirection = Vector3.zero;

    private float x;

    protected SpriteRenderer sr;

    public bool basicAttackCool;
    private bool BasicAttackCool
    {
        get { return basicAttackCool; }
    }

    public bool skill1AttackCool;
    public bool skill2BuffCool;

    public bool isGround;
    public bool isTrap;
    private bool jumpAttack;

    [SerializeField]
    private List<MonsterManager> msm = new List<MonsterManager>();

    private Image skill1Icon;
    private Image buffIcon;

    /*void Awake()
    {
        *//*charStateUI = GameObject.Find("MainChar (1)").GetComponent<CharStateUI>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GameObject.Find("GroundCheck").transform;
        jumpCheck = GameObject.Find("JumpCheck").transform;
        controllerManager = GetComponentInParent<ControllerManager>();
        sr = GetComponent<SpriteRenderer>();*//*

        CharInit();
    }*/

    public void InitState(bool controller)
    {
        if(controller)
            InitState(this, FSMStateEx.Instance);
        else
            InitState(this, PlayerAI.Instance);
    }

    Color takeA = new Color(1, 1, 1, 0);
    Color fullA = new Color(1, 1, 1, 1);

    Color buffColor = new Color(1f, 0.5f, 0.5f, 1f);

    private BattleSceneManager bsm;
        
    private void Awake()
    {
        charStateUI = GameObject.Find("MainChar (1)").GetComponent<CharStateUI>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GameObject.Find("GroundCheck").transform;
        jumpCheck = GameObject.Find("JumpCheck").transform;
        frontBackCheck = GameObject.Find("FrontBackCheck").transform;
        controllerManager = GetComponentInParent<ControllerManager>();
        sr = GetComponentInChildren<SpriteRenderer>();
        bsm = GameObject.Find("BattleSceneManager").GetComponent<BattleSceneManager>();

        skill1Icon = GameObject.Find("Skill1Icon").GetComponent<Image>();
        buffIcon = GameObject.Find("Skill2Icon").GetComponent<Image>();
        CharInit();
    }

    public void CharInit()
    {
        PlayerInfo();
        charStateUI.Init(this);
        defaultSpeed = moveSpeed = 3f;
        basicAttackCool = true;
        skill1AttackCool = true;
        skill2BuffCool = true;
        jumpPower = 6;
    }

    // Update is called once per frame
    void Update()
    {
        FSMUpdate();
    }

    private Vector3 RIGHT = new Vector3(1f, 1f, 1f); // 0.55f, 0.55f, 1�̿���
    private Vector3 LEFT = new Vector3(-1f, 1f, 1f);

    protected Vector2 direction;
    public Rigidbody2D rb;
    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    LayerMask trabLayer;

    protected Vector2 jump;

    [SerializeField]
    float jumpPower;

    private bool canDash = true;
    private bool isDashing = false;
    private float dashingPower = 10f;
    private float dashingTime = 0.2f;
    private float dashingCoolDown = 1f;

    [SerializeField]
    protected Transform groundCheck;
    [SerializeField]
    protected Transform jumpCheck;
    [SerializeField]
    protected Transform frontBackCheck;

    private int jumpCount;

    private bool isFrontBack;

    public void ChangeMode()
    {
        if(controllMode)
            ChangeState(FSMStateEx.Instance);
        else
            ChangeState(PlayerAI.Instance);
    }

    private float transformX = 1f;

    private bool isMove = true;

    public bool isAttack = false;

    [SerializeField]
    private Vector2 frontbackVec = Vector2.one;

    public void Movement()
    {
        if(isMove && !onDie)
        {
            if (isDashing)
            {
                return;
            }

            //direction.x = x * moveSpeed;
            if(!isFrontBack)
            {
                x = Input.GetAxisRaw("Horizontal");
                direction.x = x * moveSpeed;
                direction.y = rb.velocity.y;
                rb.velocity = direction;
            }

            anim.SetBool("Run", direction != Vector2.zero);
            if (anim.GetBool("Run") && isGround)
                SoundManager.Inst.PlayerRun("Walk_1.4");

            if (Input.GetKeyDown(KeyCode.Space) && canDash)
            {
                StartCoroutine(Dash(transformX));
            }

            isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
            //isJump = Physics2D.OverlapCircle(jumpCheck.position, 0.1f, groundLayer);
            isTrap = Physics2D.OverlapCircle(groundCheck.position, 0.1f, trabLayer);

            isFrontBack = Physics2D.OverlapCircle(frontBackCheck.position, 0.6f, groundLayer);



            if (Input.GetKeyDown(KeyCode.C))
            {
                if(jumpCount > 1)
                {
                    jumpCount--;
                    SoundManager.Inst.PlaySFX("Warrior_Jump");
                    jump = Vector2.up * jumpPower;
                    rb.velocity = jump;
                }
                /*if (isGround)
                {
                    SoundManager.Inst.PlaySFX("Warrior_Jump");
                    jump = Vector2.up * jumpPower;
                    rb.velocity = jump;
                }*/
            }

            if(!jumpAttack)
            {
                if (!isGround && !onDie && !isDashing && !isTrap)
                    anim.SetBool("Jump", true);
                else
                    anim.SetBool("Jump", false);
            }

            if (isGround && jumpCount < 2)
            {
                jumpCount = 2;
            }

            if(!isGround)
            {
                if(isFrontBack)
                {
                    rb.velocity = Vector2.up * -1f;
                }
            }

            // ���� �޸��� �ߴ� ���
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

            // ���� ���
            /*if (x > 0 && transform.GetChild(0).localScale != RIGHT)
               {
                   transform.GetChild(0).localScale = RIGHT;
                   transformX = 1f;
               }
               else if (x < 0 && transform.GetChild(0).localScale != LEFT)
               {
                   transform.GetChild(0).localScale = LEFT;
                   transformX = -1f;
               }*/


            //Debug.Log(x + " ���� ���� ��������? : " + transform.GetChild(0).localScale);

        }

        IEnumerator Dash(float vec)
        {
            anim.SetTrigger("Dash");
            SoundManager.Inst.PlaySFX("Warrior_Dash");
            canDash = false;
            isDashing = true;
            float originalGrabity = rb.gravityScale;
            rb.gravityScale = 0f;
            if (vec > 0)
                rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
            else
                rb.velocity = new Vector2(transform.localScale.x * -dashingPower, 0f);
            yield return YieldInstructionCache.WaitForSeconds(dashingTime);
            rb.gravityScale = originalGrabity;
            isDashing = false;
            yield return YieldInstructionCache.WaitForSeconds(dashingCoolDown);
            canDash = true;
        }
    }

    public void BasicAttackAnim()
    {
        isMove = false;
        basicAttackCool = false;
        anim.SetTrigger("BasicAttack");
        StartCoroutine(CoolIsAttack(1f));
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
        StartCoroutine(CoolIsAttack(1f));
        StartCoroutine(CoolTimeMove(0.8f));
        StartCoroutine(CoolTimeJumpAttack(1.0f));
    }

    public void Skill1AttackAnim()
    {
        isMove = false;
        skill1AttackCool = false;
        skill1Icon.fillAmount = 0;
        anim.SetTrigger("Skill1Attack");
        StartCoroutine(CoolIsAttack(1f));
        StartCoroutine(CoolTimeMove(0.8f));
        StartCoroutine(CoolTimeSkill1(12.0f));
        MpUse(18);
    }

    public bool isBuff;

    

    public void Skill2Buff()
    {
        isBuff = true;
        skill2BuffCool = false;
        buffIcon.fillAmount = 0;
        StartCoroutine(Skill2());
        StartCoroutine(CoolTimeSkill2(60f));
        MpUse(60);
    }

    public float tempDamage;
    public float tempDef;
    public float tempSpeed;

    IEnumerator Skill2()
    {
        sr.color = buffColor;
        // ���� ���� ����
        tempDamage = basicDamage;
        tempDef = def;
        tempSpeed = moveSpeed;

        // ���� ����
        basicDamage = basicDamage * 1.3f;
        def = def * 1.15f;
        moveSpeed = moveSpeed * 1.15f; // 1.15
        yield return YieldInstructionCache.WaitForSeconds(30f);

        // ���� �ǵ�����
        basicDamage = tempDamage;
        def = tempDef;
        moveSpeed = tempSpeed;

        sr.color = fullA;
        isBuff = false;
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

    IEnumerator CoolTimeSkill1(float cool)
    {
        //yield return YieldInstructionCache.WaitForSeconds(0.3f);
        SoundManager.Inst.PlaySFX("Warrior_BasicAttack");
        while (skill1Icon.fillAmount < 1f)
        {
            skill1Icon.fillAmount += 1 * Time.smoothDeltaTime / cool;
            yield return null;
        }

       /* yield return YieldInstructionCache.WaitForSeconds(0.3f);
        SoundManager.Inst.PlaySFX("Warrior_BasicAttack");
        yield return YieldInstructionCache.WaitForSeconds(cool - 0.3f);*/
        skill1AttackCool = true;
    }

    IEnumerator CoolTimeSkill2(float cool)
    {
        while (buffIcon.fillAmount < 1f)
        {
            buffIcon.fillAmount += 1 * Time.smoothDeltaTime / cool;
            yield return null;
        }
        //SoundManager.Inst.PlaySFX("Warrior_BasicAttack");
        yield return YieldInstructionCache.WaitForSeconds(cool);
        skill2BuffCool = true;
    }

    IEnumerator CoolIsAttack(float cool)
    {
        isAttack = true;
        yield return YieldInstructionCache.WaitForSeconds(1f);
        isAttack = false;
    }

    private int attackDamage;

    public int DamageCalculate(float value)
    {
        if (calculate.Critical(50))
        {
            attackDamage = (int)(calculate.CriticalDamage(basicDamage, 1.5f) * value);
        }
        else
            attackDamage = (int)(basicDamage * value);

        Debug.Log(basicDamage+" "+ attackDamage);
        return attackDamage;
    }

    private bool onDie = false;

    public void TakeDamage(float damage, Vector2 pos)
    {
        currHP -= calculate.TakeDamage(damage, def);
        GameManager.Inst.CharHPInit(currHP);
        charStateUI.UseHP();
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
            if (pos != null)
            {
                float x = transform.position.x - pos.x;
                if (x < 0)
                    x = 1;
                else
                    x = -1;
                StartCoroutine(Knockback(x));
            }
            StartCoroutine(TakeAlpha());
            
            Debug.Log(transform.name + " �÷��̾� ü�� : " + currHP);
        }
    }

    public void MpUse(float value)
    {
        currMP -= value;
        GameManager.Inst.CharMPInit(currMP);
        charStateUI.UseMP();
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
                Debug.Log("����");
            }
            else
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime * -1f * dir);
                
                Debug.Log("������");
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
