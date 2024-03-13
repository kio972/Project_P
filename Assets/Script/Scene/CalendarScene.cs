using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JinWon;
using UnityEngine.SceneManagement;
using YeongJun;
using TMPro;

namespace JinWon
{
    public class CalendarScene : MonoBehaviour
    {
        private int whatMonth; // 임시 Month변수 입니다.

        [SerializeField]
        private List<GameObject> calendarObjList = new List<GameObject>();

        /*public Texture2D cursorTexture;
        public Vector2 hotspot = Vector2.zero;
        public CursorMode cursorMode = CursorMode.Auto;*/

        [SerializeField]
        private JinWon.MapUI mapUI;

        [SerializeField]
        private GameObject calendarCam;
        [SerializeField]
        private GameObject mapCam;
        [SerializeField]
        private GameObject charInfoCam;
        [SerializeField]
        private GameObject maintenanceCam;

        [SerializeField]
        private GameObject calendarMove; // 달력으로 이동하는 버튼

        private int selectStep; // 셀렉트 단계 ( 뒤로가기 할때 활용 )
        public int SelectStep // RegionBtn 스크립트에서 사용!
        {
            get { return selectStep; }
            set { selectStep = value; }
        }

        [SerializeField]
        private List<GameObject> selectPointList = new List<GameObject>();

        [SerializeField]
        private List<Button> moveBtnList = new List<Button>();

        [SerializeField]
        private List<GameObject> regionCamList = new List<GameObject>(); // 포레스트
        [SerializeField]
        private List<Region> regionList = new List<Region>(); // 포레스트

        [SerializeField]
        private Clock clock;

        private int selectStage;
        public int SelectStage
        {
            get { return selectStage; }
            set { selectStage = value; }
        }

        [SerializeField]
        private GameObject townGo;

        void Start()
        {
            SoundManager.Inst.ChangeBGM(BGM_Type.BGM_Calendar);
            GameManager.Inst.Fade_InOut(true, 3.0f);
            mapMove = false;
<<<<<<< HEAD
            maintenanceMove = false;
            Cursor.SetCursor(cursorTexture, hotspot, cursorMode);
=======
            /*Cursor.SetCursor(cursorTexture, hotspot, cursorMode);*/
>>>>>>> Jun
            //StartCoroutine(Production());
            if (GameManager.Inst.CalendarProd)
                StartCoroutine(Production());
            else
                Init();
        }

        private void Init()
        {
            if(!calendarCam.activeSelf)
                calendarCam.SetActive(true);

            mapUI.Init();
            if(townGo.activeSelf)
                townGo.SetActive(false);

            whatMonth = Random.Range(0, 12);
            calendarObjList[whatMonth].SetActive(true);
            Debug.Log("몇월일까아아아용? " + (whatMonth + 1));

            selectStep = 0;

            for (int i = 0; i < regionList.Count; i++)
            {
                regionList[i].CloudeActive(false);
                regionList[i].LoadActive(false);
            }
            mapMove = true;
            maintenanceMove = true;
        }

        IEnumerator Production()
        {
            calendarMove.SetActive(false);
            GameManager.Inst.CalendarProd = false;
            calendarCam.SetActive(false);
            regionCamList[GameManager.Inst.CalendarProdRegion - 1].SetActive(true);
            yield return YieldInstructionCache.WaitForSeconds(4f);
            regionList[GameManager.Inst.CalendarProdRegion - 1].CloudInit(GameManager.Inst.CalendarProdCloud);
            yield return YieldInstructionCache.WaitForSeconds(4f);
            calendarCam.SetActive(true);
            regionCamList[GameManager.Inst.CalendarProdRegion - 1].SetActive(false);
            yield return YieldInstructionCache.WaitForSeconds(3f);
            //clock.ChangeTime();

            townGo.SetActive(true);

            //Invoke("SystemUiTitle", 2.0f);
            //Init();
        }

        [SerializeField]
        private SystemUI systemUI;

        private void SystemUiTitle()
        {
            systemUI.SystemUiTitle();
        }

        public void UIMove()
        {
            
            switch (selectStep)
            {
                case 0: // 타이틀씬으로 가지는 상태
                    {
                        SoundManager.Inst.PlaySFX("Click_on");
                        //Invoke("TitleSceneMove", 2.0f);
                        TitleSceneMove();
                        break;
                    }

                case 1: // 달력으로 가지는 상태
                    {
                        SoundManager.Inst.PlaySFX("Click_on");
                        MoveBtnFalse();
                        calendarCam.SetActive(true);

                        mapUI.SelectMove = false;
                        selectStep--;
                        mapUI.RegionBtnInteractable(false);
                        Invoke("MoveBtnTrue", 2.0f);
                        mapCam.SetActive(false);
                        maintenanceCam.SetActive(false);
                        break;
                    }
                case 2: // 지역 확대가 풀리는 상태
                    {
                        SoundManager.Inst.PlaySFX("Click_on");
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
                case 3: // 지역으로 가지는 상태
                    {
                        MapUiMove();
                        break;
                    }
                default:
                    {
                        Debug.Log(selectStep + " 지정된 case문이 없습니다.");
                        break;
                    }
            }
        }

        private bool mapMove;

        public void MapUiMove()
        {
            SoundManager.Inst.PlaySFX("Click_on");
            if (mapMove)
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
        }

        private bool maintenanceMove;

        public void MaintenanceMove()
        {
            if(maintenanceMove)
            {
                MoveBtnFalse();
                calendarCam.SetActive(false);
                maintenanceCam.SetActive(true);
                selectStep = 1;
                Invoke("MoveBtnTrue", 2.0f);
            }
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
            SoundManager.Inst.PlaySFX("Click_on");
            GameManager.Inst.AsyncLoadNextScene("TitleScene");
        }

        [SerializeField]
        private GameObject systemUIObj;
        [SerializeField]
        private TextMeshProUGUI systemText;

        public void OffBtn()
        {
            SoundManager.Inst.PlaySFX("Click_off");
            systemText.text = "<color=yellow>" + "아직 " + "</color>" + "활성화 되지 않았습니다.";
            StartCoroutine(SystemUi());
        }

        IEnumerator SystemUi()
        {
            LeanTween.scale(systemUIObj, Vector3.one, 0.5f);
            yield return YieldInstructionCache.WaitForSeconds(2.0f);
            LeanTween.scale(systemUIObj, Vector3.zero, 0.5f);
        }

        [SerializeField]
        private CharInfoObj charInfoObj;

        public void CharInfoUiMove(int stage)
        {
            if(selectStep == 2)
            {
                SoundManager.Inst.PlaySFX("Click_on");
                charInfoObj.SelectCardInfo();
                MoveBtnFalse();
                mapCam.SetActive(true);

                mapUI.RegionCamActive(false); // 지역 확대를 풀때 지역캠이 켜진게 있으면 모두 꺼버리기.
                mapUI.RegionLoadOff(); // 지역 길 끄기
                mapUI.RegionBtnInteractable(false); // 지역 버튼 키기

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

        public void TownSceneMove()
        {
            SoundManager.Inst.PlaySFX("Click_on");
            GameManager.Inst.AsyncLoadNextScene("TownScene");
        }
    }
}
    
