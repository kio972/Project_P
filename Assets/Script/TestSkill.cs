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
        public int uid; // 고유 코드
        public string name; // 이름
        public SkillType skillType; // 스킬 유형
        public Sprite skillImg; // 스킬 아이콘
        public int skillLevel = 1; // 스킬 레벨
        public string skillExplan; // 스킬 설명
    }
}