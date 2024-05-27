using JinWon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnTrans;
    [SerializeField]
    private float spawnDeltaTime;  // ���� �ð�.

    private bool randomBool;

    private GameObject obj;

    private void Awake()
    {
        Invoke("StartSpawn", 3f);
    }

    private void StartSpawn()
    {
        StartCoroutine("SpawnEvent");
    }
    IEnumerator SpawnEvent() // �ݺ������� ���͸� ����. 
    {
        while (true)
        {
            randomBool = Random.value > 0.5f;
            Debug.Log("Random bool: " + randomBool);
            if (randomBool && RunManager.Inst.HP >= 0)
            {
                for (int i = 0; i < 1; i++)
                {
                    obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_Obstacle1].PopObj();
                    obj.transform.position = spawnTrans[0].position;
                }
            }
            else if(!randomBool&& RunManager.Inst.HP >= 0)
            {
                for (int i = 0; i < 1; i++)
                {
                    obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_Obstacle2].PopObj();
                    obj.transform.position = spawnTrans[1].position;
                }
            }


            yield return YieldInstructionCache.WaitForSeconds(spawnDeltaTime);  // ������ ���� ������ ������ �ο�. 
        }
    }
}
