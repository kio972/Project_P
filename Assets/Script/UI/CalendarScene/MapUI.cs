using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        public List<Button> regionButtonList = new List<Button>(); // 마왕성 협곡 설산 늪 화산 사막 숲 개척지
        private int regionButtonSelect;

        private bool selectMove; // 셀렉트 이동 가능 여부!

        public void Init() // CalendarScene 시작할때 초기화!
        {
            regionCamList[0].SetActive(true);
            mapCam.SetActive(true);

            for (int i = 0; i < goBackBtnList.Count; i++) // 지역 뒤로가기 버튼 끄기
            {
                goBackBtnList[i].SetActive(false);
            }
            for(int i = 0; i < regionCamList.Count; i++) // 지역카메라 끄기
            {
                regionCamList[i].SetActive(false);
            }

            RegionStageInit();

            selectMove = false;
        }

        public void RegionInit()
        {
            regionButtonList[7].Select(); // 개척지로 셀렉트 해놓기
            selectMove = true; // 이동가능한 상태로 초기화
            regionButtonSelect = 7;
            Invoke("CalendarMoveBtnSetActive", 2.0f); // 달력으로 이동하는 버튼 키키

        }

        private void RegionStageInit() // 지역스테이지 초기화;
        {
            castleStage.SetActive(false);
        }

        private void Update()
        {
            // 방향키 입력으로 셀렉트 이동
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
            }
        }

        public void RegionOnClick(string region) // 지역 눌렀을때 해당지역으로 확대되는 함수
        {
            if (mapCam.activeSelf)
            {
                switch (region)
                {
                    case "castle":
                        {
                            regionCamList[0].SetActive(true);
                            goBackBtnList[0].SetActive(true);
                            castleStage.SetActive(true);

                            // 연속으로 누르지 못하도록, 다른지역을 누르지 못하도록 잠시 꺼주기
                            for (int i = 0; i < regionButtonList.Count; i++) 
                                regionButtonList[i].interactable = false;

                            if (regionButtonSelect != 0) // 마왕성으로 셀렉트가 안되어있으면 셀렉트 시키기
                                regionButtonList[0].Select();
                            //  EventSystem.current.SetSelectedGameObject(regionButtonList[0].gameObject);

                            break;
                        }
                }
                selectMove = false; // 셀렉트 변경 못하도록 꺼놓기.
                Debug.Log(selectMove);
                mapCam.SetActive(false);
            }
        }

        public void GobackOnClick(string region) // 지역 확대 상태에서 뒤로가는 함수!
        {
            switch (region)
            {
                case "castle":
                    {
                        mapCam.SetActive(true);
                        goBackBtnList[0].SetActive(false);
                        castleStage.SetActive(false);
                        
                        break;
                    }
            }
            for (int i = 0; i < regionButtonList.Count; i++)
                regionButtonList[i].interactable = true; // 다시 켜주기
            selectMove = true;
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

        public void CalendarMove() // 달력으로 되돌아가는 코드
        {
            calendarUIObj.SetActive(true);
            if (!mapCam.activeSelf) // 마왕성 확대중에 달력가는 코드를 눌렀을때를 방지하는 임시코드
                GobackOnClick("castle");

            calendarCam.SetActive(true);
            calendarMove.SetActive(false);
            Invoke("MapUIActive", 2.0f);
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

