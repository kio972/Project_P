using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace YeongJun 
{
    public class Pocket : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        public Inventory inventory;
        public int pocketNumber; // 주머니 번호
        [SerializeField] private Image itemImg; // 아이템 이미지
        [SerializeField] private TextMeshProUGUI countText; // 아이템 개수

        public void Refresh()
        {// 주머니 새로고침
            switch (pocketNumber)
            {
                case 0:
                    if (InvenData.pocket[0] != null)
                    {
                        if (InvenData.pocket[0].count <= 0)
                        {
                            InvenData.pocket[0] = null;
                            itemImg.gameObject.SetActive(false);
                            countText.gameObject.SetActive(false);
                            return;
                        }
                        itemImg.gameObject.SetActive(true);
                        itemImg.sprite = InvenData.pocket[0].iconImg;
                    }
                    else
                    {
                        itemImg.gameObject.SetActive(false);
                        countText.gameObject.SetActive(false);
                    }
                    break;
                case 1:
                    if (InvenData.pocket[1] != null)
                    {
                        if (InvenData.pocket[1].count <= 0)
                        {
                            InvenData.pocket[1] = null;
                            itemImg.gameObject.SetActive(false);
                            countText.gameObject.SetActive(false);
                            return;
                        }
                        itemImg.gameObject.SetActive(true);
                        itemImg.sprite = InvenData.pocket[1].iconImg;
                    }
                    else
                    {
                        itemImg.gameObject.SetActive(false);
                        countText.gameObject.SetActive(false);
                    }
                    break;
                default:
                    break;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (inventory.state == InvenState.main || inventory.state == InvenState.equip)
            {
                inventory.pocketNum = pocketNumber;
                inventory.EnterPocket(transform);
                switch (pocketNumber)
                {
                    case 0:
                        inventory.pocketItem = InvenData.pocket[0];
                        break;
                    case 1:
                        inventory.pocketItem = InvenData.pocket[1];
                        break;
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (inventory.state == InvenState.main)
                inventory.EquipPotionMenu();
            else if (inventory.state == InvenState.equip)
                inventory.SelectPocket(pocketNumber);
        }
    }
}
