using JinWon;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ForgeDialog : MonoBehaviour
{
    private Image NPC;
    private Button btn1;
    private Button btn2;
    private Button btn3;
    private Button btn4;
    private Button btn5;
    private Button btn6;
    private TextMeshProUGUI dialogText;
    private TextMeshProUGUI NPCName;

    private GameObject obj;

    int num;

    private void Start()
    {
        //SoundManager.Inst.ChangeBGM(BGM_Type.BGM_Guild);
        //GameManager.Inst.Fade_InOut(true, 3.0f);
        insideTable = Resources.Load<IS_DataTable>("IS_DataTable");
        dialogTable = Resources.Load<IS_DataTable>("IS_DataTable");

        for (int i = 0; i < insideTable.Inside.Count; i++)
        {
            insideData.Add(insideTable.Inside[i].ObjectID, insideTable.Inside[i]);
        }

        for (int i = 0; i < dialogTable.Inside.Count; i++)
        {
            dialogData.Add(dialogTable.Dialogue[i].ObjectID, dialogTable.Dialogue[i]);
        }

        NPCName = GameObject.Find("NPCName").GetComponent<TextMeshProUGUI>();
        dialogText = GameObject.Find("DialogText").GetComponent<TextMeshProUGUI>();
        NPC = GameObject.Find("NPC").GetComponent<Image>();
        btn1 = GameObject.Find("Btn1").GetComponent<Button>();
        btn1.onClick.AddListener(OnClick_Btn1);
        btn2 = GameObject.Find("Btn2").GetComponent<Button>();
        btn2.onClick.AddListener(OnClick_Btn2);
        btn3 = GameObject.Find("Btn3").GetComponent<Button>();
        btn3.onClick.AddListener(OnClick_Btn3);
        btn4 = GameObject.Find("Btn4").GetComponent<Button>();
        btn4.onClick.AddListener(OnClick_Btn4);
        btn5 = GameObject.Find("Btn5").GetComponent<Button>();
        btn5.onClick.AddListener(OnClick_Btn5);
        btn6 = GameObject.Find("Btn6").GetComponent<Button>();
        btn6.onClick.AddListener(OnClick_Btn6);

        num = Random.Range(0, 3);

        NPCName.text = insideTable.Inside[0].NPC_Name.ToString();
        if (num == 0)
        {
            dialogText.text = insideTable.Inside[0].Text1.ToString();
        }
        else if (num == 1)
        {
            dialogText.text = insideTable.Inside[0].Text2.ToString();
        }
        else
        {
            dialogText.text = insideTable.Inside[0].Text3.ToString();
        }
    }

    [SerializeField]
    private SystemUI sysUI;

    private void OnClick_Btn1()
    {
        //SoundManager.Inst.PlaySFX("Click_off");
        GameManager.Inst.AsyncLoadNextScene(SceneName.UpGame);
    }

    private void OnClick_Btn2()
    {
        //SoundManager.Inst.PlaySFX("Click_off");
    }

    private void OnClick_Btn3()
    {
        //SoundManager.Inst.PlaySFX("Click_off");
    }
    private void OnClick_Btn4()
    {
        //SoundManager.Inst.PlaySFX("Click_off");
    }

    private void OnClick_Btn5()
    {
        //SoundManager.Inst.PlaySFX("Click_on");
        num = Random.Range(0, 3);
        if (num == 0)
        {
            dialogText.text = dialogTable.Dialogue[0].Text1.ToString();
        }
        else if (num == 1)
        {
            dialogText.text = dialogTable.Dialogue[0].Text2.ToString();
        }
        else
        {
            dialogText.text = dialogTable.Dialogue[0].Text3.ToString();
        }
    }

    private void OnClick_Btn6()
    {
        // 종료 텍스트 띄우고 종료?
        //SoundManager.Inst.PlaySFX("Click_on");
        sysUI.SystemUiTitle();
        //GameManager.Inst.AsyncLoadNextScene("CalendarScene");
    }


    private IS_DataTable insideTable;
    private IS_DataTable dialogTable;
    private Dictionary<int, Inside_Entity> insideData = new Dictionary<int, Inside_Entity>();
    private Dictionary<int, Dialog_Entity> dialogData = new Dictionary<int, Dialog_Entity>();

}
