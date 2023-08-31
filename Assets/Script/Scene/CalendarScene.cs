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

        private int whatMonth; // �ӽ� Month���� �Դϴ�.

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
        private GameObject charInfoCam;

        [SerializeField]
        private GameObject calendarMove; // �޷����� �̵��ϴ� ��ư

        private int selectStep; // ����Ʈ �ܰ� ( �ڷΰ��� �Ҷ� Ȱ�� )
        public int SelectStep // RegionBtn ��ũ��Ʈ���� ���!
        {
            get { return selectStep; }
            set { selectStep = value; }
        }

        [SerializeField]
        private List<GameObject> selectPointList = new List<GameObject>();

        [SerializeField]
        private List<Button> moveBtnList = new List<Button>();

        private int selectStage;
        public int SelectStage
        {
            get { return selectStage; }
            set { selectStage = value; }
        }

        void Start()
        {
            mapUI.Init();
            Cursor.SetCursor(cursorTexture, hotspot, cursorMode);

            fade.Fade_InOut(true, 3.0f);
            whatMonth = Random.Range(0, 12);
            calendarObjList[whatMonth].SetActive(true);
            Debug.Log("����ϱ�ƾƾƿ�? " + (whatMonth + 1));

            selectStep = 0;
        }

        public void UIMove()
        {
            switch (selectStep)
            {
                case 0: // Ÿ��Ʋ������ ������ ����
                    {
                        fade.Fade_InOut(false, 2.0f);
                        Invoke("TitleSceneMove", 2.0f);
                        break;
                    }

                case 1: // �޷����� ������ ����
                    {
                        MoveBtnFalse();
                        calendarCam.SetActive(true);

                        mapUI.SelectMove = false;
                        selectStep--;
                        mapUI.RegionBtnInteractable(false);
                        Invoke("MoveBtnTrue", 2.0f);
                        break;
                    }
                case 2: // ���� Ȯ�밡 Ǯ���� ����
                    {
                        MoveBtnFalse();
                        selectStep--;
                        mapCam.SetActive(true);
                        
                        mapUI.RegionCamActive(false); // ���� Ȯ�븦 Ǯ�� ����ķ�� ������ ������ ��� ��������.
                        mapUI.RegionLoadOff(); // ���� �� ����
                        mapUI.RegionBtnInteractable(true); // ���� ��ư Ű��

                        mapUI.SelectMove = true;
                        mapUI.RegionInit();

                        Invoke("MoveBtnTrue", 2.0f);
                        break;
                    }
                case 3: // �������� ������ ����
                    {
                        MapUiMove();
                        break;
                    }
                default:
                    {
                        Debug.Log(selectStep + " ������ case���� �����ϴ�.");
                        break;
                    }
            }
        }

        public void MapUiMove()
        {
            MoveBtnFalse();
            mapUI.RegionInit();
            calendarCam.SetActive(false);
            charInfoCam.SetActive(false);
            mapCam.SetActive(true);

            mapUI.RegionBtnInteractable(true);
            //mapUI.RegionInit();
            mapUI.SelectMove = true;
            selectStep = 1;
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

        public void CharInfoUiMove(int stage)
        {
            if(selectStep == 2)
            {
                MoveBtnFalse();
                mapCam.SetActive(true);

                mapUI.RegionCamActive(false); // ���� Ȯ�븦 Ǯ�� ����ķ�� ������ ������ ��� ��������.
                mapUI.RegionLoadOff(); // ���� �� ����
                mapUI.RegionBtnInteractable(false); // ���� ��ư Ű��

                mapUI.SelectMove = false;
                mapUI.RegionInit();

                selectStep = 3;
                mapCam.SetActive(false);
                charInfoCam.SetActive(true);

                SelectPointOff();
                Invoke("MoveBtnTrue", 2.0f);

                selectStage = stage;
            }
        }

        private void SelectPointOff()
        {
            for(int i = 0; i < selectPointList.Count; i++)
            {
                if(selectPointList[i].activeSelf)
                    selectPointList[i].SetActive(false);
            }
        }
    }
}
    
