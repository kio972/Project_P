using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonAttack : FSMSingleton<MonAttack>, CharState<MonController>
{
    // 공격 상태에 들어왔을 때
    public void Enter(MonController e) 
    {
        e.AnimRun(false); // 움직임 멈추기.
    }

    // 공격 상태중일 때
    public void Excute(MonController e)
    {
        e.Attack();
    }

    // 공격 상태에서 나갈 때
    public void Exit(MonController e)
    {
<<<<<<< HEAD
        e.AnimRun(true); // 다시 움직이기.
=======
        //e.AnimRun(true);
        Debug.Log("MonAttack 나감!");
>>>>>>> Jun
    }

    
}
