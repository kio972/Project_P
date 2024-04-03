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
        if (Input.GetKeyDown(KeyCode.X) && e.basicAttackCool) // && e.isGround
        {
            if (e.isGround)
                e.BasicAttackAnim(); // ����
            else
                e.JumpAttackAnim(); // ���� ����

        }
    }

    public void Exit(Controller e) //�� ���¿��� �������� ��� ȣ��Ǵ� �Լ�
    {
        Debug.Log(e.transform.name + "Exit �Լ�");
    }
}
