using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;

public class SceneMoveBtn : MonoBehaviour
{
    public void CalenderMoveBtn()
    {
        SoundManager.Inst.PlaySFX("Click_on");
        GameManager.Inst.AsyncLoadNextScene("Guild"); // ��� ���� �� �̸����� �����ؾ���.
    }

    public void BtnOff()
    {
        SoundManager.Inst.PlaySFX("Click_off");
    }
}
