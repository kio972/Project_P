using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharInfoObj : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> btnList = new List<GameObject>(); // Ư�� ��ų ���

    [SerializeField]
    private List<GameObject> textUIList = new List<GameObject>(); // Ư�� ��ų ���

    [SerializeField]
    private TextMeshProUGUI nameText;

    [SerializeField]
    private CardArranger selectArranger;

    [SerializeField]
    private CharCard selectCard;

    private void Awake() // �ӽ��ڵ� / ���߿� Manager��ũ��Ʈ���� ȣ���ϵ��� ����!
    {
        Init();
    }

    public void Init() // �ʱ�ȭ
    {
        BtnOnClick(0); // Ư���� �⺻���� ������ ����
        //SelectCardInfo();
    }

    public void BtnOnClick(int whatBtn) // ���� Ŭ��������.
    {
        BtnScaleInit(whatBtn);
        TextUIInit(whatBtn);
    }

    private Vector3 btnScale = new Vector3(0.8f, 0.8f, 1f);

    private void BtnScaleInit(int whatBtn) // ���� ������ ����
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
