using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jun
{
    public class ChangeSubPanel : MonoBehaviour
    {
        public GameObject[] panels; // �гε��� ������ �迭
        private static int currentSubPanelIndex = 0; // ���� �г��� �ε���

        private void Start()
        {
            ShowPanelAtIndex(currentSubPanelIndex);
        }

        public void OnNextButtonClicked()
        {
            HidePanelAtIndex(currentSubPanelIndex); // ���� �г� �����
            currentSubPanelIndex = (currentSubPanelIndex + 1) % panels.Length; // ���� �г� �ε����� ����
            ShowPanelAtIndex(currentSubPanelIndex); // ���� �г� ���̱�
        }

        public void OnPrevButtonClicked()
        {
            HidePanelAtIndex(currentSubPanelIndex); // ���� �г� �����
            currentSubPanelIndex = (currentSubPanelIndex - 1 + panels.Length) % panels.Length; // ���� �г� �ε����� ����
            ShowPanelAtIndex(currentSubPanelIndex); // ���� �г� ���̱�
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
}

