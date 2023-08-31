using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;

public class Revealer : InteractObject
{
    [SerializeField]
    private GameObject highLightObject;

    public override void Interaction()
    {
        GameManager.Inst.AsyncLoadNextScene(SceneName.CalendarScene);
    }

    private void SetHighLight(bool value)
    {
        if (highLightObject == null)
            return;

        highLightObject.SetActive(value);
    }

    protected override void Update()
    {
        base.Update();
        SetHighLight(interactable);
    }
}
