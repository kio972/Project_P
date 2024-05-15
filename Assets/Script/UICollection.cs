using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YeongJun
{
    public class UICollection : MonoBehaviour
    {
        [SerializeField] private CharacterInfo characterInfo;
        [SerializeField] private Inventory inventory;
        [SerializeField] private SkillList skillList;

        private void Awake()
        {
            characterInfo.Init();
            inventory.Init();
            skillList.Init();
        }

        public void OpenInven()
        {
            inventory.gameObject.SetActive(true);
            skillList.gameObject.SetActive(false);
        }

        public void OpenSkill()
        {
            inventory.gameObject.SetActive(false);
            skillList.gameObject.SetActive(true);
        }
    }
}