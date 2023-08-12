using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : FSM<Controller>
{
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
}
