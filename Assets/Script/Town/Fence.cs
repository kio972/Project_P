using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
    [SerializeField] private TownManager townManager;
    [SerializeField] private TownUI townUI;
    [SerializeField] private int number;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && townManager.buildingOrder[number] != null)
        {
            townManager.selectedBuilding = townManager.buildingOrder[number];
            townUI.EnterBuilding();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && townManager.buildingOrder[number] != null && Input.GetKeyDown(KeyCode.Space))
        {
            townUI.InteractionBuilding();
            townUI.ExitBuilding();
            townUI.isControll = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            townUI.ExitBuilding();
        }
    }
}
