using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jun;
using JinWon;
using TMPro;

namespace YeongJun
{
    public class BattleSceneManager : MonoBehaviour
    {
        
        public int area = 0; // ���� ������ȣ

        [SerializeField]
        public int lastArea;

        [SerializeField]
        private GameObject camGroup; // ����� ī�޶� �׷�

        private List<MonsterManager> monsterGroup;
        private ControllerManager controllerManager;

        [SerializeField]
        private int stage;

        public bool stageClear = false;

        private int monCount;

        [SerializeField]
        private List<GameObject> monObjList = new List<GameObject>();

        [SerializeField]
        private List<StagePortal> stagePotal = new List<StagePortal>(); // 0�� ���� 1�� ������

        private Revealer revealer;

        private void Awake()
        {
            monsterGroup = new List<MonsterManager>(FindObjectsOfType<MonsterManager>());
            SoundManager.Inst.ChangeBGM(BGM_Type.BGM_Battle);
        }

        // Ŭ���� �� ������ �ƴ��� �Ǵ�
        private void StageInit()
        {
            if(GameManager.Inst.PlayerInfo.forestClear[stage] == true)
            {
                stageClear = true;
                for (int i = 0; i < monObjList.Count; i++)
                {
                    monObjList[i].SetActive(false);
                    monCount = 0;
                    PotalOpen();
                    if(GameManager.Inst.PlayerInfo.charactorVec == "Right")
                        ChangeCam(lastArea);
                    else if(GameManager.Inst.PlayerInfo.charactorVec == "Left")
                        ChangeCam(area);
                }
            }
            else
            {
                if (stage == 3)
                    revealer = GameObject.Find("Revealer").GetComponent<Revealer>();
                monCount = monObjList.Count; // ���� ��
                MonsterGroupInit();
                SetBattleScene(area, area);
            }
            goldText.text = GameManager.Inst.PlayerInfo.gold.ToString();
            stonText.text = GameManager.Inst.PlayerInfo.ston.ToString();
        }

        private void Start()
        {
            SoundManager.Inst.ChangeBGM(BGM_Type.BGM_Title); // �ӽ�
            GameManager.Inst.Fade_InOut(true, 3.0f);
            StageInit();

            //MonsterGroupInit();
            //SetBattleScene(area);
            controllerManager = FindObjectOfType<ControllerManager>();
            // ������ ���־�� ��.
            if (controllerManager != null)
                controllerManager.Init();
            /*CharChangeScript charChangeScript = FindObjectOfType<CharChangeScript>();
            if (charChangeScript != null && controllerManager != null)
                charChangeScript.Init(controllerManager.controllerList[0], controllerManager.controllerList[1], controllerManager.controllerList[2], this);*/
        }

        private void MonsterGroupInit()
        {
            foreach (MonsterManager monsterManager in monsterGroup)
            {
                monsterManager.MonsterManagerInit();
            }
        }

        private void ChangeMonsterState(int number)
        {
            foreach(MonsterManager monsterManager in monsterGroup)
            {
                if (monsterManager.AreaIndex == number)
                    monsterManager.StartMonster();
                else
                    monsterManager.PauseMonster();
            }
        }

        [SerializeField]
        private Transform player;

        private void ChangeCam(int number)
        {
            for (int i = 0; i < camGroup.transform.childCount; i++)
                camGroup.transform.GetChild(i).gameObject.SetActive(false);
            camGroup.transform.GetChild(number).gameObject.SetActive(true);
        }

        public void SetBattleScene(int number, int cam)
        {
            if(!stageClear)
                ChangeMonsterState(number);
            ChangeCam(cam);


        }

        public void ChangeMainChar(Controller target)
        {
            controllerManager.ControllerChange(target);
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
                GameManager.Inst.AsyncLoadNextScene("TitleScene");
        }

        public void PotalOpen() // ��Ż�� ������ �ϴ��� üũ 
        {
            monCount--;
            if (monCount <= 0)
            {
                if (stage == 3)
                {
                    revealer.PortalOpen();
                }
                else
                {
                    for (int i = 0; i < stagePotal.Count; i++)
                        stagePotal[i].PotalOpen(true);
                }   
            }
        }

        [SerializeField]
        private TextMeshProUGUI goldText;
        [SerializeField]
        private TextMeshProUGUI stonText;

        private int itemValue;

        public void ItemDrop(Items item)
        {
            itemValue = Random.Range(1, 4);
            switch (item)
            {
                case Items.gold:
                    {
                        GameManager.Inst.PlayerInfo.gold += itemValue;
                        Debug.Log(itemValue + "��� ���! Total : " + GameManager.Inst.PlayerInfo.gold);
                        goldText.text = GameManager.Inst.PlayerInfo.gold.ToString();
                        break;
                    }
                case Items.ston:
                    {
                        GameManager.Inst.PlayerInfo.ston += itemValue;
                        Debug.Log(itemValue + "���� ���! Total : " + GameManager.Inst.PlayerInfo.ston);
                        stonText.text = GameManager.Inst.PlayerInfo.ston.ToString();
                        break;
                    }
            }
        }

    }
}
