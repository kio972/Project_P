using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardArranger : MonoBehaviour
{
    public List<Transform> children; 

    private void Start()
    {
        children = new List<Transform>();

        UpdateChildren();
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

}
