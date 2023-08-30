using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;

namespace JinWon
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> buttonList = new List<GameObject>();

        [SerializeField]
        private GameObject dataChoiceUI;
        [SerializeField]
        private GameObject dataInfoUI;
        [SerializeField]
        private GameObject deleteUI;
        [SerializeField]
        private GameObject refreshUI;


        [SerializeField]
        private FadeInOut fade; 

        public void Init()
        {
            DataInit();
        }

        public void DataInit() // 기존의 데이터가 있는지 확인하는 함수
        {
            dataChoiceUI.SetActive(false);
            dataInfoUI.SetActive(false);
            refreshUI.SetActive(false);
            // 있으면 키고 없으면 끄기
            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].SetActive(true);
            }
            buttonList[0].SetActive(false); // 임시로 끔
        }

        #region 메인메뉴 Button_Click함수
        public void Continue_Click()
        {
            Debug.Log("아직 구현 안됐습니다!!");
            // 저장된 곳으로 가야 함! 현재 플레이 데이터가 없는 경우는 표시하지 않아야 함!
        }

        public void Refresh_Click()
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].SetActive(false);
            }
            refreshUI.SetActive(true);
            // 저장을 원하는 데이터슬롯을 선택 후 캘린더씬 바로 넘어가기.
            // 하면 시나리오 후 캘린더 시스템이 출력된 후 튜토리얼이 진행되야 함!
        }

        public void RefreshStart_Click()
        {
            fade.Fade_InOut(false, 3.0f); // 3초 Fade하고 넘어감
            Invoke("NextScene", 3.0f);
        }

        public void DataSelection_Click()
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].SetActive(false);
            }
            dataChoiceUI.SetActive(true);
        }

        public void Option_Click()
        {
            Debug.Log("아직 구현 안됐습니다!!");
            // 게임의 시스템 옵션을 변경! 추후 추가될 예정.
        }
        #endregion

        #region 데이터 초이스 UI
        public void DataButton_Click(int num) // 데이터 정보를 띄움
        {
            // 텍스트들을 수정시키고 띄어야 함!!
            dataInfoUI.SetActive(true);
        }

        public void Goback_Click()
        {
            DataInit();
        }

        public void Delete_Click()
        {
            dataChoiceUI.SetActive(false);
            dataInfoUI.SetActive(false);
            deleteUI.SetActive(true);
        }
        #endregion

        #region 데이터 정보 UI
        public void Load_Click() // 달력씬으로 넘어가야 함!
        {
            fade.Fade_InOut(false, 3.0f); // 3초 Fade하고 넘어감
            Invoke("NextScene", 3.0f);
        }

        public void NextScene()
        {
            GameManager.Inst.AsyncLoadNextScene(SceneName.CalendarScene);
        }

        public void Cancel_Click()
        {
            dataInfoUI.SetActive(false);
        }
        #endregion

        #region 데이터 삭제 UI
        public void DeleteCancel()
        {
            deleteUI.SetActive(false);
            dataChoiceUI.SetActive(true);
        }

        public void LastDelete()
        {
            // 최종적으로 삭제하는 코드 들어가야 함!
        }

        #endregion
    }
}

