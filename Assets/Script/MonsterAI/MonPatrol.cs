using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonPatrol : FSMSingleton<MonPatrol>, CharState<MonController>
{
    public void Enter(MonController e)
    {
        //e.AnimRun(true);
        e.PatrolInit();
        //Debug.Log("MonPatrol ����!");
    }

    public void Excute(MonController e)
    {
        e.Patrol();
        //e.TargetFollow();
    }

    public void Exit(MonController e)
    {
        //Debug.Log("MonPatrol ����!");
    }
}
