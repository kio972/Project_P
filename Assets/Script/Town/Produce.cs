using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Produce : MonoBehaviour
{
    private Queue<ProduceList> produceQueue;
    private ProduceList[] produceArray;
    [SerializeField]
    private ProduceList[] list;

    public void Init()
    {
        
    }

    public void InsertQueue()
    {
        ProduceList list = new ProduceList();
    }

    public void RefreshProduce()
    {// 새로 고침
        
    }
}
