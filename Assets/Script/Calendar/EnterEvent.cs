using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EnterEvent : MonoBehaviour
{

    private GameObject frame;
    private Button enterBtn;
    private Button cancelBtn;

    private Button villageBtn;
    private Button mapBtn;
    private Button repairBtn;

    private int index;

    private TextMeshProUGUI enterText;

    private void Awake()
    {
        frame = GameObject.Find("EnterFrame");

        enterText = GameObject.Find("EnterText").GetComponent<TextMeshProUGUI>();

        villageBtn = GameObject.Find("VillageBtn").GetComponent<Button>();
        villageBtn.onClick.AddListener(OnClick_VillageBtn);
        mapBtn = GameObject.Find("MapBtn").GetComponent<Button>();
        mapBtn.onClick.AddListener(OnClick_MapBtn);
        repairBtn = GameObject.Find("RepairBtn").GetComponent<Button>();
        repairBtn.onClick.AddListener(OnClick_RepairBtn);

        enterBtn = GameObject.Find("EnterBtn").GetComponent<Button>();
        enterBtn.onClick.AddListener(OnClick_enterBtn);
        cancelBtn = GameObject.Find("CancelBtn").GetComponent<Button>();
        cancelBtn.onClick.AddListener(OnClick_cancelBtn);
    }

    private void OnClick_VillageBtn()
    {
        index = 1;
        LeanTween.scale(frame, Vector3.zero, 0.7f).setEase(LeanTweenType.clamp);
        enterText.text = "������ �����Ͻðڽ��ϱ�?";
    }

    private void OnClick_MapBtn()
    {
        index = 2;
        LeanTween.scale(frame, Vector3.zero, 0.7f).setEase(LeanTweenType.clamp);
        enterText.text = "Ž�� �Ͻðڽ��ϱ�?";
    }

    private void OnClick_RepairBtn()
    {
        index = 3;
        LeanTween.scale(frame, Vector3.zero, 0.7f).setEase(LeanTweenType.clamp);
        enterText.text = "���� �Ͻðڽ��ϱ�?";
    }

    private void OnClick_enterBtn()
    {
        switch (index)
        {
            case 1: // ���� ��ư ������ ���
                {

                    break;
                }
            case 2: // Ž�� ��ư ������ ���
                {
                    break;
                }
            case 3: // ���� ��ư ������ ���
                {
                    break;
                }
        }
    }

    // ��� ��ư �̺�Ʈ
    private void OnClick_cancelBtn()
    {
        LeanTween.scale(frame, Vector3.zero, 0.9f).setEase(LeanTweenType.clamp);
    }
}
