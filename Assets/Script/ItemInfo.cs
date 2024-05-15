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
                    itemName.text = item.name + " (하)";
                    break;
                case ItemDetailGrade.middle:
                    itemName.text = item.name + " (중)";
                    break;
                case ItemDetailGrade.high:
                    itemName.text = item.name + " (상)";
                    break;
            }
            switch (item.type)
            {
                case ItemType.equipWeapon:
                    itemType.text = "장비";
                    itemOption.gameObject.SetActive(true);
                    itemSet.gameObject.SetActive(true);
                    itemExplain.gameObject.SetActive(false);
                    itemCooldown.gameObject.SetActive(false);
                    break;
                case ItemType.equipHelmet:
                    itemType.text = "장비";
                    itemOption.gameObject.SetActive(true);
                    itemSet.gameObject.SetActive(true);
                    itemExplain.gameObject.SetActive(false);
                    itemCooldown.gameObject.SetActive(false);
                    break;
                case ItemType.equipNecklace:
                    itemType.text = "장비";
                    itemOption.gameObject.SetActive(true);
                    itemSet.gameObject.SetActive(true);
                    itemExplain.gameObject.SetActive(false);
                    itemCooldown.gameObject.SetActive(false);
                    break;
                case ItemType.equipArmor:
                    itemType.text = "장비";
                    itemOption.gameObject.SetActive(true);
                    itemSet.gameObject.SetActive(true);
                    itemExplain.gameObject.SetActive(false);
                    itemCooldown.gameObject.SetActive(false);
                    break;
                case ItemType.equipRing:
                    itemType.text = "장비";
                    itemOption.gameObject.SetActive(true);
                    itemSet.gameObject.SetActive(true);
                    itemExplain.gameObject.SetActive(false);
                    itemCooldown.gameObject.SetActive(false);
                    break;
                case ItemType.equipShoes:
                    itemType.text = "장비";
                    itemOption.gameObject.SetActive(true);
                    itemSet.gameObject.SetActive(true);
                    itemExplain.gameObject.SetActive(false);
                    itemCooldown.gameObject.SetActive(false);
                    break;
                case ItemType.potion:
                    itemType.text = "소모품";
                    itemOption.gameObject.SetActive(false);
                    itemSet.gameObject.SetActive(false);
                    itemExplain.gameObject.SetActive(true);
                    itemCooldown.gameObject.SetActive(true);
                    break;
                case ItemType.ingredient:
                    itemType.text = "재료";
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
                    itemGrade.text = "일반";
                    break;
                case ItemGrade.rare:
                    itemGrade.gameObject.SetActive(true);
                    itemGrade.text = "레어";
                    break;
                case ItemGrade.unique:
                    itemGrade.gameObject.SetActive(true);
                    itemGrade.text = "유니크";
                    break;
            }
            itemOption.text = item.option;
            itemSet.text = item.set;
            itemExplain.text = item.explain;
            itemCooldown.text = item.cooldown.ToString() + "초";
        }
    }
}