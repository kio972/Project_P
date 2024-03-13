using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;

public class StagePortal : InteractObject
{
    [SerializeField]
    private int stage;

    [SerializeField]
    private string nextMap;

    [SerializeField]
    private string charVec;

    [SerializeField]
    private bool potalOpen;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        PotalOpen(potalOpen);
    }

    public void PotalOpen(bool open)
    {
        if (open)
        {
            potalOpen = true;
            anim.SetTrigger("Open");
        }
            
    }

    public override void Interaction()
    {
        if(potalOpen)
        {
            if(stage != 0)
                GameManager.Inst.StageClear(stage);
            SoundManager.Inst.PlaySFX("PotalWarp");
            GameManager.Inst.CharactorVec(charVec);
            GameManager.Inst.AsyncLoadNextScene(nextMap);
        }
    }
}
