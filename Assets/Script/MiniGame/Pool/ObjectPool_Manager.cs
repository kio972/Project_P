using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ObjectType
{
    ObjT_Obstacle1,
    ObjT_Obstacle2,
}

public class ObjectPool_Manager : MonoBehaviour
{
    private static ObjectPool_Manager instance;
    public static ObjectPool_Manager Inst
    {
        get => instance;
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        else
            instance = this;
    }

    public List<ObjectPool> pools;
}
