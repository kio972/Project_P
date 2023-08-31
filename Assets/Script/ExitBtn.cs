using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jun
{
    public class ExitBtn : MonoBehaviour
    {
        public GameObject panel; // 단일 패널을 저장할 변수

        private void Start()
        {
            panel.SetActive(true); // 게임 시작 시 패널을 활성화
        }

        private void Update()
        {
            // ESC 키를 누를 때 현재 오브젝트 비활성화
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameObject.SetActive(false);
            }
        }

        public void OnHideButtonClicked()
        {
            panel.SetActive(false); // 패널 비활성화
        }
    }
}

