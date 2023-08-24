using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace YeongJun
{
    public class Portrait : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        public Inventory inventory;
        public int charNumber; // 캐릭터 번호

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (inventory.state == InvenState.use)
                inventory.EnterPortrait(transform);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            inventory.SelectPortrait(charNumber);
        }
    }
}
