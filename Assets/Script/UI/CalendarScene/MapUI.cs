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
        private GameObject calendarMove; // �޷����� �̵��ϴ� ��ư

        public List<Button> regionButtonList = new List<Button>(); // ���ռ� ���� ���� �� ȭ�� �縷 �� ��ô��
        private int regionButtonSelect;

        private bool selectMove; // ����Ʈ �̵� ���� ����!

        public void Init() // CalendarScene �����Ҷ� �ʱ�ȭ!
        {
            regionCamList[0].SetActive(true);
            mapCam.SetActive(true);

            for (int i = 0; i < goBackBtnList.Count; i++) // ���� �ڷΰ��� ��ư ����
            {
                goBackBtnList[i].SetActive(false);
            }
            for(int i = 0; i < regionCamList.Count; i++) // ����ī�޶� ����
            {
                regionCamList[i].SetActive(false);
            }

            RegionStageInit();

            selectMove = false;
        }

        public void RegionInit()
        {
            regionButtonList[7].Select(); // ��ô���� ����Ʈ �س���
            selectMove = true; // �̵������� ���·� �ʱ�ȭ
            regionButtonSelect = 7;
            Invoke("CalendarMoveBtnSetActive", 2.0f); // �޷����� �̵��ϴ� ��ư ŰŰ

        }

        private void RegionStageInit() // ������������ �ʱ�ȭ;
        {
            castleStage.SetActive(false);
        }

        private void Update()
        {
            // ����Ű �Է����� ����Ʈ �̵�
            if (selectMove) // �̵��� ������ �����϶���!
            {
                if (Input.GetKeyDown(KeyCode.RightArrow)) // ������ ����Ű ��������!
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
                else if (Input.GetKeyDown(KeyCode.LeftArrow)) // ���� ����Ű�� ��������!
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
                else if (Input.GetKeyDown(KeyCode.UpArrow)) // �� ����Ű�� ��������!
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
                else if (Input.GetKeyDown(KeyCode.DownArrow)) // �� ����Ű�� ��������!
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

        public void RegionOnClick(string region) // ���� �������� �ش��������� Ȯ��Ǵ� �Լ�
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

                            // �������� ������ ���ϵ���, �ٸ������� ������ ���ϵ��� ��� ���ֱ�
                            for (int i = 0; i < regionButtonList.Count; i++) 
                                regionButtonList[i].interactable = false;

                            if (regionButtonSelect != 0) // ���ռ����� ����Ʈ�� �ȵǾ������� ����Ʈ ��Ű��
                                regionButtonList[0].Select();
                            //  EventSystem.current.SetSelectedGameObject(regionButtonList[0].gameObject);

                            break;
                        }
                }
                selectMove = false; // ����Ʈ ���� ���ϵ��� ������.
                Debug.Log(selectMove);
                mapCam.SetActive(false);
            }
        }

        public void GobackOnClick(string region) // ���� Ȯ�� ���¿��� �ڷΰ��� �Լ�!
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
                regionButtonList[i].interactable = true; // �ٽ� ���ֱ�
            selectMove = true;
        }

        private string battleSceneName;

        /*public void BattleSceneMove(string region, int stage) // ���������� ���� �Լ�!
        {
            battleSceneName = region + stage.ToString();
            SceneManager.LoadSceneAsync(battleSceneName);
            //GameManager.Inst.AsyncLoadNextScene(SceneName.nextSceneName);
        }*/

        public void CastleSceneMove(int stage) // ���������� ���� �Լ�!
        {
            battleSceneName = "Castle" + stage.ToString();
            SceneManager.LoadSceneAsync(battleSceneName);
            //GameManager.Inst.AsyncLoadNextScene(SceneName.nextSceneName);
        }

        public void CalendarMove() // �޷����� �ǵ��ư��� �ڵ�
        {
            calendarUIObj.SetActive(true);
            if (!mapCam.activeSelf) // ���ռ� Ȯ���߿� �޷°��� �ڵ带 ���������� �����ϴ� �ӽ��ڵ�
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

        public void CalendarMoveBtnSetActive() // �޷����� �ǵ��ư��� ��ư Ȱ��ȭ �ڵ�
        {
            calendarMove.SetActive(true);
            calendarUIObj.SetActive(false);
        }
    }
}

