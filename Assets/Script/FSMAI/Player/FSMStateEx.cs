using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMStateEx : FSMSingleton<FSMStateEx>, CharState<Controller>
{
    public void Enter(Controller e) //�� ���¿� ���� ��� ȣ��Ǵ� �Լ�
    {
        Debug.Log(e.transform.name + "Enter �Լ�");
         
    }

    public void Excute(Controller e) //FSMUpdate�Լ��� ȣ��� ��� ����Ǵ� �Լ�
    {
        e.Movement(); // �̵�
        if (Input.GetKeyDown(KeyCode.X) && e.basicAttackCool && !e.isAttack) // && e.isGround
        {
            if (e.isGround)
                e.BasicAttackAnim(); // ����
            else
                e.JumpAttackAnim(); // ���� ����
        }
        if (Input.GetKeyDown(KeyCode.Z) && e.skill1AttackCool && e.isGround && !e.isAttack && e.currMP >= 18)
        {
            e.Skill1AttackAnim(); // ��ų ����
        }
        if (Input.GetKeyDown(KeyCode.A) && e.skill2BuffCool && e.isGround && !e.isAttack && !e.isBuff && e.currMP >= 60)
        {
            e.Skill2Buff(); // ����
        }
    }

    public void Exit(Controller e) //�� ���¿��� �������� ��� ȣ��Ǵ� �Լ�
    {
        Debug.Log(e.transform.name + "Exit �Լ�");
    }
}
