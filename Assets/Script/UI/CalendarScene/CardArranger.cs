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

    private bool btnClick = false;

    public void GoStageBtnOnClick()
    {
        if(!btnClick)
        {
            btnClick = true;
            if (children.Count == 3)
            {
                GameManager.Inst.AsyncLoadNextScene("Forest1-" + calendarScene.SelectStage);
            }
            else
            {
                systemText.text = "파티원이 모두 모이지 않았습니다.";
                StartCoroutine(SystemUi());
            }
        }
        
    }

    IEnumerator SystemUi()
    {
        LeanTween.scale(systemUI, Vector3.one, 0.5f);
        yield return YieldInstructionCache.WaitForSeconds(2.0f);
        LeanTween.scale(systemUI, Vector3.zero, 0.5f);
        btnClick = false;
    }

}
