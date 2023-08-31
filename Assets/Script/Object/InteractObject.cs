using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractObject : MonoBehaviour
{
    protected bool interactable = false;
    protected Controller curTarget;

    public abstract void Interaction();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        Controller player = collision.GetComponentInParent<Controller>();
        if (player == null || player.CurState != FSMStateEx.Instance)
            return;

        curTarget = player;
        interactable = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        Controller player = collision.GetComponentInParent<Controller>();
        if (player == null || player.CurState != FSMStateEx.Instance)
            return;

        curTarget = null;
        interactable = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (curTarget == null)
            return;

        if (curTarget.CurState != FSMStateEx.Instance)
        {
            curTarget = null;
            interactable = false;
        }
    }

    private void InteractCheck()
    {
        if (!interactable)
            return;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Interaction();
            interactable = false;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        InteractCheck();
    }
}
