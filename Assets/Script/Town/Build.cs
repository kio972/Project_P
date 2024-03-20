using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JinWon;

public class Build : MonoBehaviour
{
    [SerializeField] private TownManager townManager;
    [SerializeField] private TownUI townUI;
    [SerializeField] private Image BI;
    [SerializeField] private Text NRGold;
    [SerializeField] private Text NRMana;
    [SerializeField] private Text EX;
    [SerializeField] private BuildBuildingList[] buildingList;
    [SerializeField] private Button build;

    public void Init()
    {
        for (int i = 0; i < townManager.building.Length; i++)
            buildingList[i].building = townManager.building[i];
    }

    public void RefreshList()
    {
        BI.gameObject.SetActive(true);
        NRGold.gameObject.SetActive(true);
        NRMana.gameObject.SetActive(true);
        EX.gameObject.SetActive(true);
        build.interactable = false;
        for (int i = 0; i < buildingList.Length; i++)
            buildingList[i].Refresh();
    }

    public void ChooseBuild(int number)
    {
        SoundManager.Inst.PlaySFX("Click_on");
        BI.gameObject.SetActive(true);
        NRGold.gameObject.SetActive(true);
        NRMana.gameObject.SetActive(true);
        EX.gameObject.SetActive(true);
        build.interactable = true;
        townUI.SelectBuilding(townManager.building[number]);
        BI.sprite = townManager.building[number].image;
        NRGold.text = GameManager.Inst.PlayerInfo.gold.ToString() + " / " + townManager.building[number].needGold.ToString();
        NRMana.text = GameManager.Inst.PlayerInfo.ston.ToString() + " / " + townManager.building[number].needManaStone.ToString();
        EX.text = townManager.building[number].explan;
        if (GameManager.Inst.PlayerInfo.gold >= townManager.building[number].needGold && GameManager.Inst.PlayerInfo.ston >= townManager.building[number].needManaStone)
        {
            build.GetComponent<Image>().color = Color.white;
            build.interactable = true; // 자원이 충분하면 건설버튼 활성화
        }
        else
        {
            build.GetComponent<Image>().color = Color.gray;
            build.interactable = false; // 자원이 모자라면 건설버튼 비활성화
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SoundManager.Inst.PlaySFX("Click_on");
            townUI.isControll = true;
            gameObject.SetActive(false);
        }
    }
}
