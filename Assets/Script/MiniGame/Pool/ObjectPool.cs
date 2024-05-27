using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject poolObj; // �ش� Pool���� �������� ��� ������Ʈ
    [SerializeField]
    private int allocateCount; // ������Ʈ�� �� ���� �������� ���ϴ� ����

    private Stack<ObjectPool_Label> poolStack = new Stack<ObjectPool_Label>();
    private int objMaxCount; // ���� Ǯ���� �����ϰ� �ִ� ������Ʈ�� ����. 
    private int objActiveCount; // Ȱ��ȭ�Ǿ� ���ǰ� �ִ� ������Ʈ�� ����. 

    private void Awake()
    {
        objMaxCount = 0;
        objActiveCount = 0;
        Allocate(); // �ʱ⿡ ������Ʈ�� �̸� ����. 
    }


    private GameObject obj;
    // Ǯ�� ������Ʈ�� �������ִ� �Լ�. 
    public void Allocate()
    {
        for (int i = 0; i < allocateCount; i++)
        {
            obj = Instantiate(poolObj, this.transform);
            obj.GetComponent<ObjectPool_Label>().Create(this);   // ������ ����. 
            poolStack.Push(obj.GetComponent<ObjectPool_Label>());
            objMaxCount++; // Ǯ�� ������ ������Ʈ ������ ����. 
        }
    }


    // Ǯ���� ������Ʈ�� �������� �Լ�. 
    public GameObject PopObj()
    {
        if (objActiveCount >= objMaxCount) // �� �̻� �����ټ� �ִ� ������Ʈ�� ������, 
        {
            Allocate(); // �߰��� ������Ʈ�� ����. 
        }

        obj = poolStack.Pop().gameObject;
        obj.SetActive(true);
        obj.GetComponent<ObjectPool_Label>().Pop();
        objActiveCount++;
        return obj;
    }


    // Ǯ�� ������Ʈ�� ��ȯ
    public void PushObj(ObjectPool_Label label)
    {
        if (label.gameObject.activeSelf)
        {
            label.gameObject.SetActive(false); // ������Ʈ �� Ȱ��ȭ. 
            poolStack.Push(label);
            objActiveCount--;
        }
    }
}
