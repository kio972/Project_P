using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getResource
{
    public int type; // 0 - ���, 1 - ����
    public int value;
}

public class Toast : MonoBehaviour
{
    private string getText = " ȹ��";
    private string useText = " ���";
    [SerializeField] private GameObject[] lists;
    [SerializeField] private Image[] images; // �� ����Ʈ�� �̹���
    [SerializeField] private Text[] texts; // �� ����Ʈ�� �ؽ�Ʈ
    [SerializeField] private Sprite[] sprites; // �ڿ� �̹���
    [SerializeField] private Animation[] animations;
    private Queue<getResource> queue = new Queue<getResource>();
    private getResource temp;

    public void TestToast(int number)
    {// �׽�Ʈ��
        PushQueue(number, number);
        Refresh();
    }

    public void UseResource(int type, int value)
    {// �ڿ� �Ҹ� �� ȹ��� �佺Ʈ �޼��� ���
        PushQueue(type, value);
        Refresh();
    }

    public void PushQueue(int type, int value)
    {// �佺Ʈ �޼��� ������ ť�� ����
        if (value != 0)
        {
            temp = new getResource();
            temp.type = type;
            temp.value = value;
            queue.Enqueue(temp);
        }
    }

    private void PopQueue(int number)
    {// �佺Ʈ �޼��� ���
        images[number].sprite = sprites[queue.Peek().type];
        if (queue.Peek().value > 0)
            texts[number].text = queue.Dequeue().value.ToString() + getText;
        else
            texts[number].text = Mathf.Abs(queue.Dequeue().value).ToString() + useText;
        animations[number].Play();
    }

    private void SortList()
    {// �佺Ʈ �޼��� ����
        for (int i = lists.Length - 1; i > 0; i--)
        {
            if (!lists[i].activeSelf && lists[i-1].activeSelf)
            {
                lists[i].SetActive(true);
                images[i].sprite = images[i - 1].sprite;
                texts[i].text = texts[i - 1].text;
                lists[i - 1].SetActive(false);
            }
        }
    }

    public void Refresh()
    {// ���ΰ�ħ
        SortList();
        for (int i = lists.Length - 1; i >= 0; i--)
        {
            if (queue.Count != 0)
            {
                if (!lists[i].activeSelf)
                {
                    lists[i].SetActive(true);
                    PopQueue(i);
                }
            }
            else
                break;
        }
    }

    public void InvisibleList(int number)
    {
        lists[number].SetActive(false);
        Refresh();
    }
}
