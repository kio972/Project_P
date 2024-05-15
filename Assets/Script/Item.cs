using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace YeongJun
{
    public enum ItemType
    {
        equipWeapon,
        equipHelmet,
        equipNecklace,
        equipArmor,
        equipRing,
        equipShoes,
        potion,
        ingredient,
        Resource,
    }

    public enum ItemGrade
    {
        none,
        normal,
        rare,
        unique,
    }

    public enum ItemDetailGrade
    {
        none,
        low,
        middle,
        high,
    }

    [CreateAssetMenu]
    public class Item : ScriptableObject
    {
        public int uid; // 고유 코드
        public string name; // 이름
        public ItemType type; // 아이템 종류
        public ItemGrade grade; // 아이템 등급
        public ItemDetailGrade detailsGrade; // 세부 등급
        public string option; // 옵션
        public string set; // 세트
        public string explain; // 설명
        public int cooldown; // 쿨타임
        public Sprite iconImg; // 이미지
        public int count; // 개수
        public int maxCount; // 1번들 당 최대 개수
    }
}