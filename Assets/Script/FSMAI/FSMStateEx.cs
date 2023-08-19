using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMStateEx : FSMSingleton<FSMStateEx>, CharState<Controller>
{
    public void Enter(Controller e) //�� ���¿� ���� ��� ȣ��Ǵ� �Լ�
    {
        Debug.Log("Enter �Լ�");
         
    }

    public void Excute(Controller e) //FSMUpdate�Լ��� ȣ��� ��� ����Ǵ� �Լ�
    {
        e.Movement(); // �̵�
        if(Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("K");
            e.Attack(); // ����
        }
    }

    public void Exit(Controller e) //�� ���¿��� �������� ��� ȣ��Ǵ� �Լ�
    {
        Debug.Log("Exit �Լ�");
    }
}
