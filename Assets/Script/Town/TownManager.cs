using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;

public class TownManager : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    public Building selectedBuilding; // 선택한 건물

    public Building[] building; // 설치될 건물
    public Building[] buildingOrder = new Building[11]; // 건물 설치 순서
    public GameObject[] buildPlace = new GameObject[11]; // 건물이 설치될 장소
    public GameObject[] sign = new GameObject[11]; // 빈 공간에 세울 표지판
    public bool[] isBuild = new bool[11]; // 장소별 건설 가능 여부

    [SerializeField] private TownCamera townCamera;
    [SerializeField] private TownUI townUI;

    private void Awake()
    {
        GameManager.Inst.Fade_InOut(true, 3.0f);
        SoundManager.Inst.ChangeBGM(BGM_Type.BGM_Town);
        LoadData();
        townUI.Init();
        townCamera.Init();
        for (int i = 0; i < building.Length; i++)
        {
            if (building[i].isBuild)
            {
                buildingOrder[building[i].placeNum] = building[i];
                isBuild[building[i].placeNum] = true;
                building[i].transform.position = buildPlace[building[i].placeNum].transform.position;
                sign[i].transform.position = new Vector3(0, -20, 0);
            }
        }
    }

    public void LoadData()
    {// 마을 입장 시 데이터를 불러오는 함수

    }

    public void BuildBuilding(Building building, int number)
    {// 건물 건설하는 함수
        building.level = 1;
        building.isBuild = true;
        buildingOrder[number] = building;
        building.placeNum = number;
        building.transform.position = buildPlace[number].transform.position;
        sign[number].transform.position = new Vector3(0, -20, 0);
    }

    public void MoveBuilding(Building building,int number)
    {// 건물 움직이는 함수 ( 옮길 건물, 옮길 위치 )
        buildingOrder[building.placeNum] = null;
        buildingOrder[number] = building;
        sign[building.placeNum].transform.position = buildPlace[building.placeNum].transform.position;
        building.placeNum = number;
        building.transform.position = buildPlace[number].transform.position;
        sign[number].transform.position = new Vector3(0, -20, 0);
    }

    private void Update()
    {
        if (townUI.isControll)
        {
            townCamera.PlayerMove();
            if (Input.GetMouseButtonDown(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    SoundManager.Inst.PlaySFX("Click_on");
                    // 건물을 눌렀을 때 UI출력
                    if (hit.transform.TryGetComponent<Building>(out selectedBuilding))
                    {
                        townUI.OpenSelectBuilding(selectedBuilding);
                        townUI.isControll = false;
                    }
                }
            }
            else if (Input.GetKey(KeyCode.N))
            {
                townUI.OpenEditCanvas();
            }
            else if (Input.GetKey(KeyCode.M))
            {
                Debug.Log("M 클릭");
                townUI.OpenBuildCanvas();
            }
            else if (Input.GetKey(KeyCode.B))
                GameManager.Inst.AsyncLoadNextScene("CalendarScene");
        }
    }
}
