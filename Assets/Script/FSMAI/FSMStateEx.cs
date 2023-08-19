using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMStateEx : FSMSingleton<FSMStateEx>, CharState<Controller>
{
    public void Enter(Controller e) //현 상태에 들어올 경우 호출되는 함수
    {
        Debug.Log("Enter 함수");
         
    }

    public void Excute(Controller e) //FSMUpdate함수가 호출될 경우 실행되는 함수
    {
        e.Movement(); // 이동
        if(Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("K");
            e.Attack(); // 공격
        }
    }

    public void Exit(Controller e) //현 상태에서 빠져나갈 경우 호출되는 함수
    {
        Debug.Log("Exit 함수");
    }
}
