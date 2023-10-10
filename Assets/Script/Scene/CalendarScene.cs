using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JinWon;
using UnityEngine.SceneManagement;
using YeongJun;

namespace JinWon
{
    public class CalendarScene : MonoBehaviour
    {


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

        [SerializeField]
        private List<GameObject> regionCamList = new List<GameObject>(); // ������Ʈ
        [SerializeField]
        private List<Region> regionList = new List<Region>(); // ������Ʈ

        [SerializeField]
        private Clock clock;

        private int selectStage;
        public int SelectStage
        {
            get { return selectStage; }
            set { selectStage = value; }
        }

        void Start()
        {
            GameManager.Inst.Fade_InOut(true, 3.0f);
            mapMove = false;
            Cursor.SetCursor(cursorTexture, hotspot, cursorMode);
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

            whatMonth = Random.Range(0, 12);
            calendarObjList[whatMonth].SetActive(true);
            Debug.Log("����ϱ�ƾƾƿ�? " + (whatMonth + 1));

            selectStep = 0;

            for (int i = 0; i < regionList.Count; i++)
            {
                regionList[i].CloudeActive(false);
                regionList[i].LoadActive(false);
            }
            mapMove = true;
        }

        IEnumerator Production()
        {
            GameManager.Inst.CalendarProd = false;
            calendarCam.SetActive(false);
            regionCamList[GameManager.Inst.CalendarProdRegion - 1].SetActive(true);
            yield return YieldInstructionCache.WaitForSeconds(4f);
            regionList[GameManager.Inst.CalendarProdRegion - 1].CloudInit(GameManager.Inst.CalendarProdCloud);
            yield return YieldInstructionCache.WaitForSeconds(4f);
            calendarCam.SetActive(true);
            regionCamList[GameManager.Inst.CalendarProdRegion - 1].SetActive(false);
            yield return YieldInstructionCache.WaitForSeconds(3f);
            clock.ChangeTime();

            Invoke("SystemUiTitle", 2.0f);
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
                case 0: // Ÿ��Ʋ������ ������ ����
                    {
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

        public ObjHighlight[] btnGroup;
        public ObjHighlight selectedButton;
        [SerializeField]
        private Image highlightImg;
        [SerializeField]
        private Button btn;
        private int highlight = 0;

        public void RefreshHighlight(ObjHighlight highlight)
        {
            for(int i = 0; i < btnGroup.Length; i++)
            {
                if (btnGroup[i] == highlight)
                    this.highlight = i;
            }
            selectedButton = highlight;
            highlightImg.gameObject.SetActive(true);
            highlightImg.sprite = selectedButton.highlightImg;
            highlightImg.rectTransform.sizeDelta = highlight.highlightImg.rect.size;
            highlightImg.transform.position = selectedButton.transform.position;
        }

        private void Update()
        {
            if(selectStep == 3)
            {
                // ���̶���Ʈ �̵� �ֱ�
                if(selectedButton != null)
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if(highlight > 0)
                        {
                            highlight--;
                            if (!btnGroup[highlight].gameObject.activeSelf)
                                highlight--;
                            highlight = Mathf.Clamp(highlight, 0, btnGroup.Length);
                            RefreshHighlight(btnGroup[highlight]);
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if(highlight < btnGroup.Length - 1)
                        {
                            highlight++;
                            if (!btnGroup[highlight].gameObject.activeSelf)
                                highlight++;
                            highlight = Mathf.Clamp(highlight, 0, btnGroup.Length);
                            RefreshHighlight(btnGroup[highlight]);
                        }
                    }
                    if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
                    {
                        if (selectedButton.TryGetComponent<Button>(out btn))
                            btn.onClick.Invoke();
                    }
                }
                else
                    highlightImg.gameObject.SetActive(false);
            }
        }

        private bool mapMove;

        public void MapUiMove()
        {
            if(mapMove)
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

        [SerializeField]
        private CharInfoObj charInfoObj;

        public void CharInfoUiMove(int stage)
        {
            if(selectStep == 2)
            {
                charInfoObj.SelectCardInfo();
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
    
