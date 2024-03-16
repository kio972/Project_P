using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{

    private Animator anim;
    float   moveSpeed = 1f;
    Vector3 moveDir = Vector3.zero;

    public void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        move();
    }

    public void move()
    {
        moveDir.x = Input.GetAxis("Horizontal");
        moveDir.z = Input.GetAxis("Vertical");
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        moveDir.Normalize();
        transform.LookAt(transform.position + moveDir);
        anim.SetBool("Walk", moveDir != Vector3.zero);

        if(anim.GetBool("Walk"))
            SoundManager.Inst.PlayerRun("Walk_1.4");

        Debug.Log("달리는 중");
    }
}
