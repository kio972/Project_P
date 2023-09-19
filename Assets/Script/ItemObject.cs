using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YeongJun
{
    public class ItemObject : MonoBehaviour
    {
        public Item itemInfo;
        public Inventory inventory;

        public void Init(Item _item, int count)
        {
            itemInfo = new Item();
            itemInfo.uid = _item.uid;
            itemInfo.name = _item.name;
            itemInfo.iconImg = _item.iconImg;
            itemInfo.count = count;
            itemInfo.maxCount = _item.maxCount;
            itemInfo.type = _item.type;
            itemInfo.grade = _item.grade;
            GetComponent<SpriteRenderer>().sprite = itemInfo.iconImg;
            for (int i = 0; i < inventory.player.Length; i++)
            {
                if (inventory.player[i].ControllMode)
                {
                    transform.position = inventory.player[i].transform.position;
                    break;
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if(collision.tag == "Player" && collision.GetComponent<Controller>().ControllMode)
            {
                if (Input.GetKey(KeyCode.Z)){
                    inventory.GetItem(itemInfo);
                    inventory.RefreshSlot();
                    gameObject.SetActive(false);
                }
            }
        }
    }
}