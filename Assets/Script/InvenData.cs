using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YeongJun
{// 임시 데이터
    public static class InvenData
    {
        public static Item[] item = new Item[24]; // 일반 아이템
        public static Item[] equip = new Item[6]; // 캐릭터가 장착한 장비 아이템
        public static Item[] pocket = new Item[2]; // 캐릭터가 장착한 소비 아이템
    }
}