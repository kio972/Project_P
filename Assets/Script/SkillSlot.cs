using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace YeongJun
{
    public class SkillSlot : MonoBehaviour, IPointerClickHandler
    {
        public SkillList skillList;
        public TestSkill skill;
        [SerializeField] private Image skillImg;
        [SerializeField] private TextMeshProUGUI skillName;
        [SerializeField] private TextMeshProUGUI skillType;
        [SerializeField] private TextMeshProUGUI skillLevel;

        public void Refresh()
        {
            if (skill != null)
            {
                gameObject.SetActive(true);
                skillImg.sprite = skill.skillImg;
                skillName.text = skill.name;
                switch (skill.skillType)
                {
                    case SkillType.passive:
                        skillType.text = "�нú�";
                        break;
                    case SkillType.active:
                        skillType.text = "��Ƽ��";
                        break;
                    case SkillType.buff:
                        skillType.text = "����";
                        break;
                }
                skillLevel.text = skill.skillLevel.ToString();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            skillList.ClickSlot(this);
        }
    }
}