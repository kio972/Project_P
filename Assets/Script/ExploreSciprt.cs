using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jun
{
    public class ExploreScript : MonoBehaviour
    {
        public List<Button> buttons; // ��ư�� ������ ����Ʈ
        private static int currentButtonIndex = 0; // ���� ��ư�� �ε���
        private Vector3[] buttonPositions; // �� ��ư�� �ʱ� ��ġ ����
        public GameObject Explore;

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
            Debug.Log(currentButtonIndex);
            if (currentButtonIndex == 3)
            {
                buttons[currentButtonIndex].gameObject.SetActive(false); // ���� ��ư ��Ȱ��ȭ
                currentButtonIndex = (currentButtonIndex) % buttons.Count; // ���� ��ư �ε����� ����
                buttons[currentButtonIndex].gameObject.SetActive(true); // ���� ��ư Ȱ��ȭ
            }
            else
            {
                buttons[currentButtonIndex].gameObject.SetActive(false); // ���� ��ư ��Ȱ��ȭ
                buttons[currentButtonIndex + 1].gameObject.SetActive(false);
                if(currentButtonIndex == 2)
                {
                    currentButtonIndex = (currentButtonIndex + 1) % buttons.Count; // ���� ��ư �ε����� ����
                }
                else
                {
                    currentButtonIndex = (currentButtonIndex + 2) % buttons.Count; // ���� ��ư �ε����� ����
                }
                
                buttons[currentButtonIndex].gameObject.SetActive(true); // ���� ��ư Ȱ��ȭ
                buttons[currentButtonIndex + 1].gameObject.SetActive(true);
            }
            

            UpdateButtonPositions();
        }

        public void OnPreviousButtonClicked()
        {
            Debug.Log(currentButtonIndex);
            if (buttons.Count < 4)
            {
                // ���� ó��: ��ư ������ 4�� �̸��� ��� ����
                return;
            }

            buttons[currentButtonIndex+2].gameObject.SetActive(false); // ���� ��ư ��Ȱ��ȭ
            buttons[currentButtonIndex + 3].gameObject.SetActive(false);
            buttons[(currentButtonIndex + buttons.Count - 2) % buttons.Count].gameObject.SetActive(true); // ���� ��ư Ȱ��ȭ
            buttons[(currentButtonIndex + buttons.Count - 1) % buttons.Count].gameObject.SetActive(true); // ������ ��ư Ȱ��ȭ

            currentButtonIndex = (currentButtonIndex + buttons.Count - 2) % buttons.Count; // ���� ��ư �ε����� ����
            UpdateButtonPositions();
        }

        private void UpdateButtonPositions()
        {
            int numButtons = buttons.Count;

            for (int i = 0; i < numButtons; i++)
            {
                int offset = i - currentButtonIndex;
                if (offset < 0) offset += numButtons;

                buttons[i].transform.position = buttonPositions[offset];
            }
        }

        private void test()
        {
            /*for(int i = 0; i < buttons.Count; i++){
                Explore.transform.position = Vector3.Lerp(Explore.transform.position, buttons[i - 1], Time.deltaTime);
            }*/
            
        }
    }
}
