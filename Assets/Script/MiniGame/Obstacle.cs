using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : ObjectPool_Label
{

    private BoxCollider2D col;
    private Movement movement;
    private GameObject obj;

    private void Awake()
    {
        if (!TryGetComponent<BoxCollider2D>(out col))
        {
            col.isTrigger = true;
        }
    }

    public override void InitInfo()
    {
        base.InitInfo();
        if (!TryGetComponent<Movement>(out movement))
        {
            Debug.Log("EnemyChar.cs - InitInfo() - movement ���� ����");
        }
        else
        {
            movement.InitMovement(Vector3.left);
        }
    }

    // �÷��̾�� ������ �ο��ϴ� �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RunManager.Inst.TakeDamage(RunManager.Inst.HP);
            Debug.Log(RunManager.Inst.HP);
            Push();
            if(RunManager.Inst.HP < 0)
            {
                RunManager.Inst.Start = false;
                RunManager.Inst.GameOver();
            }
        }
        if (collision.CompareTag("Destroy"))
        {
            Debug.Log("�ı�");
            Push();
        }
    }
}
