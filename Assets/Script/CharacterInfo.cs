using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JinWon;

namespace YeongJun
{
    public class CharacterInfo : MonoBehaviour
    {
        [SerializeField] private Image charImg; // 캐릭터 이미지
        [SerializeField] private TextMeshProUGUI charName; // 캐릭터 이름
        [SerializeField] private TextMeshProUGUI charLevel; // 캐릭터 레벨
        [SerializeField] private TextMeshProUGUI charClass; // 캐릭터 직업
        [SerializeField] private Image charEXP; // 경험치
        [SerializeField] private TextMeshProUGUI charHP; // HP
        [SerializeField] private TextMeshProUGUI charMP; // MP
        [SerializeField] private TextMeshProUGUI charStr; // Str
        [SerializeField] private TextMeshProUGUI charDex; // Dex
        [SerializeField] private TextMeshProUGUI charVit; // Vit
        [SerializeField] private TextMeshProUGUI charInt; // Int
        [SerializeField] private TextMeshProUGUI charLuk; // Luk
        [SerializeField] private Image charCondition; // 컨디션
        [SerializeField] private TextMeshProUGUI propertyText1; // 특성 1
        [SerializeField] private TextMeshProUGUI propertyText2; // 특성 2
        [SerializeField] private TextMeshProUGUI propertyText3; // 특성 3

        public void Init()
        {
            Refresh();
        }

        public void Refresh()
        {// 새로고침
            //charImg.sprite =
            //charName.text =
            //charLevel.text =
            //charClass.text =
            //charEXP.sprite =
            //charHP.text =
            //charMP.text =
            //charStr.text =
            //charDex.text =
            //charVit.text =
            //charInt.text =
            //charLuk.text =
            //charCondition.sprite =
            //propertyText1.text =
            //propertyText2.text =
            //propertyText3.text =
        }       
    }           
}               
                