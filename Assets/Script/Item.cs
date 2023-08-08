using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace YeongJun
{
    public enum ItemType
    {
        equip,
        consumable,
        material,
        important,
    }

    public class Item : MonoBehaviour
    {
        public int uid; // 고유 코드
        public string name; // 이름
        public Sprite iconImg; // 이미지
        public int count; // 개수
        public int maxCount; // 최대 개수
        public ItemType type; // 아이템 타입
        public int grade; // 아이템 등급
    }
}