using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 pos = new Vector2(-350, 1040);

    Transform root;

    private bool init;
    

    private void Start()
    {
        root = transform.root;
    }

    public void CharCardInit(bool value)
    {
        if (value)
            init = true;
        else
            init = false;

        //Debug.Log(transform.name + " ���� : " + init);
    }

    public void OnBeginDrag(PointerEventData eventData) // ��������
    {
        if(init)
            root.transform.SendMessageUpwards("BeginDrag", transform, SendMessageOptions.DontRequireReceiver);
    }

    public void OnDrag(PointerEventData eventData) // ��� ������
    {
        if (init)
        {
            root.transform.SendMessageUpwards("Drag", transform, SendMessageOptions.DontRequireReceiver);
            transform.position = eventData.position + pos;
        }
    }

    public void OnEndDrag(PointerEventData eventData) // ���� ��
    {
        if(init)
            root.transform.SendMessageUpwards("EndDrag", transform, SendMessageOptions.DontRequireReceiver);
    }

}