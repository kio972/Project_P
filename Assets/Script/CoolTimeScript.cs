using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimeScript : MonoBehaviour
{
    public Image image;
    public Button button;
    public float coolTime = 10.0f; // ��Ÿ�� �ð�
    public bool isClicked = false; // ��ų ��� ����
    float leftTime = 10.0f; // ���� �ð�
    float speed = 5.0f;

   
    void Update()
    {
        // Ŭ���� ���� �ð� ���
        if (isClicked)
            if (leftTime > 0)
            {
                leftTime -= Time.deltaTime * speed;
                if (leftTime < 0)
                {
                    leftTime = 0;
                    if (button)
                        button.enabled = true;
                    isClicked = true;
                }

                float ratio = 1.0f - (leftTime / coolTime);
                if (image)
                    image.fillAmount = ratio;
            }
    }

    //��ų Ŭ���� �ߵ� �̺�Ʈ
    public void StartCoolTime()
    {
        leftTime = coolTime;
        isClicked = true;
        if (button)
        {
            button.enabled = false; // ��ư ����� ����
        }
            
    }
}