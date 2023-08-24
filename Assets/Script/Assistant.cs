using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YeongJun
{
    public class Assistant : MonoBehaviour
    {
        public Inventory inventory;
        public int slotNumber = 0;
        private int count = 1;
        [SerializeField] private GameObject up;
        [SerializeField] private GameObject down;
        [SerializeField] private Text number;
        public void AssistantCheck()
        {
            inventory.assistCount = count;
            if(inventory.state == InvenState.use)
            {

            }
            else if(inventory.state == InvenState.equip_count)
            {
                inventory.EquipItem(count);
            }
            else if(inventory.state == InvenState.move)
            {

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
            if (count >= InvenData.nItem[slotNumber].count)
                up.SetActive(false);
            if(count <= 1)
                down.SetActive(false);
            number.text = count.ToString();
        }
    }
}
