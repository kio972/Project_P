using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBuilding : MonoBehaviour
{
    [SerializeField] private TownUI townUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            townUI.isControll = true;
            gameObject.SetActive(false);
        }
    }
}
