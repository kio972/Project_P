using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MoveIcon : MonoBehaviour
{
    [SerializeField]
    private GameObject layout;

    private static Vector3 startPos;
    private Vector3 endPos;
    private int move;
    private static int index = 0;
    private float duration = 1.0f; // �̵��ϴ� �� �ɸ��� �ð�
    public List<Button> buttons; // ��ư�� ������ ����Ʈ

    private Color hide = new Color(1f,1f,1f,0f);
    private Color show = new Color(1f,1f,1f,1f);

    private bool isMoving = false;
    private int screenWidth = Screen.width;

    private void Start()
    {
        startPos = layout.transform.position;
    }

    public void NextBtn()
    {
        move = 0;

        if(index == 2)
        {
            endPos = startPos - new Vector3(screenWidth * 0.21875f, 0, 0);
            if (!isMoving)
            {
                buttons[(index + buttons.Count + 4) % buttons.Count].image.color = show; // ���� ��ư Ȱ��ȭ
                StartCoroutine(MoveCoroutine());

            }
        }
        else if(index < 2)
        {
            endPos = startPos - new Vector3(screenWidth * 0.4375f, 0, 0);
            if (!isMoving)
            {
                buttons[(index + buttons.Count + 4) % buttons.Count].image.color = show; // ���� ��ư Ȱ��ȭ
                buttons[(index + buttons.Count + 5) % buttons.Count].image.color = show; // ���� ��ư Ȱ��ȭ
                StartCoroutine(MoveCoroutine());
            }
        }

        
        
    }

    public void PrevBtn()
    {
        move = 1;

        if(index > 2)
        {
            endPos = startPos + new Vector3(screenWidth * 0.4375f, 0, 0);
            if (!isMoving)
            {
                buttons[(index + buttons.Count - 1) % buttons.Count].image.color = show; // ���� ��ư Ȱ��ȭ
                buttons[(index + buttons.Count - 2) % buttons.Count].image.color = show; // ���� ��ư Ȱ��ȭ
                StartCoroutine(MoveCoroutine());

            }
        }
        else if(index == 1)
        {
            endPos = startPos + new Vector3(screenWidth * 0.21875f, 0, 0);
            if (!isMoving)
            {
                buttons[(index + buttons.Count - 1) % buttons.Count].image.color = show; // ���� ��ư Ȱ��ȭ
                StartCoroutine(MoveCoroutine());

            }
        }

        
        
    }

    private IEnumerator MoveCoroutine()
    {
        isMoving = true;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            layout.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �̵��� ������ ��ġ�� ����
        if (move == 0)
        {
            if(index < 2)
            {
                index += 2;
                buttons[index - 2].image.color = hide;
                buttons[index - 1].image.color = hide;
                startPos.x -= Screen.width * 0.4375f;
            }
            else if(index == 2)
            {
                index += 1;
                buttons[index - 1].image.color = hide;
                startPos.x -= Screen.width * 0.21875f;
            }
            else if(index >= 3)
            {
                index = 3;
            }

        }
        else if(move == 1)
        {
            if(index >= 2)
            {
                buttons[index + 2].image.color = hide;
                buttons[index + 3].image.color = hide;
                index -= 2;
                startPos.x += Screen.width * 0.4375f;

            }
            else if(index == 1)
            {
                buttons[index + 3].image.color = hide;
                index -= 1;
                startPos.x += Screen.width * 0.21875f;
            }
            else if(index <= 0)
            {
                index = 0;
            }

        }
        Debug.Log("index: " + index);

        isMoving = false;
    }
}
