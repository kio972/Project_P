using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JinWon;
using UnityEngine.SceneManagement;

namespace JinWon
{
    public class CalendarScene : MonoBehaviour
    {
        [SerializeField]
        private FadeInOut fade;

        private int whatMonth; // 임시 Month변수 입니다.

        [SerializeField]
        private List<GameObject> calendarObjList = new List<GameObject>();

        public Texture2D cursorTexture;
        public Vector2 hotspot = Vector2.zero;
        public CursorMode cursorMode = CursorMode.Auto;

        [SerializeField]
        private JinWon.MapUI mapUI;

        [SerializeField]
        private GameObject calendarCam;
        [SerializeField]
        private GameObject mapCam;

        [SerializeField]
        private GameObject calendarMove; // 달력으로 이동하는 버튼

        private int selectStep; // 셀렉트 단계 ( 뒤로가기 할때 활용 )
        public int SelectStep // RegionBtn 스크립트에서 사용!
        {
            get { return selectStep; }
            set { selectStep = value; }
        }

        [SerializeField]
        private List<Button> moveBtnList = new List<Button>();

        void Start()
        {
            mapUI.Init();
            Cursor.SetCursor(cursorTexture, hotspot, cursorMode);

            fade.Fade_InOut(true, 3.0f);
            whatMonth = Random.Range(0, 12);
            calendarObjList[whatMonth].SetActive(true);
            Debug.Log("몇월일까아아아용? " + (whatMonth + 1));

            selectStep = 0;
        }

        public void UIMove()
        {
            switch (selectStep)
            {
                case 0: // 타이틀씬으로 가지는 상태
                    {
                        fade.Fade_InOut(false, 2.0f);
                        Invoke("TitleSceneMove", 2.0f);
                        break;
                    }

                case 1: // 달력으로 가지는 상태
                    {
                        MoveBtnFalse();
                        calendarCam.SetActive(true);

                        mapUI.SelectMove = false;
                        selectStep--;
                        mapUI.RegionBtnInteractable(false);
                        Invoke("MoveBtnTrue", 2.0f);
                        break;
                    }
                case 2: // 지역 확대가 풀리는 상태
                    {
                        MoveBtnFalse();
                        selectStep--;
                        mapCam.SetActive(true);
                        
                        mapUI.RegionCamActive(false); // 지역 확대를 풀때 지역캠이 켜진게 있으면 모두 꺼버리기.
                        mapUI.RegionLoadOff(); // 지역 길 끄기
                        mapUI.RegionBtnInteractable(true); // 지역 버튼 키기

                        mapUI.SelectMove = true;
                        mapUI.RegionInit();

                        Invoke("MoveBtnTrue", 2.0f);
                        break;
                    }
                default:
                    {
                        Debug.Log(selectStep + " 지정된 case문이 없습니다.");
                        break;
                    }
            }

        }

        public void MapUiMove()
        {
            MoveBtnFalse();
            mapUI.RegionInit();
            calendarCam.SetActive(false);

            mapUI.RegionBtnInteractable(true);
            mapUI.RegionInit();
            mapUI.SelectMove = true;
            selectStep++;
            Invoke("MoveBtnTrue", 2.0f);
        }

        private void MoveBtnFalse()
        {
            for (int i = 0; i < moveBtnList.Count; i++)
            {
                moveBtnList[i].interactable = false;
            }
        }

        private void MoveBtnTrue()
        {
            for(int i = 0; i < moveBtnList.Count; i++)
            {
                moveBtnList[i].interactable = true;
            }
        }

        private void TitleSceneMove()
        {
            GameManager.Inst.AsyncLoadNextScene(SceneName.TitleScene);
        }
    }
}
    
