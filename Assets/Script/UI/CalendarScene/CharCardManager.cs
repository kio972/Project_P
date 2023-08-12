using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCardManager : MonoBehaviour
{
    [SerializeField]
    private Transform invisibleCard;

    List<CardArranger> cardArrangers = new List<CardArranger>();

    CardArranger workingArranger;
    int oriIndex;

    

    private void Start()
    {
        var arrs = transform.GetComponentsInChildren<CardArranger>();

        for(int i = 0; i < arrs.Length; i++)
        {
            cardArrangers.Add(arrs[i]);
        }
    }

    public static void SwapCards(Transform sour, Transform dest)
    {
        Transform sourParent = sour.parent;
        Transform destParent = dest.parent;

        int sourIndex = sour.GetSiblingIndex();
        int destIndex = dest.GetSiblingIndex();

        sour.SetParent(destParent);
        sour.SetSiblingIndex(destIndex);

        dest.SetParent(sourParent);
        dest.SetSiblingIndex(sourIndex);
    }

    private void SwapCardsinHierarchy(Transform sour, Transform dest)
    {
        SwapCards(sour, dest);

        cardArrangers.ForEach(t => t.UpdateChildren());
    }

    bool ContainPos(RectTransform rt, Vector2 pos) 
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rt, pos); // Pos�� rt�ȿ� ������ True �ƴϸ� false ��ȯ
    }

    public void BeginDrag(Transform card) // �巡�׸� ����������
    {

        workingArranger = cardArrangers.Find(t => ContainPos(t.transform as RectTransform, card.position));
        oriIndex = card.GetSiblingIndex();
        SwapCardsinHierarchy(invisibleCard, card);
        Debug.Log(oriIndex + " <- Ori working -> " + workingArranger);
    }

    public void Drag(Transform card) // �巡�� ���϶�
    {
        //Debug.Log("Drag");

        var whichArrangerCard = cardArrangers.Find(t => ContainPos(t.transform as RectTransform, card.position));

        if(whichArrangerCard == null)
        {
            bool updateChilden = transform != invisibleCard.parent;

            invisibleCard.SetParent(transform);

            if(updateChilden)
            {
                cardArrangers.ForEach(t => t.UpdateChildren());
            }
        }
        else
        {
            bool insert = invisibleCard.parent == transform;

            if(insert)
            {
                int index = whichArrangerCard.GetIndexByPosition(card);

                invisibleCard.SetParent(whichArrangerCard.transform);
                whichArrangerCard.InsertCard(invisibleCard, index);
            }
            else
            {
                int invisibleCardIndex = invisibleCard.GetSiblingIndex();
                int targetIndex = whichArrangerCard.GetIndexByPosition(card, invisibleCardIndex);

                if (invisibleCardIndex != targetIndex)
                {
                    whichArrangerCard.SwapCard(invisibleCardIndex, targetIndex);
                }
            }
            
        }
    }

    public void EndDrag(Transform card) // �巡�װ� ��������
    {
        if(invisibleCard.parent == transform)
        {
            Debug.Log("�ٱ��� ����");
            card.SetParent(workingArranger.transform); 
            workingArranger.InsertCard(card, oriIndex);
            workingArranger = null;
            oriIndex = -1;
        }
        else
        {
            Debug.Log("�ȿ� ����");
            SwapCardsinHierarchy(invisibleCard, card);
        }
    }

    public Transform card01Transform;
    public Transform card02Transform;

    public void PartyAdd()
    {
        if(card01Transform = GameObject.Find("SelectCardObj").GetComponentInChildren<CharCard>().transform)
        {
            card01Transform.SetParent(cardArrangers[2].transform);
            cardArrangers[2].InsertCard(card01Transform, 0);
        }

        if(card02Transform = GameObject.Find("CharCardObj").GetComponentInChildren<CharCard>().transform)
        {
            card02Transform.SetParent(cardArrangers[0].transform);
            cardArrangers[0].InsertCard(card02Transform, 0);
        }
        
        for (int i = 0; i < cardArrangers.Count; i++)
        {
            cardArrangers[i].ChildrenInit(cardArrangers[i].Init);
        }
    }
}
