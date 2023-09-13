using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using JinWon;

namespace JinWon
{
    public class MapUI : MonoBehaviour
    {

        [SerializeField]
        private GameObject calendarCam;

        [SerializeField]
        private List<GameObject> regionCamList = new List<GameObject>();
        [SerializeField]
        private GameObject mapCam;

        [SerializeField]
        private List<GameObject> goBackBtnList = new List<GameObject>();

        [SerializeField]
        private GameObject castleStage;

        [SerializeField]
        private GameObject calendarMove; // 달력으로 이동하는 버튼

        [SerializeField]
        public List<Button> regionButtonList = new List<Button>(); // 마왕성 협곡 설산 늪 화산 사막 숲 개척지
        private int regionButtonSelect;

        private bool selectMove; // 셀렉트 이동 가능 여부!
        public bool SelectMove
        {
            set { selectMove = value; }
        }


        [SerializeField]
        private JinWon.CalendarScene calendarScene;

        [SerializeField]
        private List<RegionBtn> regionBtnList = new List<RegionBtn>();
        [SerializeField]
        private List<GameObject> regionLoadList = new List<GameObject>();

        [SerializeField]
        private GameObject forestCloud;

        public void Init() // CalendarScene 시작할때 초기화!
        {
            regionCamList[0].SetActive(true);
            //mapCam.SetActive(true);

            for (int i = 0; i < goBackBtnList.Count; i++) // 지역 뒤로가기 버튼 끄기
            {
                goBackBtnList[i].SetActive(false);
            }
            for(int i = 0; i < regionCamList.Count; i++) // 지역카메라 끄기
            {
                regionCamList[i].SetActive(false);
            }

            RegionStageInit();
            RegionBtnInit();

            selectMove = false;

            /*regionButtonSelect = 6;
            regionButtonList[regionButtonSelect].Select();
            regionBtnList[1].ForestPointActive();*/

        }

        private void RegionBtnInit()
        {
            for(int i = 0; i < regionBtnList.Count; i++)
            {
                regionBtnList[i].Init();
            }
        }

        public void RegionInit()
        {
            regionButtonList[6].Select(); // 개척지로 셀렉트 해놓기
            selectMove = true; // 이동가능한 상태로 초기화
            regionButtonSelect = 6;
            regionBtnList[1].ForestPointActive();
            //Invoke("CalendarMoveBtnSetActive", 2.0f); // 달력으로 이동하는 버튼 키키

        }

        private void RegionStageInit() // 지역스테이지 초기화;
        {
            castleStage.SetActive(false);
        }

        private void Update()
        {
            if(selectMove)
            {
                if(Input.GetKeyDown(KeyCode.RightArrow))
                {
                    switch(regionButtonSelect)
                    {
                        case 6: 
                            {
                                regionButtonSelect = 0;
                                break;
                            }
                    }
                    regionButtonList[regionButtonSelect].Select();
                }
                if(Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    switch(regionButtonSelect)
                    {
                        case 0:
                            {
                                regionButtonSelect = 6;
                                break;
                            }
                    }
                    regionButtonList[regionButtonSelect].Select();
                }
                
            }

            /*// 방향키 입력으로 셀렉트 이동
            if (selectMove) // 이동이 가능한 상태일때만!
            {
                if (Input.GetKeyDown(KeyCode.RightArrow)) // 오른쪽 방향키 눌렀을때!
                {
                    switch (regionButtonSelect)
                    {
                        case 0:
                            {
                                regionButtonSelect = 1;
                                break;
                            }
                        case 1:
                            {
                                regionButtonSelect = 2;
                                break;
                            }
                        case 2 or 3 or 5:
                            {
                                regionButtonSelect = 6;
                                break;
                            }
                        case 4:
                            {
                                regionButtonSelect = 5;
                                break;
                            }
                        case 6:
                            {
                                regionButtonSelect = 7;
                                break;
                            }
                    }

                    regionButtonList[regionButtonSelect].Select();
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow)) // 왼쪽 방향키를 눌렀을때!
                {
                    switch (regionButtonSelect)
                    {
                        case 1:
                            {
                                regionButtonSelect = 0;
                                break;
                            }
                        case 2 or 3 or 4:
                            {
                                regionButtonSelect = 1;
                                break;
                            }
                        case 5:
                            {
                                regionButtonSelect = 4;
                                break;
                            }
                        case 6:
                            {
                                regionButtonSelect = 2;
                                break;
                            }
                        case 7:
                            {
                                regionButtonSelect = 6;
                                break;
                            }
                    }
                    regionButtonList[regionButtonSelect].Select();
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow)) // 위 방향키를 눌렀을때!
                {
                    switch (regionButtonSelect)
                    {
                        case 1:
                            {
                                regionButtonSelect = 2;
                                break;
                            }
                        case 3 or 6:
                            {
                                regionButtonSelect = 2;
                                break;
                            }
                        case 4 or 5:
                            {
                                regionButtonSelect = 3;
                                break;
                            }
                        case 7:
                            {
                                regionButtonSelect = 6;
                                break;
                            }
                    }
                    regionButtonList[regionButtonSelect].Select();
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow)) // 위 방향키를 눌렀을때!
                {
                    switch (regionButtonSelect)
                    {
                        case 1:
                            {
                                regionButtonSelect = 4;
                                break;
                            }
                        case 2:
                            {
                                regionButtonSelect = 3;
                                break;
                            }
                        case 3:
                            {
                                regionButtonSelect = 4;
                                break;
                            }
                        case 6:
                            {
                                regionButtonSelect = 5;
                                break;
                            }
                        case 7:
                            {
                                regionButtonSelect = 6;
                                break;
                            }
                    }
                    regionButtonList[regionButtonSelect].Select();
                }
            }*/


        }

        public void RegionOnClick(string region) // 지역 눌렀을때 해당지역으로 확대되는 함수
        {
            if (mapCam.activeSelf)
            {
                switch (region)
                {
                    case "Forest": // 숲
                        {
                            // 임시 번호
                            regionCamList[1].SetActive(true);
                            //forestCloud.SetActive(true);
                            LeanTween.scale(forestCloud, new Vector3(1, 1, 1), 1f);
                            Debug.Log("포레스트 눌러씀");

                            if (regionButtonSelect != 6)
                                regionButtonList[6].Select();

                            Invoke("RegionLoadOn", 2f);
                            //regionLoadList[0].SetActive(true);

                            break;
                        }
                    case "castle": // 성
                        {
                            regionCamList[0].SetActive(true);
/*                            goBackBtnList[0].SetActive(true);
                            castleStage.SetActive(true);*/

                            if (regionButtonSelect != 0) // 마왕성으로 셀렉트가 안되어있으면 셀렉트 시키기
                                regionButtonList[0].Select();
                            //  EventSystem.current.SetSelectedGameObject(regionButtonList[0].gameObject);
                            break;
                        }
                }

                RegionBtnInteractable(false);

                selectMove = false; // 셀렉트 변경 못하도록 꺼놓기.
                mapCam.SetActive(false);
                calendarScene.SelectStep++;
            }
        }

        public void RegionLoadOn()
        {
            for (int i = 0; i < regionLoadList.Count; i++)
            {
                if (!regionLoadList[i].activeSelf)
                {
                    regionLoadList[i].SetActive(true);
                }
            }
        }

        public void RegionLoadOff() // 지역의 길을 끄는 함수
        {
            for(int i = 0; i < regionLoadList.Count; i++)
            {
                if(regionLoadList[i].activeSelf)
                {
                    regionLoadList[i].SetActive(false);
                }
            }
            if (forestCloud.transform.localScale == Vector3.one)
                LeanTween.scale(forestCloud, new Vector3(0, 0, 0), 1f);

            /*if (forestCloud.activeSelf)
                forestCloud.SetActive(false);*/
        }

        private string battleSceneName;

        /*public void BattleSceneMove(string region, int stage) // 전투씬으로 가는 함수!
        {
            battleSceneName = region + stage.ToString();
            SceneManager.LoadSceneAsync(battleSceneName);
            //GameManager.Inst.AsyncLoadNextScene(SceneName.nextSceneName);
        }*/

        public void CastleSceneMove(int stage) // 전투씬으로 가는 함수!
        {
            battleSceneName = "Castle" + stage.ToString();
            SceneManager.LoadSceneAsync(battleSceneName);
            //GameManager.Inst.AsyncLoadNextScene(SceneName.nextSceneName);
        }

        public void RegionBtnInteractable(bool OnOff) // 지역버튼을 키고끄는 함수
        {
            if(OnOff)
            {
                for (int i = 0; i < regionButtonList.Count; i++)
                    regionButtonList[i].interactable = true;
            }
            else
            {
                for (int i = 0; i < regionButtonList.Count; i++)
                    regionButtonList[i].interactable = false;
            }
        }

        public void RegionCamActive(bool OnOff) // 지역캠을 키고끄는 함수
        {
            if (OnOff)
            {
                for(int i = 0; i < regionCamList.Count; i++)
                {
                    if(!regionCamList[i].activeSelf)
                        regionCamList[i].SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < regionCamList.Count; i++)
                {
                    if (regionCamList[i].activeSelf)
                        regionCamList[i].SetActive(false);
                }
            }
        }

        private void MapUIActive()
        {
            gameObject.SetActive(false);
        }

        [SerializeField]
        private GameObject calendarUIObj;

        public void CalendarMoveBtnSetActive() // 달력으로 되돌아가는 버튼 활성화 코드
        {
            calendarMove.SetActive(true);
            calendarUIObj.SetActive(false);
        }
    }
}

