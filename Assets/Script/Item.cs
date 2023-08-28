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

    [CreateAssetMenu]
    public class Item : ScriptableObject
    {
        public int uid; // ���� �ڵ�
        public string name; // �̸�
        public Sprite iconImg; // �̹���
        public int count; // ����
        public int maxCount; // 1���� �� �ִ� ����
        public ItemType type; // ������ Ÿ��
        public int grade; // ������ ���
    }
}