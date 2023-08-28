using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Jun
{
    public class ExploreSciprt : MonoBehaviour
    {

        public List<Button> buttons; // 8���� ��ư�� ������ ����Ʈ
        private int currentButtonIndex = 0; // ���� ��ư�� �ε���
        private Vector3[] buttonPositions; // �� ��ư�� �ʱ� ��ġ ����

        private void Start()
        {
            // ��ư ��ġ �ʱ�ȭ �� ����
            buttonPositions = new Vector3[buttons.Count];
            for (int i = 0; i < buttons.Count; i++)
            {
                buttonPositions[i] = buttons[i].transform.position;
            }
        }

        public void OnNextButtonClicked()
        {
            buttons[currentButtonIndex].gameObject.SetActive(false); // ���� ��ư �����
            buttons[currentButtonIndex+1].gameObject.SetActive(false);

            currentButtonIndex = (currentButtonIndex + 2) % buttons.Count; // ���� ��ư �ε����� ����
            UpdateButtonPositions();
        }

        private void UpdateButtonPositions()
        {
            buttons[currentButtonIndex].gameObject.SetActive(true); // ���� ��ư ���̵��� ����
            buttons[currentButtonIndex+1].gameObject.SetActive(true);
            buttons[currentButtonIndex].transform.position = buttonPositions[currentButtonIndex];
            buttons[currentButtonIndex+1].transform.position = buttonPositions[currentButtonIndex+1];// ��ư ��ġ ������Ʈ
        }
    }
}
