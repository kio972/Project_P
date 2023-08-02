using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseHPItemScript : MonoBehaviour
{
    [SerializeField]
    private Slider HPbar;
    private float maxHP = 100;
    private float curHP = 100;

    // HP 회복 테스트
    public void IncreaseHP()
    {
        if (curHP < maxHP)
        {
            float HPToIncrease = maxHP * 0.1f; // 10% 증가하는 양 계산
            curHP = Mathf.Min(curHP + HPToIncrease, maxHP); // 현재 HP와 최대 HP 사이에서 증가
            HPbar.value = (float)curHP / (float)maxHP;
        }
    }
}
