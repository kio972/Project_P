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

<<<<<<< HEAD
    private ControllerManager controllerManager;

    private void Awake()
    {
        controllerManager = GameObject.Find("PlayerParty").GetComponent<ControllerManager>();
=======
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
            
>>>>>>> Jun
    }

    public override void Interaction()
    {
<<<<<<< HEAD
        PlayerInfoSet();
        GameManager.Inst.AsyncLoadNextScene(nextMap);
=======
        if(potalOpen)
        {
            if(stage != 0)
                GameManager.Inst.StageClear(stage);
            SoundManager.Inst.PlaySFX("PotalWarp");
            GameManager.Inst.CharactorVec(charVec);
            GameManager.Inst.AsyncLoadNextScene(nextMap);
        }
>>>>>>> Jun
    }

    private void PlayerInfoSet()
    {
        controllerManager.ControllerInfoSet();
    }
}
