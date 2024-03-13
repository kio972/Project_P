using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;
using TMPro;

public class CardArranger : MonoBehaviour
{

    private CalendarScene calendarScene;

    public List<Transform> children;

    [SerializeField]
    private bool init;
    public bool Init
    {
        get { return init; }
        set { init = value; }
    }

    private void Start()
    {
        calendarScene = GameObject.Find("CalendarScene").GetComponent<CalendarScene>();
        children = new List<Transform>();

        UpdateChildren();
        ChildrenInit(init);
    }

    public void UpdateChildren()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if(i == children.Count)
            {
                children.Add(null);
            }

            var child = transform.GetChild(i);

            if(child != children[i])
            {
                children[i] = child;
            }
        }
        
        children.RemoveRange(transform.childCount, children.Count - transform.childCount);
    }

    public void ChildrenInit(bool value)
    {
        UpdateChildren();
        if (value)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                children[i].gameObject.GetComponent<CharCard>().CharCardInit(value);
            }
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                children[i].gameObject.GetComponent<CharCard>().CharCardInit(value);
            }
        }
    }

    public void InsertCard(Transform card, int index)
    {
        Debug.Log("인설트 카드" + card + " " + index);
        children.Add(card);
        card.SetSiblingIndex(index);
        UpdateChildren();
    }

    public int GetIndexByPosition(Transform card, int skipindex = -1)
    {
        int result = 0;
        
        for(int i = 0; i < children.Count; i++)
        {
            if(card.position.x < children[i].position.x)
            {
                break;
            }
            else if(skipindex != i)
            {
                result++;
            }
        }

        return result;
    }
    
    public void SwapCard(int index01, int index02)
    {
        CharCardManager.SwapCards(children[index01], children[index02]);
        UpdateChildren();
    }

    [SerializeField]
    private GameObject systemUI;
    [SerializeField]
    private TextMeshProUGUI systemText;

    /*[SerializeField]
    private SystemUI systemUI;*/

    private bool btnClick = false;

    public void GoStageBtnOnClick()
    {
        if(!btnClick)
        {
            btnClick = true;
<<<<<<< HEAD
            if (children.Count == 3)
            {
                //GameManager.Inst.AsyncLoadNextScene("Forest1-" + calendarScene.SelectStage);
                GameManager.Inst.Fade_InOut(false, 1.0f);
                Invoke("NextScene", 1.0f);
=======
            Debug.Log("칠드런 카운트는 : " + children.Count);
            if (children.Count == 1)
            {
                SoundManager.Inst.PlaySFX("Click_on");
                GameManager.Inst.CharHPInit(220f); // 임시 워리어 MaxHP
                Debug.Log("로드 씬 들어옴");
                GameManager.Inst.AsyncLoadNextScene("Forest" + calendarScene.SelectStage);
>>>>>>> Jun
            }
            else
            {
                SoundManager.Inst.PlaySFX("Click_off");
                systemText.text = "<color=yellow>"+ "파티" +"</color>"+ "가 구성되지 않으면 탐색을 출발할 수 없어요.";
                StartCoroutine(SystemUi());
                //systemUI.SystemUIActive("<color=yellow>" + "파티" + "</color>" + "가 구성되지 않으면 탐색을 출발할 수 없어요.", 2.0f);
                //btnClick = false;
            }
        }
        
    }

    public void hiddenBtn()
    {
        SoundManager.Inst.PlaySFX("Click_off");
        systemText.text = "<color=yellow>" + "아직 " + "</color>" + "활성화 되지 않았습니다.";
        StartCoroutine(SystemUi());
    }

    IEnumerator SystemUi()
    {
        LeanTween.scale(systemUI, Vector3.one, 0.5f);
        yield return YieldInstructionCache.WaitForSeconds(2.0f);
        LeanTween.scale(systemUI, Vector3.zero, 0.5f);
        btnClick = false;
    }

    public void NextScene()
    {
        GameManager.Inst.AsyncLoadNextScene(SceneName.Forest1_1);
    }

}
