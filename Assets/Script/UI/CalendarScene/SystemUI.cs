using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JinWon;
public class SystemUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;


    public void SystemUiTitle()
    {
        text.text = "Thank you for play";
        StartCoroutine(SystemUiOpen());
    }

    IEnumerator SystemUiOpen()
    {
        LeanTween.scale(transform.gameObject, Vector3.one, 0.5f);
        yield return YieldInstructionCache.WaitForSeconds(2.0f);
        GameManager.Inst.AsyncLoadNextScene("TitleScene");
        //LeanTween.scale(transform.gameObject, Vector3.zero, 0.5f);
        //btnClick = false;
    }
}
