using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool_Label : MonoBehaviour
{
    protected ObjectPool pool; // 나를 관리해주는 관리자(pool)를 기억하기 위한 참조. 


    // 오브젝트 생성 
    public virtual void Create(ObjectPool objectPool)
    {
        pool = objectPool; // 관리자 지정.
        gameObject.SetActive(false); // 비활성 상태로 관리. 
    }

    public virtual void InitInfo()  // 가상 함수 : 상속받은 자식에서 재정의를 할수 있도록. 
    {

    }

    // 오브젝트 풀이 오브젝트를 꺼냄
    public virtual void Pop()
    {
        InitInfo();
    }


    // 오브젝트 풀에 반환
    public virtual void Push()
    {
        pool.PushObj(this);
    }
}
