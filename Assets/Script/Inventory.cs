using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YeongJun
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField]
        private GameObject slotPrefab;
        [SerializeField]
        private Transform slotParent;
        [SerializeField]
        private GameObject textn;
        [SerializeField]
        private GameObject texti;
        public Slot[] slots;
        private bool mode = false; // 인벤토리 모드

        private void Awake()
        {
            slots = new Slot[24];
            for (int i = 0; i < slots.Length; i++)
            {
                GameObject obj = Instantiate(slotPrefab);
                obj.transform.parent = slotParent;
                slots[i] = obj.GetComponent<Slot>();
                slots[i].slotNumber = i;
            }
        }

        public void ActiveInventory()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        public void RefreshSlot()
        {// 전체 슬롯 새로고침
            for (int i = 0; i < 24; i++)
                slots[i].Refresh(mode);
        }

        public void ChangeMode()
        {
            mode = !mode;
            textn.SetActive(!mode);
            texti.SetActive(mode);
            RefreshSlot();
        }

        public void Sort()
        {// 슬롯 정렬
            if (mode)
                iSort(); // 중요 아이템 정렬
            else
                nSort(); // 일반 아이템 정렬
            RefreshSlot();
        }

        private void nSort()
        {
            int i, j;
            Item key;
            // 아이템 위로 모으기
            for (i = 0; i < InvenData.nItem.Length - 1; i++)
            {
                for (j = i + 1; j < InvenData.nItem.Length; j++)
                {
                    if (InvenData.nItem[i] == null)
                    {
                        if (InvenData.nItem[j] == null)
                            continue;
                        else
                        {
                            InvenData.nItem[i] = InvenData.nItem[j];
                            InvenData.nItem[j] = null;
                        }
                    }
                    else
                        break;
                }
            }
            // 아이템 정렬
            for (i = 1; i < InvenData.nItem.Length; i++)
            {
                key = InvenData.nItem[i];
                if (key != null)
                {
                    for (j = i - 1; j >= 0 && InvenData.nItem[j].uid > key.uid; j--)
                        InvenData.nItem[j + 1] = InvenData.nItem[j];
                    InvenData.nItem[j + 1] = key;
                }
                else
                    break;
            }
        }

        private void iSort()
        {
            int i, j;
            for (i = 0; i < InvenData.iItem.Length - 1; i++)
            {
                for (j = i + 1; j < InvenData.iItem.Length; j++)
                {
                    if (InvenData.iItem[i] == null)
                    {
                        if (InvenData.iItem[j] == null)
                            continue;
                        else
                        {
                            InvenData.iItem[i] = InvenData.iItem[j];
                            InvenData.iItem[j] = null;
                        }
                    }
                    else
                        break;
                }
            }
        }

        void Update()
        {

        }
    }
}
