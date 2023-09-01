using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharInfoObj : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> btnList = new List<GameObject>(); // 특성 스킬 장비

    [SerializeField]
    private List<GameObject> textUIList = new List<GameObject>(); // 특성 스킬 장비

    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI levelText;
    [SerializeField]
    private TextMeshProUGUI infoText;

    [SerializeField]
    private TextMeshProUGUI hpText;
    [SerializeField]
    private TextMeshProUGUI mpText;

    [SerializeField]
    private List<TextMeshProUGUI> statTextList = new List<TextMeshProUGUI>();

    [SerializeField]
    private List<TextMeshProUGUI> hiddenTextList = new List<TextMeshProUGUI>();

    [SerializeField]
    private List<GameObject> charImgList = new List<GameObject>(); // 전사 궁수 신관

    [SerializeField]
    private CardArranger selectArranger;

    [SerializeField]
    private CharCard selectCard;

    private void Awake() // 임시코드 / 나중에 Manager스크립트에서 호출하도록 변경!
    {
        Init();
    }

    public void Init() // 초기화
    {
        BtnOnClick(0); // 특성을 기본으로 설정해 놓기
        //SelectCardInfo();
    }

    public void BtnOnClick(int whatBtn) // 탭을 클릭했을때.
    {
        BtnScaleInit(whatBtn);
        TextUIInit(whatBtn);
    }

    private Vector3 btnScale = new Vector3(0.8f, 0.8f, 1f);

    private void BtnScaleInit(int whatBtn) // 탭의 사이즈 조정
    {
        for (int i = 0; i < btnList.Count; i++)
        {
            if(i == whatBtn)
                btnList[whatBtn].transform.localScale = Vector3.one;
            else
                btnList[i].transform.localScale = btnScale;
        }
    }

    private void TextUIInit(int whatUI)
    {
        for (int i = 0; i < textUIList.Count; i++)
        {
            if (i == whatUI)
                textUIList[whatUI].SetActive(true);
            else
                textUIList[i].SetActive(false);
        }
    }

    public void SelectCardInfo()
    {
        if(selectArranger.GetComponentInChildren<CharCard>())
        {
            selectCard = selectArranger.GetComponentInChildren<CharCard>();

            switch(selectCard.PlayerType)
            {
                case PlayerType.Warrior:
                    {
                        CharImgSetting(0);
                        break;
                    }
                case PlayerType.Archer:
                    {
                        CharImgSetting(1);
                        break;
                    }
                case PlayerType.Priest:
                    {
                        CharImgSetting(2);
                        break;
                    }
            }

            nameText.text = selectCard.charName;
            levelText.text = "Lv. " + selectCard.charLv;
            infoText.text = selectCard.charInfo;

            hpText.text = selectCard.charHP.ToString();
            mpText.text = selectCard.charMP.ToString();

            for (int i = 0; i < statTextList.Count; i++)
            {
                statTextList[i].text = selectCard.charStat[i].ToString();
            }

            for(int i = 0; i < selectCard.hiddenList.Count; i++)
            {
                hiddenTextList[i].text = selectCard.hiddenList[i];
            }
        }
    }

    public void CharImgSetting(int num)
    {
        for(int i = 0; i < charImgList.Count; i++)
        {
            if (i == num)
                charImgList[i].SetActive(true);
            else
                charImgList[i].SetActive(false);
        }
    }
}
