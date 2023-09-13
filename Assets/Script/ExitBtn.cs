using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jun
{
    public class ExitBtn : MonoBehaviour
    {
        public GameObject panel; // ���� �г��� ������ ����

        private void Start()
        {
            panel.SetActive(true); // ���� ���� �� �г��� Ȱ��ȭ
        }

        private void Update()
        {
            // ESC Ű�� ���� �� ���� ������Ʈ ��Ȱ��ȭ
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameObject.SetActive(false);
            }
        }

        public void OnHideButtonClicked()
        {
            panel.SetActive(false); // �г� ��Ȱ��ȭ
        }
    }
}

