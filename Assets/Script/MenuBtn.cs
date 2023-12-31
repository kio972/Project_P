using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YeongJun
{
    public class MenuBtn : MonoBehaviour, IPointerEnterHandler
    {
        public Inventory inventory;
        public void Refresh()
        {
            if (GetComponent<Button>().interactable)
                transform.GetChild(0).GetComponent<Text>().color = Color.black;
            else
                transform.GetChild(0).GetComponent<Text>().color = Color.red;
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (inventory.state == InvenState.control)
                inventory.EnterMenu(transform);
        }
    }
}
