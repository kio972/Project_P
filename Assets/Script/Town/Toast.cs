using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getResource
{
    public int type; // 0 - 골드, 1 - 마석
    public int value;
}

public class Toast : MonoBehaviour
{
    private string getText = " 획득";
    private string useText = " 사용";
    [SerializeField] private GameObject[] lists;
    [SerializeField] private Image[] images; // 각 리스트의 이미지
    [SerializeField] private Text[] texts; // 각 리스트의 텍스트
    [SerializeField] private Sprite[] sprites; // 자원 이미지
    [SerializeField] private Animation[] animations;
    private Queue<getResource> queue = new Queue<getResource>();
    private getResource temp;

    public void TestToast(int number)
    {// 테스트용
        PushQueue(number, number);
        Refresh();
    }

    public void UseResource(int type, int value)
    {// 자원 소모 및 획득시 토스트 메세지 출력
        PushQueue(type, value);
        Refresh();
    }

    public void PushQueue(int type, int value)
    {// 토스트 메세지 정보를 큐에 저장
        if (value != 0)
        {
            temp = new getResource();
            temp.type = type;
            temp.value = value;
            queue.Enqueue(temp);
        }
    }

    private void PopQueue(int number)
    {// 토스트 메세지 출력
        images[number].sprite = sprites[queue.Peek().type];
        if (queue.Peek().value > 0)
            texts[number].text = queue.Dequeue().value.ToString() + getText;
        else
            texts[number].text = Mathf.Abs(queue.Dequeue().value).ToString() + useText;
        animations[number].Play();
    }

    private void SortList()
    {// 토스트 메세지 정렬
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
    {// 새로고침
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
