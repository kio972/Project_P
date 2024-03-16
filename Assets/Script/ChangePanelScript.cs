using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Jun;

public class ChangePanelScript : MonoBehaviour
{
    public GameObject[] panels; // 패널들을 저장할 배열
    
    private static int currentPanelIndex = 0; // 현재 패널의 인덱스
    

    private void Start()
    {
        ShowPanelAtIndex(currentPanelIndex);
    }

    public void OnNextButtonClicked()
    {
        HidePanelAtIndex(currentPanelIndex); // 현재 패널 숨기기
        currentPanelIndex = (currentPanelIndex + 1) % panels.Length; // 다음 패널 인덱스로 변경
        ShowPanelAtIndex(currentPanelIndex); // 다음 패널 보이기
    }

    public void OnPrevButtonClicked()
    {
        HidePanelAtIndex(currentPanelIndex); // 현재 패널 숨기기
        currentPanelIndex = (currentPanelIndex - 1 + panels.Length) % panels.Length; // 이전 패널 인덱스로 변경
        ShowPanelAtIndex(currentPanelIndex); // 이전 패널 보이기
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
