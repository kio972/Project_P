using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JinWon;

public class Resource : MonoBehaviour
{
    [SerializeField] private Text goldText;
    [SerializeField] private Text manaText;

    public void Refresh()
    {
        goldText.text = GameManager.Inst.PlayerInfo.gold.ToString();
        manaText.text = GameManager.Inst.PlayerInfo.ston.ToString();
    }
}
