using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickAdvenScript : MonoBehaviour
{
    public GameObject panels; // �гε��� ������ �迭;
    [SerializeField]
    public Button AdvenBtn;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HidePanel();
        }
        AdvenBtn.onClick.AddListener(ShowPanel);
    }


    public void ShowPanel()
    {
        panels.SetActive(true);
    }

    private void HidePanel()
    {
        panels.SetActive(false);
    }
}
