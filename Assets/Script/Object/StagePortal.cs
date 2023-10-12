using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;

public class StagePortal : InteractObject
{
    [SerializeField]
    private string nextMap;

    private ControllerManager controllerManager;

    private void Awake()
    {
        controllerManager = GameObject.Find("PlayerParty").GetComponent<ControllerManager>();
    }

    public override void Interaction()
    {
        PlayerInfoSet();
        GameManager.Inst.AsyncLoadNextScene(nextMap);
    }

    private void PlayerInfoSet()
    {
        controllerManager.ControllerInfoSet();
    }
}
