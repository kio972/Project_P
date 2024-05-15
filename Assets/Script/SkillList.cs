using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace YeongJun
{
    public class SkillList : MonoBehaviour
    {
        [Header("equipSkill")]
        private bool selectNum = false; // 0이면 1번 스킬 선택, 1이면 2번 스킬 선택
        [SerializeField] private GameObject highlightImg;
        private TestSkill equipSkill1;
        [SerializeField] private Image equipSkill1Img;
        [SerializeField] private TextMeshProUGUI equipSkill1Name;
        private TestSkill equipSkill2;
        [SerializeField] private Image equipSkill2Img;
        [SerializeField] private TextMeshProUGUI equipSkill2Name;

        [Header("selectedSkill")]
        private TestSkill selectedSkill;
        [SerializeField] private GameObject skillInfo;
        [SerializeField] private TextMeshProUGUI skillName;
        [SerializeField] private Image skillImg;
        [SerializeField] private TextMeshProUGUI skillType;
        [SerializeField] private TextMeshProUGUI skillExplan;
        [SerializeField] private GameObject BtnGroup;

        [Header("SkillSlot")]
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private Transform slotParent;
        public SkillSlot[] slots;

        [Header("TestSkill")]
        [SerializeField] TestSkill[] skills;

        public void Init()
        {
            selectedSkill = null;
            slots = new SkillSlot[skills.Length];
            for (int i = 0; i < skills.Length; i++)
            {
                GameObject obj = Instantiate(slotPrefab);
                obj.transform.SetParent(slotParent);
                slots[i] = obj.GetComponent<SkillSlot>();
                slots[i].skill = skills[i];
                slots[i].skillList = this;
                slots[i].Refresh();
            }
            highlightImg.transform.position = equipSkill1Img.transform.position;
            if (equipSkill1 == null)
                equipSkill1 = slots[0].skill;
            if (equipSkill2 == null)
                equipSkill2 = slots[1].skill;
        }

        public void Skill1Click()
        {
            selectNum = false;
            Refresh();
        }

        public void Skill2Click()
        {
            selectNum = true;
            Refresh();
        }

        public void Refresh()
        {
            if (!selectNum)
                highlightImg.transform.position = equipSkill1Img.transform.position;
            else
                highlightImg.transform.position = equipSkill2Img.transform.position;
            if (equipSkill1 != null)
            {
                equipSkill1Img.sprite = equipSkill1.skillImg;
                equipSkill1Name.text = equipSkill1.name;
            }
            if (equipSkill2 != null)
            {
                equipSkill2Img.sprite = equipSkill2.skillImg;
                equipSkill2Name.text = equipSkill2.name;
            }
        }

        public void ClickSlot(SkillSlot skillSlot)
        {
            BtnGroup.SetActive(true);
            BtnGroup.transform.position = skillSlot.transform.position + new Vector3(150, 15, 0);
            selectedSkill = skillSlot.skill;
            skillInfo.SetActive(true);
            skillName.text = selectedSkill.name;
            skillImg.sprite = selectedSkill.skillImg;
            switch (selectedSkill.skillType)
            {
                case SkillType.passive:
                    skillType.text = "패시브";
                    break;
                case SkillType.active:
                    skillType.text = "액티브";
                    break;
                case SkillType.buff:
                    skillType.text = "버프";
                    break;
            }
            skillExplan.text = selectedSkill.skillExplan;
        }

        public void EquipSkill()
        {// 장착 버튼
            if (!selectNum)
            {
                equipSkill1 = selectedSkill;
                Refresh();
                Cancel();
            }
            else
            {
                equipSkill2 = selectedSkill;
                Refresh();
                Cancel();
            }
        }

        public void Cancel()
        {// 취소 버튼
            BtnGroup.SetActive(false);
            selectedSkill = null;
        }
    }
}