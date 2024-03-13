using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildBuildingList : MonoBehaviour
{
    public Building building;
    [SerializeField] private Image btnImg;
    [SerializeField] private Button btn;
    [SerializeField] private Image img;
    [SerializeField] private Text text;

    public void Refresh()
    {
        if(building != null)
        {
            img.sprite = building.image;
            text.text = building.name;
            if (building.isBuild)
            {
                btnImg.color = Color.gray;
                btn.interactable = false;
            }
            else
            {
                btnImg.color = Color.white;
                btn.interactable = true;
            }
        }
        else
            gameObject.SetActive(false);
    }
}
