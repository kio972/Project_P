using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;

namespace JinWon
{
    public class TitleScene : MonoBehaviour
    {
        [SerializeField]
        private GameObject bg1; // �÷��� ����� ���� ������ ����̹���
        [SerializeField]
        private GameObject bg2; // �÷��� ����� �ִ� ������ ����̹���

        [SerializeField]
        private GameObject titleImg;

        [SerializeField]
        private GameObject pressTextObj;

        [SerializeField]
        private GameObject mainMenuObj;

        private bool press;

        [SerializeField]
        private FadeInOut fade;

        [SerializeField]
        private JinWon.MainMenuUI mainMenuUI;

        void Start()
        {
            fade.Fade_InOut(true, 3.0f);
            Init();
            BGSetting();
        }

        void Update()
        {
            if (Input.anyKeyDown && !press)
            {
                press = true;
                Press();
            }
        }

        private void Init() // �ʱ� ���� �Լ�
        {
            press = false;
            bg1.SetActive(false);
            bg2.SetActive(false);
            mainMenuObj.SetActive(false);
            mainMenuUI.Init();
        }

        private void BGSetting() // ����̹����� �����ϴ� �Լ�
        {
            // �������� �÷��� ����� �ִ��� Ȯ���ϰ� ����� ��������� ��.
            bg1.SetActive(true); // �ӽ÷� ����
        }

        private void Press() // �ƹ�Ű�� �������� ���θ޴��� ���̴� �Լ�
        {
            titleImg.SetActive(false);
            pressTextObj.SetActive(false); // �ؽ�Ʈ�� ����
            mainMenuObj.SetActive(true);
            mainMenuUI.DataInit();
        }
    }
}

