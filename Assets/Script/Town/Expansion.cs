using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expansion : MonoBehaviour
{
    [SerializeField] private TownUI townUI;

    public void Init()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            townUI.isControll = true;
            gameObject.SetActive(false);
        }
    }
}
