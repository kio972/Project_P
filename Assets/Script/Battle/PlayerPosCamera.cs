using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosCamera : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> cameraList = new List<GameObject>();

    [SerializeField]
    private Transform player;

    [SerializeField]
    private bool qestStage;

    [SerializeField]
    private int addCam;

    private void Awake()
    {
        if (qestStage)
        {
            addCam = 1;
        }
        else
            addCam = 0;
    }

    private void Update()
    {
        if (player.transform.position.y >= 5f)
        {
            if (cameraList[0].activeSelf)
                cameraList[3 + addCam].SetActive(true);
            else if (cameraList[1].activeSelf)
                cameraList[4 + addCam].SetActive(true);
            else if (cameraList[2].activeSelf)
                cameraList[5 + addCam].SetActive(true);
            else if (cameraList[3].activeSelf && qestStage)
                cameraList[7].SetActive(true);
        }
        else
        {
            if (cameraList[3 + addCam].activeSelf)
            {
                cameraList[0].SetActive(true);
                cameraList[3 + addCam].SetActive(false);
            }
            else if (cameraList[4 + addCam].activeSelf)
            {
                cameraList[1].SetActive(true);
                cameraList[4 + addCam].SetActive(false);
            }
            else if (cameraList[5 + addCam].activeSelf)
            {
                cameraList[2].SetActive(true);
                cameraList[5 + addCam].SetActive(false);
            }

            if(qestStage)
            {
                if (cameraList[7].activeSelf)
                {
                    cameraList[3].SetActive(true);
                    cameraList[7].SetActive(false);
                }
                    
            }
        }
    }
}
