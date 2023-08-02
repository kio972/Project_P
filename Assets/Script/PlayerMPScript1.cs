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

    // MP �ʱ� ����
    void Start()
    {
        MPbar.value = (float)curMP / (float)maxMP;
        targetMP = curMP; // �ʱ⿡�� Ÿ�� MP�� ���� MP�� ����
    }

    // Update is called once per frame
    void Update()
    {
        // MP ������ ���� ó��
        if (curMP != targetMP)
        {
            curMP = Mathf.Lerp(curMP, targetMP, Time.deltaTime * 5);
            MPbar.value = (float)curMP / (float)maxMP;
        }
    }

    // ��ų ��� ��ư Ŭ�� ��
    public void UseSkill()
    {
        if (curMP >= maxMP * 0.1f)
        {
            // ���� MP���� 10% ���ҽ�Ű�� �� ���
            mpToDecrease = maxMP * 0.1f;
            targetMP = curMP - mpToDecrease;

            // ��ų ��� �� ��ư ��Ȱ��ȭ
            if (button)
            {
                button.enabled = false;
            }
        }
    }
}
