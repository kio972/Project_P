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

        private int selectStep; // ����Ʈ �ܰ� ( �ڷΰ��� �Ҷ� Ȱ�� )
        public int SelectStep // RegionBtn ��ũ��Ʈ���� ���!
        {
            get { return selectStep; }
        }


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
            selectStep = 0;
        }

        public void RegionInit()
        {
            regionButtonList[6].Select(); // ��ô���� ����Ʈ �س���
            selectMove = true; // �̵������� ���·� �ʱ�ȭ
            regionButtonSelect = 6;
            Invoke("CalendarMoveBtnSetActive", 2.0f); // �޷����� �̵��ϴ� ��ư ŰŰ

        }

        private void RegionStageInit() // ������������ �ʱ�ȭ;
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

            /*// ����Ű �Է����� ����Ʈ �̵�
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
            }*/


        }

        public void RegionOnClick(string region) // ���� �������� �ش��������� Ȯ��Ǵ� �Լ�
        {
            if (mapCam.activeSelf)
            {
                switch (region)
                {
                    case "Forest": // ��
                        {
                            // �ӽ� ��ȣ
                            regionCamList[1].SetActive(true);
                            Debug.Log("������Ʈ ������");

                            if (regionButtonSelect != 6)
                                regionButtonList[6].Select();

                            break;
                        }
                    case "castle": // ��
                        {
                            regionCamList[0].SetActive(true);
/*                            goBackBtnList[0].SetActive(true);
                            castleStage.SetActive(true);*/

                            if (regionButtonSelect != 0) // ���ռ����� ����Ʈ�� �ȵǾ������� ����Ʈ ��Ű��
                                regionButtonList[0].Select();
                            //  EventSystem.current.SetSelectedGameObject(regionButtonList[0].gameObject);

                            break;
                        }
                }

                // �������� ������ ���ϵ���, �ٸ������� ������ ���ϵ��� ��� ���ֱ�
                for (int i = 0; i < regionButtonList.Count; i++) 
                    regionButtonList[i].interactable = false;

                selectMove = false; // ����Ʈ ���� ���ϵ��� ������.
                Debug.Log(selectMove);
                mapCam.SetActive(false);
                selectStep += 1;
            }
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
            switch(selectStep)
            {
                case 0: // �޷����� ������ ����
                    {
                        calendarUIObj.SetActive(true);
                        /*if (!mapCam.activeSelf) // ���ռ� Ȯ���߿� �޷°��� �ڵ带 ���������� �����ϴ� �ӽ��ڵ�
                            GobackOnClick("castle");*/

                        calendarCam.SetActive(true);
                        calendarMove.SetActive(false);
                        
                        Invoke("MapUIActive", 2.0f);
                        break;
                    }
                case 1: // ���� Ȯ�밡 Ǯ���� ����
                    {
                        selectStep -= 1;
                        mapCam.SetActive(true);

                        // ���� Ȯ�븦 Ǯ�� ����ķ�� ������ ������ ��� ��������.
                        for (int i = 0; i < regionCamList.Count; i++)
                        {
                            if (regionCamList[i].activeSelf)
                            {
                                regionCamList[i].SetActive(false);
                            }
                        }
                        break;
                    }
            }
            for (int i = 0; i < regionButtonList.Count; i++)
                regionButtonList[i].interactable = true; // �ٽ� ���ֱ�

            selectMove = true;

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

