using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimeScript : MonoBehaviour
{
    public Image image;
    public Button button;
    public float coolTime = 10.0f; // 쿨타임 시간
    public bool isClicked = false; // 스킬 사용 여부
    float leftTime = 10.0f; // 남은 시간
    float speed = 5.0f;

   
    void Update()
    {
        // 클릭시 남은 시간 계산
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

    //스킬 클릭시 발동 이벤트
    public void StartCoolTime()
    {
        leftTime = coolTime;
        isClicked = true;
        if (button)
        {
            button.enabled = false; // 버튼 기능을 해지
        }
            
    }
}