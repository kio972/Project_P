using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;

public class Revealer : InteractObject
{
    [SerializeField]
    private GameObject highLightObject;
    [SerializeField]
    private int stage;
    [SerializeField]
    private int region;

    public override void Interaction()
    {
        GameManager.Inst.CalendarProd = true;
        GameManager.Inst.CalendarProdRegion = region;
        GameManager.Inst.CalendarProdCloud = stage;

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
