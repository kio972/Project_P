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
        public int uid; // ���� �ڵ�
        public string name; // �̸�
        public ItemType type; // ������ ����
        public ItemGrade grade; // ������ ���
        public ItemDetailGrade detailsGrade; // ���� ���
        public string option; // �ɼ�
        public string set; // ��Ʈ
        public string explain; // ����
        public int cooldown; // ��Ÿ��
        public Sprite iconImg; // �̹���
        public int count; // ����
        public int maxCount; // 1���� �� �ִ� ����
    }
}