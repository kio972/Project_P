using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMStateEx : FSMSingleton<FSMStateEx>, CharState<Controller>
{
    public void Enter(Controller e)
    {
        //�� ���¿� ���� ��� ȣ��Ǵ� �Լ�
    }

    public void Excute(Controller e)
    {
        //FSMUpdate�Լ��� ȣ��� ��� ����Ǵ� �Լ�
    }

    public void Exit(Controller e)
    {
        //�� ���¿��� �������� ��� ȣ��Ǵ� �Լ�
    }
}
