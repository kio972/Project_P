using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jun;
using JinWon;
namespace YeongJun
{
    public class BattleSceneManager : MonoBehaviour
    {
        public int area = 0; // 맵의 구간번호
        [SerializeField]
        private GameObject camGroup; // 버츄얼 카메라 그룹

        private List<MonsterManager> monsterGroup;
        private ControllerManager controllerManager;

        private CharChangeScript charChangeScript;

        private void Awake()
        {
            monsterGroup = new List<MonsterManager>(FindObjectsOfType<MonsterManager>());
        }

        private void Start()
        {
            GameManager.Inst.Fade_InOut(true, 3.0f);
            MonsterGroupInit();
            SetBattleScene(area);
            controllerManager = FindObjectOfType<ControllerManager>();
            if (controllerManager != null)
                controllerManager.Init();
            charChangeScript = FindObjectOfType<CharChangeScript>();
            if (charChangeScript != null && controllerManager != null)
                CharChangeScriptInit();
        }

        private string controller;

        private void CharChangeScriptInit()
        {
            controller = GameManager.Inst.PlayerInfo.controller;
            switch(controller)
            {
                case "Warrior":
                    {
                        charChangeScript.Init(controllerManager.controllerList[0], controllerManager.controllerList[1], controllerManager.controllerList[2], this);
                        break;
                    }
                case "Priest":
                    {
                        charChangeScript.Init(controllerManager.controllerList[1], controllerManager.controllerList[2], controllerManager.controllerList[0], this);
                        break;
                    }
                case "Archer":
                    {
                        charChangeScript.Init(controllerManager.controllerList[2], controllerManager.controllerList[0], controllerManager.controllerList[1], this);
                        break;
                    }
            }
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

        private void ChangeCam(int number)
        {
            for (int i = 0; i < camGroup.transform.childCount; i++)
                camGroup.transform.GetChild(i).gameObject.SetActive(false);
            camGroup.transform.GetChild(number).gameObject.SetActive(true);
        }

        public void SetBattleScene(int number)
        {
            ChangeMonsterState(number);
            ChangeCam(number);


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

    }
}
