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
        [SerializeField] private Image charImg; // ĳ���� �̹���
        [SerializeField] private TextMeshProUGUI charName; // ĳ���� �̸�
        [SerializeField] private TextMeshProUGUI charLevel; // ĳ���� ����
        [SerializeField] private TextMeshProUGUI charClass; // ĳ���� ����
        [SerializeField] private Image charEXP; // ����ġ
        [SerializeField] private TextMeshProUGUI charHP; // HP
        [SerializeField] private TextMeshProUGUI charMP; // MP
        [SerializeField] private TextMeshProUGUI charStr; // Str
        [SerializeField] private TextMeshProUGUI charDex; // Dex
        [SerializeField] private TextMeshProUGUI charVit; // Vit
        [SerializeField] private TextMeshProUGUI charInt; // Int
        [SerializeField] private TextMeshProUGUI charLuk; // Luk
        [SerializeField] private Image charCondition; // �����
        [SerializeField] private TextMeshProUGUI propertyText1; // Ư�� 1
        [SerializeField] private TextMeshProUGUI propertyText2; // Ư�� 2
        [SerializeField] private TextMeshProUGUI propertyText3; // Ư�� 3

        public void Init()
        {
            Refresh();
        }

        public void Refresh()
        {// ���ΰ�ħ
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
                