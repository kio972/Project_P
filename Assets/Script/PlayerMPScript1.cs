using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMPScript : MonoBehaviour
{
    [SerializeField]
    private Slider MPbar;
    private float maxMP = 100;
    private float curMP = 100;
    private float targetMP;
    private float mpToDecrease = 0;

    public Button button;

    // MP 초기 설정
    void Start()
    {
        MPbar.value = (float)curMP / (float)maxMP;
        targetMP = curMP; // 초기에는 타겟 MP를 현재 MP로 설정
    }

    // Update is called once per frame
    void Update()
    {
        // MP 게이지 감소 처리
        if (curMP != targetMP)
        {
            curMP = Mathf.Lerp(curMP, targetMP, Time.deltaTime * 5);
            MPbar.value = (float)curMP / (float)maxMP;
        }
    }

    // 스킬 사용 버튼 클릭 시
    public void UseSkill()
    {
        if (curMP >= maxMP * 0.1f)
        {
            // 현재 MP에서 10% 감소시키는 양 계산
            mpToDecrease = maxMP * 0.1f;
            targetMP = curMP - mpToDecrease;

            // 스킬 사용 시 버튼 비활성화
            if (button)
            {
                button.enabled = false;
            }
        }
    }
}
