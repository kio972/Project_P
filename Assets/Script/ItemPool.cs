using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YeongJun
{
    public class ItemPool : MonoBehaviour
    {
        public GameObject itemObject;
        [SerializeField]
        private Inventory inventory;
        private int maxCount = 10;
        private void Awake()
        {
            for (int i = 0; i < maxCount; i++)
            {
                GameObject obj = Instantiate(itemObject,transform);
                obj.GetComponent<ItemObject>().inventory = inventory;
                obj.SetActive(false);
            }
        }

        public void DropItem(Item _item ,int count)
        {
            bool isfull = true;
            for(int i = 0; i < maxCount; i++)
            {
                if (!transform.GetChild(i).gameObject.activeSelf)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                    transform.GetChild(i).GetComponent<ItemObject>().Init(_item, count);
                    isfull = false;
                    break;
                }
            }
            if (isfull)
            {
                GameObject obj = Instantiate(itemObject, transform);
                obj.GetComponent<ItemObject>().Init(_item, count);
            }
        }
    }
}
