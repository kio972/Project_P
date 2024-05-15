using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMStateEx : FSMSingleton<FSMStateEx>, CharState<Controller>
{
    public void Enter(Controller e) //현 상태에 들어올 경우 호출되는 함수
    {
        Debug.Log(e.transform.name + "Enter 함수");
         
    }

    public void Excute(Controller e) //FSMUpdate함수가 호출될 경우 실행되는 함수
    {
        e.Movement(); // 이동
        if (Input.GetKeyDown(KeyCode.X) && e.basicAttackCool && !e.isAttack) // && e.isGround
        {
            if (e.isGround)
                e.BasicAttackAnim(); // 공격
            else
                e.JumpAttackAnim(); // 점프 공격
        }
        if (Input.GetKeyDown(KeyCode.Z) && e.skill1AttackCool && e.isGround && !e.isAttack && e.currMP >= 18)
        {
            e.Skill1AttackAnim(); // 스킬 공격
        }
        if (Input.GetKeyDown(KeyCode.A) && e.skill2BuffCool && e.isGround && !e.isAttack && !e.isBuff && e.currMP >= 60)
        {
            e.Skill2Buff(); // 버프
        }
    }

    public void Exit(Controller e) //현 상태에서 빠져나갈 경우 호출되는 함수
    {
        Debug.Log(e.transform.name + "Exit 함수");
    }
}
