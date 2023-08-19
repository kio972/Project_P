using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonController : FSM<MonController>
{
    Transform targetTrans;
    Rigidbody2D rb;

    private bool target = false;
    public bool Target
    {
        set { target = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        InitState(this, MonState.Instance);
    }

    private void Awake()
    {
        targetTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void MonInit()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FSMUpdate();
    }

    public void TargetFollow()
    {
        if (Vector2.Distance(transform.position, targetTrans.position) > 1f && target)
            transform.position = Vector2.MoveTowards(transform.position, targetTrans.position, 4f * Time.deltaTime);
        else
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(7f,-1.745f), 2f * Time.deltaTime);
    }
}
