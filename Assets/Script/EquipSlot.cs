using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace YeongJun
{
    public class EquipSlot : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        public Inventory inventory;
        public int equipNumber; // 번호
        [SerializeField] private Image itemImg; // 아이템 이미지


        public void Refresh()
        {// 주머니 새로고침
            switch (equipNumber)
            {
                case 0:
                    if (InvenData.equip[0] != null)
                    {
                        if (InvenData.equip[0].count <= 0)
                        {
                            InvenData.equip[0] = null;
                            itemImg.gameObject.SetActive(false);
                            return;
                        }
                        itemImg.gameObject.SetActive(true);
                        itemImg.sprite = InvenData.equip[0].iconImg;
                    }
                    else
                        itemImg.gameObject.SetActive(false);
                    break;
                case 1:
                    if (InvenData.equip[1] != null)
                    {
                        if (InvenData.equip[1].count <= 0)
                        {
                            InvenData.equip[1] = null;
                            itemImg.gameObject.SetActive(false);
                            return;
                        }
                        itemImg.gameObject.SetActive(true);
                        itemImg.sprite = InvenData.equip[1].iconImg;
                    }
                    else
                        itemImg.gameObject.SetActive(false);
                    break;
                case 2:
                    if (InvenData.equip[2] != null)
                    {
                        if (InvenData.equip[2].count <= 0)
                        {
                            InvenData.equip[2] = null;
                            itemImg.gameObject.SetActive(false);
                            return;
                        }
                        itemImg.gameObject.SetActive(true);
                        itemImg.sprite = InvenData.equip[2].iconImg;
                    }
                    else
                        itemImg.gameObject.SetActive(false);
                    break;
                case 3:
                    if (InvenData.equip[3] != null)
                    {
                        if (InvenData.equip[3].count <= 0)
                        {
                            InvenData.equip[3] = null;
                            itemImg.gameObject.SetActive(false);
                            return;
                        }
                        itemImg.gameObject.SetActive(true);
                        itemImg.sprite = InvenData.equip[3].iconImg;
                    }
                    else
                        itemImg.gameObject.SetActive(false);
                    break;
                case 4:
                    if (InvenData.equip[4] != null)
                    {
                        if (InvenData.equip[4].count <= 0)
                        {
                            InvenData.equip[4] = null;
                            itemImg.gameObject.SetActive(false);
                            return;
                        }
                        itemImg.gameObject.SetActive(true);
                        itemImg.sprite = InvenData.equip[4].iconImg;
                    }
                    else
                        itemImg.gameObject.SetActive(false);
                    break;
                case 5:
                    if (InvenData.equip[5] != null)
                    {
                        if (InvenData.equip[5].count <= 0)
                        {
                            InvenData.equip[5] = null;
                            itemImg.gameObject.SetActive(false);
                            return;
                        }
                        itemImg.gameObject.SetActive(true);
                        itemImg.sprite = InvenData.equip[5].iconImg;
                    }
                    else
                        itemImg.gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (inventory.state == InvenState.main)
            {
                inventory.equipNum = equipNumber;
                inventory.EnterPocket(transform);
                if (InvenData.equip[equipNumber] != null)
                {
                    inventory.OpenItemInfo(InvenData.equip[equipNumber]);
                    switch (equipNumber)
                    {
                        case 0:
                            inventory.equipItem = InvenData.equip[0];
                            break;
                        case 1:
                            inventory.equipItem = InvenData.equip[1];
                            break;
                        case 2:
                            inventory.equipItem = InvenData.equip[2];
                            break;
                        case 3:
                            inventory.equipItem = InvenData.equip[3];
                            break;
                        case 4:
                            inventory.equipItem = InvenData.equip[4];
                            break;
                        case 5:
                            inventory.equipItem = InvenData.equip[5];
                            break;
                    }
                }
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            inventory.CloseItemInfo();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (inventory.state == InvenState.main)
                inventory.EquipmentMenu();
        }
    }

}