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

    protected void InitState(T owner, CharState<T> initState) // ������Ʈ �ʱ�ȭ
    {
        this.owner = owner;
        ChangeState(initState);
    }

    protected void FSMUpdate()
    {
        if (owner == null)
            return;

        if (curState != null)
            curState.Excute(owner); // ���¸� �����ϰ� ������ ȣ��ǰ��ִ� �Լ�
    }

    public void ChangeState(CharState<T> nextState) // ���¸� �����ϴ� �Լ�
    {
        if (owner == null)
            return;

        prevState = curState;
        if (prevState != null) 
            prevState.Exit(owner); // ������ ���¸� ������ ȣ���ϴ� �Լ�
        curState = nextState;
        if (curState != null)
            curState.Enter(owner); // ���ο� ���¿� ���ö� ȣ���ϴ� �Լ�
    }

    public void RevertState()
    {
        if (prevState != null)
            ChangeState(prevState);
    }
}