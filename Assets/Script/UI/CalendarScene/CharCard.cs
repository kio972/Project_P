using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 pos = new Vector2(-350, 1040);

    Transform root;

    private void Start()
    {
        root = transform.root;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        root.transform.SendMessageUpwards("BeginDrag", transform, SendMessageOptions.DontRequireReceiver);
    }

    public void OnDrag(PointerEventData eventData)
    {
        root.transform.SendMessageUpwards("Drag", transform, SendMessageOptions.DontRequireReceiver);
        transform.position = eventData.position + pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        root.transform.SendMessageUpwards("EndDrag", transform, SendMessageOptions.DontRequireReceiver);
    }

}
