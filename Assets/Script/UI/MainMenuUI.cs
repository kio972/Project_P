using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JinWon
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject continueObj; // �̾��ϱ� ��ư

        public void Init()
        {
            DataInit();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DataInit() // ������ �����Ͱ� �ִ��� Ȯ���ϴ� �Լ�
        {
            // ������ Ű�� ������ ����
            continueObj.SetActive(false); // �ӽ÷� ��
        }
    }
}

