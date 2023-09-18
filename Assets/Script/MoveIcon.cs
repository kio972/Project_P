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
    private float duration = 1.0f; // 이동하는 데 걸리는 시간
    public List<Button> buttons; // 버튼을 저장할 리스트

    private bool isMoving = false;

    private void Start()
    {
        startPos = layout.transform.position;

    }

    public void NextBtn()
    {
        move = 0;
        if(index == 2)
        {
            endPos = startPos - new Vector3(560, 0, 0);
            if (!isMoving)
            {
                StartCoroutine(MoveCoroutine());

            }
        }
        else if(index < 2)
        {
            endPos = startPos - new Vector3(1120, 0, 0);
            if (!isMoving)
            {

                StartCoroutine(MoveCoroutine());

            }
        }

        
        
    }

    public void PrevBtn()
    {
        move = 1;
        if(index > 2)
        {
            endPos = startPos + new Vector3(1120, 0, 0);
            if (!isMoving)
            {
                buttons[(index + buttons.Count - 1) % buttons.Count].gameObject.SetActive(true); // 이전 버튼 활성화
                buttons[(index + buttons.Count - 2) % buttons.Count].gameObject.SetActive(true); // 이전 버튼 활성화
                StartCoroutine(MoveCoroutine());
                buttons[index + 2].gameObject.SetActive(false);
                buttons[index + 3].gameObject.SetActive(false);

            }
        }
        else if(index == 1)
        {
            endPos = startPos + new Vector3(560, 0, 0);
            if (!isMoving)
            {
                buttons[(index + buttons.Count - 1) % buttons.Count].gameObject.SetActive(true); // 이전 버튼 활성화
                StartCoroutine(MoveCoroutine());
                buttons[index + 1].gameObject.SetActive(false);
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

        // 이동이 끝나면 위치를 보정
        if (move == 0)
        {
            if(index < 2)
            {
                index += 2;
                startPos.x -= 1120;
            }
            else if(index == 2)
            {
                index += 1;
                startPos.x -=560;
            }
            if(index >= 3)
            {
                index = 3;
            }

        }
        else if(move == 1)
        {
            if(index >= 2)
            {
                index -= 2;
                startPos.x += 1120;
            }
            else if(index == 1)
            {
                index -= 1;
                startPos.x += 560;
                
            }
            if(index <= 0)
            {
                index = 0;
            }

        }
        Debug.Log(index);

        isMoving = false;
    }
}
