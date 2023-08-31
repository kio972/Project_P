using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonChase : FSMSingleton<MonChase>, CharState<MonController>
{
    public void Enter(MonController e)
    {
        e.AnimRun(true);
        Debug.Log("MonChase µé¾î¿È!");
    }

    public void Excute(MonController e)
    {
        e.Chase();
    }

    public void Exit(MonController e)
    {
        Debug.Log("MonChase ³ª°¨!");
    }
}
