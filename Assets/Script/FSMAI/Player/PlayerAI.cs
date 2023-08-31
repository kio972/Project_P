using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : FSMSingleton<PlayerAI>, CharState<Controller>
{
    public Transform controllPlayer;

    public void Enter(Controller e)
    {
        //controllPlayer = e.controllerManager.TargetController();
        e.rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        e.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        //Debug.Log(e.transform.name + "플레이어 캐릭터가 AI에 들어왔습니다. 현재 타겟 플레이어는 : " + controllPlayer.name);
    }

    public void Excute(Controller e)
    {
        controllPlayer = e.controllerManager.TargetController();

        if (controllPlayer != null)
        {
            e.LookAtPlayer(controllPlayer);
            if (Vector2.Distance(e.transform.position, controllPlayer.position) > 1f && !e.Same)
            {
                if (e.anim.GetBool("Run") == false)
                    e.anim.SetBool("Run", true);
                e.transform.position = Vector2.MoveTowards(e.transform.position, controllPlayer.position, 4 * Time.deltaTime);
                //LookAtDir();
            }
            else if (e.Same)
            {
                if (e.anim.GetBool("Run") == true)
                    e.anim.SetBool("Run", false);
            }
        }
    }

    public void Exit(Controller e)
    {
        //controllPlayer = null;
        Debug.Log(e.transform.name + "플레이어 캐릭터가 AI에서 나갔습니다.");
    }

    

}
