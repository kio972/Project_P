using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditBuildingList : MonoBehaviour
{
    [SerializeField] private int number;
    [SerializeField] private Image myImage;
    [SerializeField] private Image buildingImg;
    [SerializeField] private Text buildingText;
    [SerializeField] private Sprite basicImg;
    [SerializeField] private Sprite clickImg;
    [SerializeField] private GameObject buttonGroup;

    public void Refresh(Building building)
    {
        ClickEditCancel();
        buildingImg.sprite = building.image;
        buildingText.text = "Lv." + building.level + " / " + building.name;
    }

    public void ClickEditList()
    {
        buildingText.gameObject.SetActive(false);
        myImage.sprite = clickImg;
        buttonGroup.SetActive(true);
    }

    public void ClickEditCancel()
    {
        buildingText.gameObject.SetActive(true);
        myImage.sprite = basicImg;
        buttonGroup.SetActive(false);
    }
}
