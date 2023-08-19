using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : FSM<Controller>
{
    private float moveSpeed = 5;
    private Vector3 moveDirection = Vector3.zero;

    float x;
    float y;

    // Start is called before the first frame update
    void Start()
    {
        InitState(this, FSMStateEx.Instance);
    }

    // Update is called once per frame
    void Update()
    {
        FSMUpdate();
    }

    public void Movement()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(x, y, 0);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void Attack()
    {
        Debug.Log("АјАн!!"); 
    }
}
