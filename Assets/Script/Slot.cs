using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace YeongJun
{
    public class Slot : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler, IPointerClickHandler
    {
        public Inventory inventory;
        public int slotNumber; // 슬롯 번호
        private Image itemImg; // 아이템 이미지
        private TextMeshProUGUI countText; // 아이템 개수

        private void Awake()
        {
            itemImg = transform.GetChild(0).GetComponent<Image>();
            countText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }

        
        public void Refresh()
        {// 슬롯 새로고침
            if (InvenData.item[slotNumber] != null)
            {
                if (InvenData.item[slotNumber].count <= 0)
                {
                    InvenData.item[slotNumber] = null;
                    itemImg.gameObject.SetActive(false);
                    countText.gameObject.SetActive(false);
                    return;
                }
                itemImg.gameObject.SetActive(true);
                countText.gameObject.SetActive(true);
                itemImg.sprite = InvenData.item[slotNumber].iconImg;
                if (InvenData.item[slotNumber].count <= 1)
                    countText.gameObject.SetActive(false);
                else
                    countText.text = InvenData.item[slotNumber].count.ToString();
            }
            else
            {
                itemImg.gameObject.SetActive(false);
                countText.gameObject.SetActive(false);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (inventory.state == InvenState.main)
            {
                inventory.selectSlot = inventory.slots[slotNumber];
                inventory.EnterSlot(slotNumber);
                if(InvenData.item[slotNumber] != null)
                {
                    inventory.OpenItemInfo(InvenData.item[slotNumber]);
                }
            }
            else if(inventory.state == InvenState.move2)
            {
                inventory.selectSlot = inventory.slots[slotNumber];
                inventory.EnterSlot(slotNumber);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            inventory.CloseItemInfo();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (inventory.state == InvenState.main)
                inventory.SelectItem(slotNumber);
            else if(inventory.state == InvenState.move2)
            {
                if(InvenData.item[slotNumber] == null)
                {
                    InvenData.item[slotNumber] = new Item();
                    InvenData.item[slotNumber].uid = inventory.selectedItem.uid;
                    InvenData.item[slotNumber].name = inventory.selectedItem.name;
                    InvenData.item[slotNumber].type = inventory.selectedItem.type;
                    InvenData.item[slotNumber].grade = inventory.selectedItem.grade;
                    InvenData.item[slotNumber].detailsGrade = inventory.selectedItem.detailsGrade;
                    InvenData.item[slotNumber].option = inventory.selectedItem.option;
                    InvenData.item[slotNumber].set = inventory.selectedItem.set;
                    InvenData.item[slotNumber].explain = inventory.selectedItem.explain;
                    InvenData.item[slotNumber].cooldown = inventory.selectedItem.cooldown;
                    InvenData.item[slotNumber].iconImg = inventory.selectedItem.iconImg;
                    InvenData.item[slotNumber].count = inventory.selectedItem.count;
                    InvenData.item[slotNumber].maxCount = inventory.selectedItem.maxCount;
                    inventory.moveItem.gameObject.SetActive(false);
                    inventory.selectedItem = null;
                    inventory.RefreshSlot();
                    inventory.state = InvenState.main;
                }
                else
                {
                    if(InvenData.item[slotNumber].uid == inventory.selectedItem.uid)
                    {
                        if (InvenData.item[slotNumber].count + inventory.selectedItem.count > InvenData.item[slotNumber].maxCount)
                        {
                            inventory.selectedItem.count -= InvenData.item[slotNumber].maxCount - InvenData.item[slotNumber].count;
                            InvenData.item[slotNumber].count = InvenData.item[slotNumber].maxCount;
                            inventory.RefreshSlot();
                        }
                        else
                        {
                            InvenData.item[slotNumber].count += inventory.selectedItem.count;
                            inventory.moveItem.gameObject.SetActive(false);
                            inventory.selectedItem = null;
                            inventory.RefreshSlot();
                            inventory.state = InvenState.main;
                        }
                    }
                    else
                    {
                        // 안 바뀜
                    }
                }
            }
        }
    }
}