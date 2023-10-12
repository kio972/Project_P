using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;

namespace JinWon
{
    public class TitleScene : MonoBehaviour
    {
        [SerializeField]
        private GameObject bg1; // 플레이 기록이 없는 상태의 배경이미지
        [SerializeField]
        private GameObject bg2; // 플레이 기록이 있는 상태의 배경이미지

        [SerializeField]
        private GameObject pressTextObj;

        [SerializeField]
        private GameObject mainMenuObj;

        private bool press;

        [SerializeField]
        private JinWon.MainMenuUI mainMenuUI;

        void Start()
        {
           // GameManager.Inst.Fade_InOut(true, 3.0f);
            Init();
            BGSetting();
        }

        void Update()
        {
            if (Input.anyKeyDown && !press)
            {
                press = true;
                Press();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        private void Init() // 초기 세팅 함수
        {
            press = false;
            bg1.SetActive(false);
            bg2.SetActive(false);
            mainMenuObj.SetActive(false);
            mainMenuUI.Init();
        }

        private void BGSetting() // 배경이미지를 세팅하는 함수
        {
            // 조건으로 플레이 기록이 있는지 확인하고 배경을 변경해줘야 함.
            bg1.SetActive(true); // 임시로 켜줌
        }

        private void Press() // 아무키나 눌렸을때 메인메뉴가 보이는 함수
        {
            pressTextObj.SetActive(false); // 텍스트를 꺼줌
            mainMenuObj.SetActive(true);
            mainMenuUI.DataInit();
        }
    }
}

