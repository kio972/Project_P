using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YeongJun
{
    public class Assistant : MonoBehaviour
    {
        public Inventory inventory;
        private int count = 1;
        private int maxCount = 1;
        private int slotNumber = 0;
        [SerializeField] private GameObject up;
        [SerializeField] private GameObject down;
        [SerializeField] private Text number;
        Item _item;

        public void Init()
        {
            count = 1;
            if (inventory.state == InvenState.unequip)
            {
                switch (inventory.pocketNum)
                {
                    case 0:
                        _item = InvenData.pocket[0];
                        maxCount = InvenData.pocket[0].count;
                        break;
                    case 1:
                        _item = InvenData.pocket[1];
                        maxCount = InvenData.pocket[1].count;
                        break;
                    default:
                        _item = InvenData.pocket[inventory.pocketNum];
                        maxCount = 1;
                        break;
                }
                if (InvenData.item[inventory.selectSlot.slotNumber].count <= _item.maxCount - _item.count)
                    maxCount = InvenData.item[inventory.selectSlot.slotNumber].count;
                else
                    maxCount = _item.maxCount - _item.count;
            }
            else
                maxCount = InvenData.item[inventory.selectSlot.slotNumber].count;
            Refresh();
        }

        public void AssistantCheck()
        {
            inventory.assistCount = -1;
            if(inventory.state == InvenState.equip_count)
            {
                inventory.EquipItem(count);
                inventory.RefreshSlot();
            }
            else if(inventory.state == InvenState.move1)
            {
                inventory.selectedItem = new Item();
                inventory.selectedItem.uid = InvenData.item[inventory.selectSlot.slotNumber].uid;
                inventory.selectedItem.name = InvenData.item[inventory.selectSlot.slotNumber].name;
                inventory.selectedItem.type = InvenData.item[inventory.selectSlot.slotNumber].type;
                inventory.selectedItem.grade = InvenData.item[inventory.selectSlot.slotNumber].grade;
                inventory.selectedItem.detailsGrade = InvenData.item[inventory.selectSlot.slotNumber].detailsGrade;
                inventory.selectedItem.option = InvenData.item[inventory.selectSlot.slotNumber].option;
                inventory.selectedItem.set = InvenData.item[inventory.selectSlot.slotNumber].set;
                inventory.selectedItem.explain = InvenData.item[inventory.selectSlot.slotNumber].explain;
                inventory.selectedItem.cooldown = InvenData.item[inventory.selectSlot.slotNumber].cooldown;
                inventory.selectedItem.iconImg = InvenData.item[inventory.selectSlot.slotNumber].iconImg;
                inventory.selectedItem.count = count;
                inventory.selectedItem.maxCount = InvenData.item[inventory.selectSlot.slotNumber].maxCount;
                InvenData.item[inventory.selectSlot.slotNumber].count -= count;
                inventory.Move2();
                inventory.RefreshSlot();
            }
            else if(inventory.state == InvenState.drop1)
            {
                inventory.selectedItem = new Item();
                inventory.selectedItem.uid = InvenData.item[inventory.selectSlot.slotNumber].uid;
                inventory.selectedItem.name = InvenData.item[inventory.selectSlot.slotNumber].name;
                inventory.selectedItem.type = InvenData.item[inventory.selectSlot.slotNumber].type;
                inventory.selectedItem.grade = InvenData.item[inventory.selectSlot.slotNumber].grade;
                inventory.selectedItem.detailsGrade = InvenData.item[inventory.selectSlot.slotNumber].detailsGrade;
                inventory.selectedItem.option = InvenData.item[inventory.selectSlot.slotNumber].option;
                inventory.selectedItem.set = InvenData.item[inventory.selectSlot.slotNumber].set;
                inventory.selectedItem.explain = InvenData.item[inventory.selectSlot.slotNumber].explain;
                inventory.selectedItem.cooldown = InvenData.item[inventory.selectSlot.slotNumber].cooldown;
                inventory.selectedItem.iconImg = InvenData.item[inventory.selectSlot.slotNumber].iconImg;
                inventory.selectedItem.count = count;
                inventory.selectedItem.maxCount = InvenData.item[inventory.selectSlot.slotNumber].maxCount;
                inventory.ShowDropMessage();
                inventory.state = InvenState.drop2;
            }
            else if(inventory.state == InvenState.unequip)
            {
                inventory.assistCount = slotNumber;
                inventory.EquipItem(count);
                inventory.RefreshSlot();
            }
            gameObject.SetActive(false);
        }

        public void AssistantCancel()
        {
            count = 1;
            gameObject.SetActive(false);
            inventory.state = InvenState.main;
        }

        public void Plus()
        {// count 증가
            count++;
            Refresh();
        }

        public void Minus()
        {// count 감소
            count--;
            Refresh();
        }

        public void Plus10()
        {// count 10증가
            count += 10;
            Refresh();
        }

        public void Minus10()
        {// count 10감소
            count -= 10;
            Refresh();
        }

        public void Refresh()
        {// 새로고침
            up.SetActive(true);
            down.SetActive(true);
            if (count >= maxCount)
            {
                count = maxCount;
                up.SetActive(false);
            }
            if (count <= 1)
            {
                count = 1;
                down.SetActive(false);
            }
            number.text = count.ToString();
        }
    }
}
