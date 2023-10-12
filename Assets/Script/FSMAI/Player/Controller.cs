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
                    maxHP = playerInfo.HP;
                    currHP = GameManager.Inst.PlayerInfo.warrior_HP;

                    break;
                }
            case PlayerType.Priest:
                {
                    PriestInfo playerInfo = new PriestInfo();
                    maxHP = playerInfo.HP;
                    currHP = GameManager.Inst.PlayerInfo.priest_HP;

                    break;
                }
            case PlayerType.Archer:
                {
                    ArcherInfo playerInfo = new ArcherInfo();
                    maxHP = playerInfo.HP;
                    currHP = GameManager.Inst.PlayerInfo.archer_HP;

                    break;
                }
        }
    }

    public void PlayerInfoSave()
    {
        
        switch (playerType)
        {
            case PlayerType.Warrior:
                {
                    GameManager.Inst.PlayerInfo.warrior_HP = currHP;
                    break;
                }
            case PlayerType.Priest:
                {
                    GameManager.Inst.PlayerInfo.priest_HP = currHP;
                    break;
                }
            case PlayerType.Archer:
                {
                    GameManager.Inst.PlayerInfo.archer_HP = currHP;
                    break;
                }
        }
    }

    public void ControllerSetting()
    {
        if (controllMode)
        {
            switch (playerType)
            {
                case PlayerType.Warrior:
                    {
                        GameManager.Inst.PlayerInfo.controller = "Warrior";
                        break;
                    }
                case PlayerType.Priest:
                    {
                        GameManager.Inst.PlayerInfo.controller = "Priest";
                        break;
                    }
                case PlayerType.Archer:
                    {
                        GameManager.Inst.PlayerInfo.controller = "Archer";
                        break;
                    }
            }
        }
        else
            return;
    }

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

    [SerializeField]
    private float maxHP;
    [SerializeField]
    private float currHP;
    public float _MaxHp { get => maxHP; }
    public float _CurrHp { get => currHP; }

    private float moveSpeed;
    private Vector3 moveDirection = Vector3.zero;

    private float x;

    public bool basicAttackCool;
    private bool BasicAttackCool
    {
        get { return basicAttackCool; }
    }

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentInChildren<GroundCheckGizmos>().transform;
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
        PlayerInfo();
        moveSpeed = 5f;
        basicAttackCool = true;
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
            Debug.Log("점프" + isGround);
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
        StartCoroutine(CoolTimeBasic(1.0f));
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
