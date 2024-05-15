using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace YeongJun
{
    public class ItemInfo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemType;
        [SerializeField] private TextMeshProUGUI itemGrade;
        [SerializeField] private TextMeshProUGUI itemOption;
        [SerializeField] private TextMeshProUGUI itemSet;
        [SerializeField] private TextMeshProUGUI itemExplain;
        [SerializeField] private TextMeshProUGUI itemCooldown;

        public void ShowInfo(Item item)
        {
            switch (item.detailsGrade)
            {
                case ItemDetailGrade.none:
                    itemName.text = item.name;
                    break;
                case ItemDetailGrade.low:
                    itemName.text = item.name + " (��)";
                    break;
                case ItemDetailGrade.middle:
                    itemName.text = item.name + " (��)";
                    break;
                case ItemDetailGrade.high:
                    itemName.text = item.name + " (��)";
                    break;
            }
            switch (item.type)
            {
                case ItemType.equipWeapon:
                    itemType.text = "���";
                    itemOption.gameObject.SetActive(true);
                    itemSet.gameObject.SetActive(true);
                    itemExplain.gameObject.SetActive(false);
                    itemCooldown.gameObject.SetActive(false);
                    break;
                case ItemType.equipHelmet:
                    itemType.text = "���";
                    itemOption.gameObject.SetActive(true);
                    itemSet.gameObject.SetActive(true);
                    itemExplain.gameObject.SetActive(false);
                    itemCooldown.gameObject.SetActive(false);
                    break;
                case ItemType.equipNecklace:
                    itemType.text = "���";
                    itemOption.gameObject.SetActive(true);
                    itemSet.gameObject.SetActive(true);
                    itemExplain.gameObject.SetActive(false);
                    itemCooldown.gameObject.SetActive(false);
                    break;
                case ItemType.equipArmor:
                    itemType.text = "���";
                    itemOption.gameObject.SetActive(true);
                    itemSet.gameObject.SetActive(true);
                    itemExplain.gameObject.SetActive(false);
                    itemCooldown.gameObject.SetActive(false);
                    break;
                case ItemType.equipRing:
                    itemType.text = "���";
                    itemOption.gameObject.SetActive(true);
                    itemSet.gameObject.SetActive(true);
                    itemExplain.gameObject.SetActive(false);
                    itemCooldown.gameObject.SetActive(false);
                    break;
                case ItemType.equipShoes:
                    itemType.text = "���";
                    itemOption.gameObject.SetActive(true);
                    itemSet.gameObject.SetActive(true);
                    itemExplain.gameObject.SetActive(false);
                    itemCooldown.gameObject.SetActive(false);
                    break;
                case ItemType.potion:
                    itemType.text = "�Ҹ�ǰ";
                    itemOption.gameObject.SetActive(false);
                    itemSet.gameObject.SetActive(false);
                    itemExplain.gameObject.SetActive(true);
                    itemCooldown.gameObject.SetActive(true);
                    break;
                case ItemType.ingredient:
                    itemType.text = "���";
                    itemOption.gameObject.SetActive(false);
                    itemSet.gameObject.SetActive(false);
                    itemExplain.gameObject.SetActive(true);
                    itemCooldown.gameObject.SetActive(true);
                    break;
            }
            switch (item.grade)
            {
                case ItemGrade.none:
                    itemGrade.gameObject.SetActive(false);
                    break;
                case ItemGrade.normal:
                    itemGrade.gameObject.SetActive(true);
                    itemGrade.text = "�Ϲ�";
                    break;
                case ItemGrade.rare:
                    itemGrade.gameObject.SetActive(true);
                    itemGrade.text = "����";
                    break;
                case ItemGrade.unique:
                    itemGrade.gameObject.SetActive(true);
                    itemGrade.text = "����ũ";
                    break;
            }
            itemOption.text = item.option;
            itemSet.text = item.set;
            itemExplain.text = item.explain;
            itemCooldown.text = item.cooldown.ToString() + "��";
        }
    }
}