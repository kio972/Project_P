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

    [SerializeField]
    private bool potalOpen;

    public void PortalOpen()
    {
        potalOpen = true;
    }

    public override void Interaction()
    {
        if(potalOpen)
        {
            GameManager.Inst.StageClear(3);
            SoundManager.Inst.PlaySFX("PotalWarp");

            GameManager.Inst.CalendarProd = true;
            GameManager.Inst.CalendarProdRegion = region;
            GameManager.Inst.CalendarProdCloud = stage;

            GameManager.Inst.AsyncLoadNextScene(SceneName.CalendarScene);
        }
    }

    private void SetHighLight(bool value)
    {
        if (highLightObject == null)
            return;

        highLightObject.SetActive(value);
    }

    protected override void Update()
    {
        if(potalOpen)
        {
            base.Update();
            SetHighLight(interactable);
        }
    }
}
