using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YeongJun
{
    public class BattleSceneManager : MonoBehaviour
    {
        public int area = 0; // ���� ������ȣ
        [SerializeField]
        private GameObject camGroup; // ����� ī�޶� �׷�

        private List<MonsterManager> monsterGroup;

        private void Awake()
        {
            monsterGroup = new List<MonsterManager>(FindObjectsOfType<MonsterManager>());
        }

        private void Start()
        {
            MonsterGroupInit();
            SetBattleScene(area);
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

    }
}
