using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject poolObj; // 해당 Pool에서 관리해줄 대상 오브젝트
    [SerializeField]
    private int allocateCount; // 오브젝트를 몇 개나 생성할지 정하는 변수

    private Stack<ObjectPool_Label> poolStack = new Stack<ObjectPool_Label>();
    private int objMaxCount; // 현재 풀에서 관리하고 있는 오브젝트의 총합. 
    private int objActiveCount; // 활성화되어 사용되고 있는 오브젝트의 갯수. 

    private void Awake()
    {
        objMaxCount = 0;
        objActiveCount = 0;
        Allocate(); // 초기에 오브젝트를 미리 생성. 
    }


    private GameObject obj;
    // 풀에 오브젝트를 생성해주는 함수. 
    public void Allocate()
    {
        for (int i = 0; i < allocateCount; i++)
        {
            obj = Instantiate(poolObj, this.transform);
            obj.GetComponent<ObjectPool_Label>().Create(this);   // 관리자 설정. 
            poolStack.Push(obj.GetComponent<ObjectPool_Label>());
            objMaxCount++; // 풀에 생성된 오브젝트 총합을 증가. 
        }
    }


    // 풀에서 오브젝트를 꺼내가는 함수. 
    public GameObject PopObj()
    {
        if (objActiveCount >= objMaxCount) // 더 이상 꺼내줄수 있는 오브젝트가 없을때, 
        {
            Allocate(); // 추가로 오브젝트를 생성. 
        }

        obj = poolStack.Pop().gameObject;
        obj.SetActive(true);
        obj.GetComponent<ObjectPool_Label>().Pop();
        objActiveCount++;
        return obj;
    }


    // 풀에 오브젝트를 반환
    public void PushObj(ObjectPool_Label label)
    {
        if (label.gameObject.activeSelf)
        {
            label.gameObject.SetActive(false); // 오브젝트 비 활성화. 
            poolStack.Push(label);
            objActiveCount--;
        }
    }
}
