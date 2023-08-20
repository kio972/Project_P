using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace YeongJun 
{
    public class Pocket : MonoBehaviour
    {
        public int pocketNumber; // 주머니 번호
        private Image itemImg; // 아이템 이미지
        private TextMeshProUGUI countText; // 아이템 개수

        private void Awake()
        {
            itemImg = transform.GetChild(0).GetComponent<Image>();
            countText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }

        public void Refresh()
        {// 주머니 새로고침
            switch (pocketNumber)
            {
                case 0:
                    if (InvenData.Pocket1[0] != null)
                    {
                        itemImg.gameObject.SetActive(true);
                        countText.gameObject.SetActive(true);
                        itemImg.sprite = InvenData.Pocket1[0].iconImg;
                        countText.text = InvenData.Pocket1[0].count.ToString();
                    }
                    else
                    {
                        itemImg.gameObject.SetActive(false);
                        countText.gameObject.SetActive(false);
                    }
                    break;
                case 1:
                    if (InvenData.Pocket1[1] != null)
                    {
                        itemImg.gameObject.SetActive(true);
                        countText.gameObject.SetActive(true);
                        itemImg.sprite = InvenData.Pocket1[1].iconImg;
                        countText.text = InvenData.Pocket1[1].count.ToString();
                    }
                    else
                    {
                        itemImg.gameObject.SetActive(false);
                        countText.gameObject.SetActive(false);
                    }
                    break;
                case 2:
                    if (InvenData.Pocket2[0] != null)
                    {
                        itemImg.gameObject.SetActive(true);
                        countText.gameObject.SetActive(true);
                        itemImg.sprite = InvenData.Pocket2[0].iconImg;
                        countText.text = InvenData.Pocket2[0].count.ToString();
                    }
                    else
                    {
                        itemImg.gameObject.SetActive(false);
                        countText.gameObject.SetActive(false);
                    }
                    break;
                case 3:
                    if (InvenData.Pocket2[1] != null)
                    {
                        itemImg.gameObject.SetActive(true);
                        countText.gameObject.SetActive(true);
                        itemImg.sprite = InvenData.Pocket2[1].iconImg;
                        countText.text = InvenData.Pocket2[1].count.ToString();
                    }
                    else
                    {
                        itemImg.gameObject.SetActive(false);
                        countText.gameObject.SetActive(false);
                    }
                    break;
                case 4:
                    if (InvenData.Pocket3[0] != null)
                    {
                        itemImg.gameObject.SetActive(true);
                        countText.gameObject.SetActive(true);
                        itemImg.sprite = InvenData.Pocket3[0].iconImg;
                        countText.text = InvenData.Pocket3[0].count.ToString();
                    }
                    else
                    {
                        itemImg.gameObject.SetActive(false);
                        countText.gameObject.SetActive(false);
                    }
                    break;
                case 5:
                    if (InvenData.Pocket3[1] != null)
                    {
                        itemImg.gameObject.SetActive(true);
                        countText.gameObject.SetActive(true);
                        itemImg.sprite = InvenData.Pocket3[1].iconImg;
                        countText.text = InvenData.Pocket3[1].count.ToString();
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
    }
}
