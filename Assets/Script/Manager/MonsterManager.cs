using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private MonController[] monControllerArr;

    [SerializeField]
    private int areaIndex;
    public int AreaIndex { get => areaIndex; }

    // Start is called before the first frame update
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
            monster.gameObject.SetActive(false);
        }
    }

    public void StartMonster()
    {
        foreach (MonController monster in monControllerArr)
        {
            monster.gameObject.SetActive(true);
            if (monster.CurState != MonIdle.Instance)
                monster.ChangeState(MonPatrol.Instance);
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
