using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JinWon;
using TMPro;

public class TownUI : MonoBehaviour
{
    [SerializeField] private TownManager townManager;
    public bool isControll = true;

    [Header("MainCanvas")]
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private Produce produce;
    [SerializeField] private Toast toast;
    [SerializeField] private Resource resource;
    [SerializeField] private SimpleCalender simpleCalender;
    [SerializeField] private EndBtn endBtn;
    [SerializeField] private SelectBuilding selectBuilding; // ���õ� �ǹ� UI
    [SerializeField] private GameObject interaction; // �ǹ��� ��ȣ�ۿ� �� UI
    [SerializeField] private Text interText;

    [Header("EditCanvas")]
    [SerializeField] private Edit editCanvas;
    [SerializeField] private EditBuildingList[] editBuildingList;

    [Header("ExpansionCanvas")]
    [SerializeField] private Expansion expansionCanvas;
    [SerializeField] private GameObject possible; // Ȯ���� ������ �� ����� UI
    [SerializeField] private GameObject impossible; // Ȯ���� �Ұ����� �� ����� UI

    [Header("BuildCanvas")]
    [SerializeField] private Build buildCanvas;
    [SerializeField] private Button[] buttons;
    [SerializeField] private Button[] secondButtons;
    [SerializeField] private Sprite basicImg;
    [SerializeField] private Sprite secondImg;
    [SerializeField] private GameObject buildMenu;
    [SerializeField] private GameObject buildList;
    [SerializeField] private GameObject btnMenu;

    [Header("MenuCanvas")]
    [SerializeField] private Menu menuCanvas;

    public void Init()
    {
        resource.Refresh();
        buildCanvas.Init();
    }

    #region MainCanvas
    public void OpenSelectBuilding(Building sb)
    {
        selectBuilding.gameObject.SetActive(true);
        selectBuilding.transform.GetChild(0).GetComponent<Image>().sprite = sb.image;
        selectBuilding.transform.GetChild(2).GetComponent<Text>().text = sb.name;
    }

    public void EnterBuilding()
    {
        interaction.SetActive(true);
        interText.text = townManager.selectedBuilding.name;
    }

    public void ExitBuilding()
    {
        interaction.SetActive(false);
    }

    public void InteractionBuilding()
    {
        OpenSelectBuilding(townManager.selectedBuilding);
    }
    #endregion

    #region EditCanvas
    public void OpenEditCanvas()
    {
        SoundManager.Inst.PlaySFX("Click_on");
        editCanvas.gameObject.SetActive(true);
        RefreshEditList();
        isControll = false;
    }

    private void RefreshEditList()
    {
        for (int i = 0; i < editBuildingList.Length; i++)
        {
            if (i < townManager.building.Length && townManager.building[i].isBuild)
            {
                editBuildingList[i].gameObject.SetActive(true);
                editBuildingList[i].Refresh(townManager.building[i]);
            }
            else
                editBuildingList[i].gameObject.SetActive(false);
        }
    }

    public void EditBuildingListBtn(int number)
    {
        townManager.selectedBuilding = townManager.building[number].GetComponent<Building>();
        RefreshEditList();
        editBuildingList[number].ClickEditList();
    }

    public void AdmissionBtn()
    {
        SoundManager.Inst.PlaySFX("Click_on");
        GameManager.Inst.AsyncLoadNextScene("Guild");
        // �ǹ� ����
    }

    public void ExpansionBtn()
    {
        SoundManager.Inst.PlaySFX("Click_off");
        /*editCanvas.gameObject.SetActive(false);
        OpenExpansionCanvas();*/
    }

    public void CancelBtn()
    {// EditBuildingList - CancelBtn ��ư ������ ��
        SoundManager.Inst.PlaySFX("Click_on");
        RefreshEditList();
    }
    #endregion

    #region ExpansionCanvas
    public void OpenExpansionCanvas()
    {
        expansionCanvas.gameObject.SetActive(true);
        // ���� �����ϴٸ�

        // ���� �Ұ����ϴٸ�

        isControll = false;
    }

    public void RefreshEditBuildingList()
    {
        for (int i = 0; i < editBuildingList.Length; i++)
        {
            //editBuildingList[i].Refresh(townManager.building[i]);
        }
    }
    #endregion

    #region BuildCanvas
    public int placeNum = 11;

    public void OpenBuildCanvas()
    {
        SoundManager.Inst.PlaySFX("Click_on");
        buildCanvas.gameObject.SetActive(true);
        buildMenu.SetActive(true);
        buildList.SetActive(false);
        btnMenu.SetActive(false);
        RefreshBuild();
        isControll = false;
    }

    public void OpenBuildList()
    {
        buildMenu.SetActive(false);
        buildList.SetActive(true);
        buildCanvas.RefreshList();
    }

    public void BPBtnClick(int number)
    {
        SoundManager.Inst.PlaySFX("Click_on");
        placeNum = number;
        SelectBuilding(townManager.buildingOrder[number]);
        if (townManager.selectedBuilding != null)
        {
            // ���� �̵� ö�� ��� ui����
            btnMenu.SetActive(true);
            btnMenu.transform.position = buttons[placeNum].transform.position + new Vector3(0, -40, 0);
            btnMenu.transform.GetChild(0).gameObject.SetActive(true);
            btnMenu.transform.GetChild(1).gameObject.SetActive(true);
            btnMenu.transform.GetChild(2).gameObject.SetActive(true);
            btnMenu.transform.GetChild(3).gameObject.SetActive(false);
        }
        else
        {
            // �Ǽ� ��� ui����
            btnMenu.SetActive(true);
            btnMenu.transform.position = buttons[placeNum].transform.position + new Vector3(0, -40, 0);
            btnMenu.transform.GetChild(0).gameObject.SetActive(false);
            btnMenu.transform.GetChild(1).gameObject.SetActive(false);
            btnMenu.transform.GetChild(2).gameObject.SetActive(false);
            btnMenu.transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    public void BuildBtnClick()
    {
        switch (placeNum)
        {
            case 0:
                // ������
                break;
            case 3:
                // ������
                break;
            case 7:
                // ä����
                break;
            case 10:
                // �갣
                break;
            default:
                // �Ǽ� ����Ʈ
                break;
        }
    }

    public void BtnMenuExpansion()
    {// ���� ��ư

    }

    public void BtnMenuMove()
    {// �̵� ��ư

    }

    public void BtnMenuDemolition()
    {// ö�� ��ư

    }

    public void BtnMenuBuild()
    {// �Ǽ� ��ư
        SoundManager.Inst.PlaySFX("Click_on");
        OpenBuildList();
        btnMenu.SetActive(false);
    }

    public void BtnMenuCancel()
    {
        SoundManager.Inst.PlaySFX("Click_on");
        btnMenu.SetActive(false);
    }

    [SerializeField]
    private TextMeshProUGUI tutorialText;

    public void BuildBtn()
    {
        SoundManager.Inst.PlaySFX("Click_on");
        GameManager.Inst.PlayerInfo.gold -= townManager.selectedBuilding.needGold;
        GameManager.Inst.PlayerInfo.ston -= townManager.selectedBuilding.needManaStone;
        resource.Refresh();
        townManager.BuildBuilding(townManager.selectedBuilding, placeNum);
        buildCanvas.gameObject.SetActive(false);
        isControll = true;

        tutorialText.text = "��� �ǹ��� Ŭ���Ͽ� �����ϼ���.";
    }

    public void RefreshBuild()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] != null && secondButtons[i] != null)
            {
                if(townManager.buildingOrder[i] != null)
                {
                    buttons[i].gameObject.SetActive(false);
                    secondButtons[i].gameObject.SetActive(true);
                }
                else
                {
                    buttons[i].gameObject.SetActive(true);
                    secondButtons[i].gameObject.SetActive(false);
                }
            }
        }
    }
    #endregion

    #region MenuCanvas
    public void OpenMenuCanvas()
    {
        SoundManager.Inst.PlaySFX("Click_on");
        menuCanvas.gameObject.SetActive(true);
        isControll = false;
    }
    #endregion

    public void SelectBuilding(Building building = null)
    {
        townManager.selectedBuilding = building;
    }
}
