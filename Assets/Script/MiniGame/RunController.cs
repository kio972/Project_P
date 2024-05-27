using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rig;
    private bool isGround;

    private bool game;

    [SerializeField]
    private float jumpPower = 3f;



    private void Awake()
    {
        isGround = true;
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("isJump", false);
    }

    // Update is called once per frame
    void Update()
    {
        game = RunManager.Inst.Start;
        if(isGround)
        {
            anim.SetBool("isJump", false);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)&&isGround&&game)
        {
            isGround = false;
            anim.SetBool("isJump", true);
            rig.AddForce(Vector2.up * jumpPower,ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }


}
