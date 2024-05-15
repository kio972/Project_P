using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonIdle : FSMSingleton<MonIdle>, CharState<MonController>
{

    public void Enter(MonController e)
    {
        //Debug.Log("MonIdle µé¾î¿È!");
    }

    public void Excute(MonController e)
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            e.PatrolState();
        }
        //e.TargetFollow();
    }

    public void Exit(MonController e)
    {
        //Debug.Log("MonIdle ³ª°¨!");
    }

}
