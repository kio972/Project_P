using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;

public class StagePortal : InteractObject
{
    [SerializeField]
    private SceneName nextMap;

    public override void Interaction()
    {
        GameManager.Inst.AsyncLoadNextScene(nextMap);
    }
}
