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
        selectCard = selectArranger.GetComponentInChildren<CharCard>();

        nameText.text = selectCard.charName;
        
    }
}
