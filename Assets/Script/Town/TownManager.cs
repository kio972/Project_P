using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;

public class TownManager : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    public Building selectedBuilding; // ������ �ǹ�

    public Building[] building; // ��ġ�� �ǹ�
    public Building[] buildingOrder = new Building[11]; // �ǹ� ��ġ ����
    public GameObject[] buildPlace = new GameObject[11]; // �ǹ��� ��ġ�� ���
    public GameObject[] sign = new GameObject[11]; // �� ������ ���� ǥ����
    public bool[] isBuild = new bool[11]; // ��Һ� �Ǽ� ���� ����

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
    {// ���� ���� �� �����͸� �ҷ����� �Լ�

    }

    public void BuildBuilding(Building building, int number)
    {// �ǹ� �Ǽ��ϴ� �Լ�
        building.level = 1;
        building.isBuild = true;
        buildingOrder[number] = building;
        building.placeNum = number;
        building.transform.position = buildPlace[number].transform.position;
        sign[number].transform.position = new Vector3(0, -20, 0);
    }

    public void MoveBuilding(Building building,int number)
    {// �ǹ� �����̴� �Լ� ( �ű� �ǹ�, �ű� ��ġ )
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
                    // �ǹ��� ������ �� UI���
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
                Debug.Log("M Ŭ��");
                townUI.OpenBuildCanvas();
            }
            else if (Input.GetKey(KeyCode.B))
                GameManager.Inst.AsyncLoadNextScene("CalendarScene");
        }
    }
}
