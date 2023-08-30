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
                        _item = InvenData.Pocket1[0];
                        maxCount = InvenData.Pocket1[0].count;
                        break;
                    case 1:
                        _item = InvenData.Pocket1[1];
                        maxCount = InvenData.Pocket1[1].count;
                        break;
                    case 2:
                        _item = InvenData.Pocket2[0];
                        maxCount = InvenData.Pocket2[0].count;
                        break;
                    case 3:
                        _item = InvenData.Pocket2[1];
                        maxCount = InvenData.Pocket2[1].count;
                        break;
                    case 4:
                        _item = InvenData.Pocket3[0];
                        maxCount = InvenData.Pocket3[0].count;
                        break;
                    case 5:
                        _item = InvenData.Pocket3[1];
                        maxCount = InvenData.Pocket3[1].count;
                        break;
                }
                if (InvenData.nItem[inventory.selectSlot.slotNumber].count <= _item.maxCount - _item.count)
                    maxCount = InvenData.nItem[inventory.selectSlot.slotNumber].count;
                else
                    maxCount = _item.maxCount - _item.count;
            }
            else
                maxCount = InvenData.nItem[inventory.selectSlot.slotNumber].count;
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
            else if(inventory.state == InvenState.move)
                inventory.SelectItem(InvenData.nItem[inventory.selectSlot.slotNumber], count);
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

        public void Refresh()
        {// 새로고침
            up.SetActive(true);
            down.SetActive(true);
            if (count >= maxCount)
                up.SetActive(false);
            if (count <= 1)
                down.SetActive(false);
            number.text = count.ToString();
        }
    }
}
