using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jun
{
    public class ExploreScript : MonoBehaviour
    {
        public List<Button> buttons; // 버튼을 저장할 리스트
        private int currentButtonIndex = 0; // 현재 버튼의 인덱스
        private Vector3[] buttonPositions; // 각 버튼의 초기 위치 저장

        private void Start()
        {
            // 버튼 위치 초기화 및 저장
            buttonPositions = new Vector3[buttons.Count];
            for (int i = 0; i < buttons.Count; i++)
            {
                buttonPositions[i] = buttons[i].transform.position;
            }
        }

        public void OnNextButtonClicked()
        {
            if (currentButtonIndex == 3)
            {
                buttons[currentButtonIndex].gameObject.SetActive(false); // 현재 버튼 비활성화
                currentButtonIndex = (currentButtonIndex) % buttons.Count; // 다음 버튼 인덱스로 변경
                buttons[currentButtonIndex].gameObject.SetActive(true); // 다음 버튼 활성화
            }
            else
            {
                buttons[currentButtonIndex].gameObject.SetActive(false); // 현재 버튼 비활성화
                buttons[currentButtonIndex + 1].gameObject.SetActive(false);
                if(currentButtonIndex == 2)
                {
                    currentButtonIndex = (currentButtonIndex + 1) % buttons.Count; // 다음 버튼 인덱스로 변경
                }
                else
                {
                    currentButtonIndex = (currentButtonIndex + 2) % buttons.Count; // 다음 버튼 인덱스로 변경
                }
                
                buttons[currentButtonIndex].gameObject.SetActive(true); // 다음 버튼 활성화
                buttons[currentButtonIndex + 1].gameObject.SetActive(true);
            }
            

            UpdateButtonPositions();
        }

        public void OnPreviousButtonClicked()
        {
            buttons[currentButtonIndex+2].gameObject.SetActive(false); // 현재 버튼 비활성화
            buttons[currentButtonIndex+3].gameObject.SetActive(false);
            currentButtonIndex = (currentButtonIndex + buttons.Count - 2) % buttons.Count; // 이전 버튼 인덱스로 변경
            buttons[currentButtonIndex - 1].gameObject.SetActive(true); // 이전 버튼 활성화
            buttons[currentButtonIndex - 2].gameObject.SetActive(true);

            UpdateButtonPositions();
        }

        private void UpdateButtonPositions()
        {
            int numButtons = buttons.Count;

            for (int i = 0; i < numButtons; i++)
            {
                int offset = i - currentButtonIndex;
                if (offset < 0) offset += numButtons;

                buttons[i].transform.position = buttonPositions[offset];
            }
        }
    }
}
