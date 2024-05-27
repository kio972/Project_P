using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool_Label : MonoBehaviour
{
    protected ObjectPool pool; // ���� �������ִ� ������(pool)�� ����ϱ� ���� ����. 


    // ������Ʈ ���� 
    public virtual void Create(ObjectPool objectPool)
    {
        pool = objectPool; // ������ ����.
        gameObject.SetActive(false); // ��Ȱ�� ���·� ����. 
    }

    public virtual void InitInfo()  // ���� �Լ� : ��ӹ��� �ڽĿ��� �����Ǹ� �Ҽ� �ֵ���. 
    {

    }

    // ������Ʈ Ǯ�� ������Ʈ�� ����
    public virtual void Pop()
    {
        InitInfo();
    }


    // ������Ʈ Ǯ�� ��ȯ
    public virtual void Push()
    {
        pool.PushObj(this);
    }
}
