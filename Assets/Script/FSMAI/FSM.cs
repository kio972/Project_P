using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CharState<T>
{
    void Enter(T e);

    void Excute(T e);

    void Exit(T e);
}

public class FSM<T> : MonoBehaviour
{
    private T owner;
    private CharState<T> curState;
    private CharState<T> prevState;

    public CharState<T> CurState { get { return curState; } }
    public CharState<T> PrevState { get { return prevState; } }

    protected void InitState(T owner, CharState<T> initState) // 스테이트 초기화
    {
        this.owner = owner;
        ChangeState(initState);
    }

    protected void FSMUpdate()
    {
        if (owner == null)
            return;

        if (curState != null)
            curState.Excute(owner); // 상태를 유지하고 있을때 호출되고있는 함수
    }

    public void ChangeState(CharState<T> nextState) // 상태를 변경하는 함수
    {
        if (owner == null)
            return;

        prevState = curState;
        if (prevState != null) 
            prevState.Exit(owner); // 기존의 상태를 나갈때 호출하는 함수
        curState = nextState;
        if (curState != null)
            curState.Enter(owner); // 새로운 상태에 들어올때 호출하는 함수
    }

    public void RevertState()
    {
        if (prevState != null)
            ChangeState(prevState);
    }
}