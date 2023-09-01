using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Jun;

public class CharStateUI : MonoBehaviour
{
    private Controller controller;
    
    [SerializeField]
    private PlayerHPScript hpbar;
    [SerializeField]
    private Image charMark;

    public void Init(Controller controller)
    {
        this.controller = controller;
        hpbar?.Init(controller);
    }

    public void SwapCharState(CharStateUI charStateUI)
    {
        //마크 스왑
        Sprite temp = charMark.sprite;
        charMark.sprite = charStateUI.charMark.sprite;
        charStateUI.charMark.sprite = temp;
        //컨트롤러 스왑
        Controller temp2 = controller;
        controller = charStateUI.controller;
        charStateUI.controller = temp2;

        hpbar?.Init(controller);
        charStateUI.hpbar?.Init(charStateUI.controller);
    }
}
