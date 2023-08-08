using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Jun
{
    public class ExploreSciprt : MonoBehaviour
    {

        public List<Button> buttons; // 8개의 버튼을 저장할 리스트
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
            buttons[currentButtonIndex].gameObject.SetActive(false); // 현재 버튼 숨기기

            currentButtonIndex = (currentButtonIndex + 1) % buttons.Count; // 다음 버튼 인덱스로 변경
            UpdateButtonPositions();
        }

        private void UpdateButtonPositions()
        {
            buttons[currentButtonIndex].gameObject.SetActive(true); // 다음 버튼 보이도록 설정
            buttons[currentButtonIndex].transform.position = buttonPositions[currentButtonIndex]; // 버튼 위치 업데이트
        }
    }
}

