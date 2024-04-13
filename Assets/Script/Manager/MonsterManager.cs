using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour 
{
    private MonController[] monControllerArr;

    [SerializeField]
    private int areaIndex;
    public int AreaIndex { get => areaIndex; }

    /*private void Start()
    {
        MonsterManagerInit();   
    }*/

    private YeongJun.BattleSceneManager battleSceneManager;

    private void Awake()
    {
        battleSceneManager = GameObject.Find("BattleSceneManager").GetComponent<YeongJun.BattleSceneManager>();
    }

    public void MonsterDie()
    {
        battleSceneManager.PotalOpen();
    }

    public void MonsterManagerInit()
    {
        monControllerArr = GetComponentsInChildren<MonController>();
        
        for(int i = 0; i < monControllerArr.Length; i++)
        {
            monControllerArr[i].Init();
        }
    }

    public void PauseMonster()
    {
        foreach(MonController monster in monControllerArr)
        {
            /*if (monster.CurState != MonIdle.Instance)
                monster.ChangeState(MonIdle.Instance);*/
            //monster.gameObject.SetActive(false);

            /*if (monster.CurState == MonPatrol.Instance)
                monster.ChangeState(MonIdle.Instance);
            else if(monster.CurState != MonPatrol.Instance)
                monster.ChangeState(MonPatrol.Instance);*/

            monster.Pause(true);
            monster.ChangeState(MonIdle.Instance);
        }
        
    }

    public void StartMonster()
    {
        foreach (MonController monster in monControllerArr)
        {
            if(!monster.Die)
            {
                //monster.gameObject.SetActive(true);
                //monster.Target = false;
                monster.Pause(false);
                if (monster.CurState == MonIdle.Instance)
                    monster.ChangeState(MonPatrol.Instance);
                else if(monster.CurState != MonIdle.Instance)
                    monster.ChangeState(MonChase.Instance);
            }
        }
    }

    public void TargetDetectionManager()
    {
        for (int i = 0; i < monControllerArr.Length; i++)
        {
            monControllerArr[i].TargetDetection();
        }
    }
}
