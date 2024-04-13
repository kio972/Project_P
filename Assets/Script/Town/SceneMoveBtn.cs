using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;

public class SceneMoveBtn : MonoBehaviour
{
    public void CalenderMoveBtn()
    {
        SoundManager.Inst.PlaySFX("Click_on");
        GameManager.Inst.AsyncLoadNextScene("Guild"); // 길드 내부 씬 이름으로 수정해야함.
    }

    public void BtnOff()
    {
        SoundManager.Inst.PlaySFX("Click_off");
    }
}
