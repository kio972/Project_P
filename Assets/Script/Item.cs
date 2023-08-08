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
        public int uid; // ���� �ڵ�
        public string name; // �̸�
        public Sprite iconImg; // �̹���
        public int count; // ����
        public int maxCount; // �ִ� ����
        public ItemType type; // ������ Ÿ��
        public int grade; // ������ ���
    }
}