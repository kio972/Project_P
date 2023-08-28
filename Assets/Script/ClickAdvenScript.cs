using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAdvenScript : MonoBehaviour
{
    public GameObject[] panels; // �гε��� ������ �迭
    private int currentPanelIndex = 0; // ���� �г��� �ε���

    private void Start()
    {
        ShowPanelAtIndex(currentPanelIndex);
    }

    public void OnNextButtonClicked()
    {
        HidePanelAtIndex(currentPanelIndex); // ���� �г� �����

        currentPanelIndex = (currentPanelIndex + 1) % panels.Length; // ���� �г� �ε����� ����
        ShowPanelAtIndex(currentPanelIndex); // ���� �г� ���̱�
    }

    private void ShowPanelAtIndex(int index)
    {
        panels[index].SetActive(true);
    }

    private void HidePanelAtIndex(int index)
    {
        panels[index].SetActive(false);
    }
}