using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private MonController[] monControllerArr; 

    // Start is called before the first frame update
    void Start()
    {
        monControllerArr = GetComponentsInChildren<MonController>();
        
        for(int i = 0; i < monControllerArr.Length; i++)
        {
            monControllerArr[i].Init();
            Debug.Log(monControllerArr[i].transform.name);
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
