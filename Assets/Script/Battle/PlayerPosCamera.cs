using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosCamera : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> cameraList = new List<GameObject>();

    [SerializeField]
    private Transform player;

    private void Update()
    {
        if(player.transform.position.y >= 5f)
        {
            if (cameraList[0].activeSelf)
                cameraList[1].SetActive(true);
            else if (cameraList[2].activeSelf)
                cameraList[3].SetActive(true);
            else if (cameraList[4].activeSelf)
                cameraList[5].SetActive(true);
        }
        else
        {
            if (cameraList[1].activeSelf)
                cameraList[1].SetActive(false);
            else if (cameraList[3].activeSelf)
                cameraList[3].SetActive(false);
            else if (cameraList[5].activeSelf)
                cameraList[5].SetActive(false);
        }


    }
}
