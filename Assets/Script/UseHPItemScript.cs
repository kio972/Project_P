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

    // HP ȸ�� �׽�Ʈ
    public void IncreaseHP()
    {
        if (curHP < maxHP)
        {
            float HPToIncrease = maxHP * 0.1f; // 10% �����ϴ� �� ���
            curHP = Mathf.Min(curHP + HPToIncrease, maxHP); // ���� HP�� �ִ� HP ���̿��� ����
            HPbar.value = (float)curHP / (float)maxHP;
        }
    }
}
