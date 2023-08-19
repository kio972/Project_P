using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonState : FSMSingleton<MonState>, CharState<MonController>
{

    public void Enter(MonController e)
    {
        e.MonInit();
    }

    public void Excute(MonController e)
    {
        e.TargetFollow();
    }

    public void Exit(MonController e)
    {
        throw new System.NotImplementedException();
    }

}
