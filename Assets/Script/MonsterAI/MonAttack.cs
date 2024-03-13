using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonAttack : FSMSingleton<MonAttack>, CharState<MonController>
{
    public void Enter(MonController e)
    {
        e.AnimRun(false);
        Debug.Log("MonAttack µé¾î¿È!");
    }

    public void Excute(MonController e)
    {
        e.Attack();
    }

    public void Exit(MonController e)
    {
        //e.AnimRun(true);
        Debug.Log("MonAttack ³ª°¨!");
    }

    
}
