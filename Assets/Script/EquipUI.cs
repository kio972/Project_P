using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace YeongJun
{
    public class EquipUI : MonoBehaviour, IPointerEnterHandler
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
            if (inventory.state == InvenState.unequip)
                inventory.EnterEquipMenu(transform);
        }
    }
}