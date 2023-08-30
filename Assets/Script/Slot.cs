using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace YeongJun
{
    public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        public Inventory inventory;
        public int slotNumber; // ���� ��ȣ
        private Image itemImg; // ������ �̹���
        private TextMeshProUGUI countText; // ������ ����

        private void Awake()
        {
            itemImg = transform.GetChild(0).GetComponent<Image>();
            countText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }

        
        public void Refresh(bool mode)
        {// ���� ���ΰ�ħ
            if (mode)
            {
                if (InvenData.iItem[slotNumber] != null)
                {
                    if (InvenData.iItem[slotNumber].count <= 0)
                    {
                        InvenData.iItem[slotNumber] = null;
                        itemImg.gameObject.SetActive(false);
                        countText.gameObject.SetActive(false);
                        return;
                    }
                    itemImg.gameObject.SetActive(true);
                    countText.gameObject.SetActive(true);
                    itemImg.sprite = InvenData.iItem[slotNumber].iconImg;
                    countText.text = InvenData.iItem[slotNumber].count.ToString();
                }
                else
                {
                    itemImg.gameObject.SetActive(false);
                    countText.gameObject.SetActive(false);
                }
            }
            else
            {
                if (InvenData.nItem[slotNumber] != null)
                {
                    if (InvenData.nItem[slotNumber].count <= 0)
                    {
                        InvenData.nItem[slotNumber] = null;
                        itemImg.gameObject.SetActive(false);
                        countText.gameObject.SetActive(false);
                        return;
                    }
                    itemImg.gameObject.SetActive(true);
                    countText.gameObject.SetActive(true);
                    itemImg.sprite = InvenData.nItem[slotNumber].iconImg;
                    countText.text = InvenData.nItem[slotNumber].count.ToString();
                }
                else
                {
                    itemImg.gameObject.SetActive(false);
                    countText.gameObject.SetActive(false);
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (inventory.state == InvenState.main || inventory.state == InvenState.moveItem)
            {
                inventory.selectSlot = inventory.slots[slotNumber];
                inventory.EnterSlot(slotNumber);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (inventory.state == InvenState.main)
                inventory.SelectItem(slotNumber);
            else if (inventory.state == InvenState.moveItem)
                inventory.MoveItem();
        }
    }
}