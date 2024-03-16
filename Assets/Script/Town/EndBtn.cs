using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;

public class EndBtn : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            GameManager.Inst.AsyncLoadNextScene("CalendarScene");
        }
    }

    public void CalenderMoveBtn()
    {
        GameManager.Inst.AsyncLoadNextScene("CalendarScene");
    }


}
