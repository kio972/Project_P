using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YeongJun
{
    public enum SkillType
    {
        passive,
        active,
        buff,
    }

    [CreateAssetMenu]
    public class TestSkill : ScriptableObject
    {
        public int uid; // ���� �ڵ�
        public string name; // �̸�
        public SkillType skillType; // ��ų ����
        public Sprite skillImg; // ��ų ������
        public int skillLevel = 1; // ��ų ����
        public string skillExplan; // ��ų ����
    }
}