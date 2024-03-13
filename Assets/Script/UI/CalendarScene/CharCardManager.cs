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

    private CharInfoObj charInfoObj;

    private void Start()
    {
        charInfoObj = GameObject.Find("CharInfoObj").GetComponent<CharInfoObj>();

        var arrs = transform.GetComponentsInChildren<CardArranger>();

        for(int i = 0; i < arrs.Length; i++)
        {
            cardArrangers.Add(arrs[i]);
        }
        charInfoObj.SelectCardInfo();
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
        //workingArranger = cardArrangers.Find(t => ContainPos(t.transform as RectTransform, card.position));
        workingArranger = card.parent.GetComponent<CardArranger>();
        oriIndex = card.GetSiblingIndex();
        SwapCardsinHierarchy(invisibleCard, card);
    }

    public void Drag(Transform card) // �巡�� ���϶�
    {
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
        else if(whichArrangerCard != null && whichArrangerCard != cardArrangers[1])
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
            Debug.Log(whichArrangerCard);
        }
    }

    public void EndDrag(Transform card) // �巡�װ� ��������
    {
        if(invisibleCard.parent == transform)
        {
            Debug.Log(workingArranger);
            Debug.Log("�ٱ��� ����");
            card.SetParent(workingArranger.transform); 
            workingArranger.InsertCard(card, oriIndex);
            workingArranger = null;
            
            oriIndex = -1;
        }
        else if (invisibleCard.parent == cardArrangers[0].transform)
        {
            Debug.Log(invisibleCard.parent);
            SelectAtTheParty(card);
        }
        else
        {
            Debug.Log("�ȿ� ����");
            SwapCardsinHierarchy(invisibleCard, card);
            if(cardArrangers[0].GetComponentInChildren<CharCard>() == null)
            {
                Debug.Log("�ȿ� ����2");
                if (GameObject.Find("CharCardObj").GetComponentInChildren<CharCard>() != null)
                {
                    card02Transform = GameObject.Find("CharCardObj").GetComponentInChildren<CharCard>().transform;
                    card02Transform.SetParent(cardArrangers[0].transform);
                    cardArrangers[0].InsertCard(card02Transform, -1);

                    ArrangerChildInit();
                }
            }
        }
        charInfoObj.SelectCardInfo();
    }

    public Transform card01Transform;
    public Transform card02Transform;

    public void PartyAdd()
    {
        SoundManager.Inst.PlaySFX("Click_on");
        if (GameObject.Find("SelectCard").GetComponentInChildren<CharCard>() != null)
        {
            card01Transform = GameObject.Find("SelectCard").GetComponentInChildren<CharCard>().transform;
            card01Transform.SetParent(cardArrangers[2].transform);
            cardArrangers[2].InsertCard(card01Transform, -1);
            //Debug.Log("����Ʈ ī��");
        }

        if(GameObject.Find("CharCardObj").GetComponentInChildren<CharCard>() != null)
        {
            card02Transform = GameObject.Find("CharCardObj").GetComponentInChildren<CharCard>().transform;
            card02Transform.SetParent(cardArrangers[0].transform);
            cardArrangers[0].InsertCard(card02Transform, -1);
            //Debug.Log("ĳ���� ī��");
        }

        ArrangerChildInit();
        charInfoObj.SelectCardInfo();
    }

    private void SelectAtTheParty(Transform card) // ��Ƽ���� ����Ʈ�� �巡�� �Ҷ�
    {
        Debug.Log("Select 1");
        if (GameObject.Find("SelectCardObj").GetComponentInChildren<CharCard>() != null)
        {
            Debug.Log("Select 2");
            card01Transform = GameObject.Find("SelectCardObj").GetComponentInChildren<CharCard>().transform;
            card01Transform.SetParent(cardArrangers[1].transform);
            cardArrangers[1].InsertCard(card01Transform, -1);
            //SwapCardsinHierarchy(invisibleCard, card);
        }

        SwapCardsinHierarchy(invisibleCard, card);
        ArrangerChildInit();
    }

    public void ArrangerChildInit()
    {
        for (int i = 0; i < cardArrangers.Count; i++)
        {
            if (cardArrangers[i].children != null)
                cardArrangers[i].ChildrenInit(cardArrangers[i].Init);
        }
    }

    public void SelectCardBtn(bool whatDir)
    {
        if (whatDir) // ������ ����Ű
        {
            if (GameObject.Find("SelectCard").GetComponentInChildren<CharCard>() != null)
            {
                card01Transform = GameObject.Find("SelectCard").GetComponentInChildren<CharCard>().transform;
                card01Transform.SetParent(cardArrangers[1].transform);
                cardArrangers[1].InsertCard(card01Transform, 0);
                //Debug.Log("����Ʈ ī��");
            }

            if (GameObject.Find("CharCardObj").GetComponentInChildren<CharCard>() != null)
            {
                var card03Transform = GameObject.Find("CharCardObj").GetComponentsInChildren<CharCard>();
                card03Transform[card03Transform.Length - 1].transform.SetParent(cardArrangers[0].transform);
                cardArrangers[0].InsertCard(card03Transform[card03Transform.Length - 1].transform, -1);
                //Debug.Log("ĳ���� ī��");
            }
        }
        else // ���� ����Ű
        {
            if (GameObject.Find("SelectCard").GetComponentInChildren<CharCard>() != null)
            {
                card01Transform = GameObject.Find("SelectCard").GetComponentInChildren<CharCard>().transform;
                card01Transform.SetParent(cardArrangers[1].transform);
                cardArrangers[1].InsertCard(card01Transform, -1);
                //Debug.Log("����Ʈ ī��");
            }

            if (GameObject.Find("CharCardObj").GetComponentInChildren<CharCard>() != null)
            {
                var card03Transform = GameObject.Find("CharCardObj").GetComponentsInChildren<CharCard>();
                card03Transform[0].transform.SetParent(cardArrangers[0].transform);
                cardArrangers[0].InsertCard(card03Transform[0].transform, -1);
                //Debug.Log("ĳ���� ī��");
            }
        }

        ArrangerChildInit();
        charInfoObj.SelectCardInfo();
    }
}
