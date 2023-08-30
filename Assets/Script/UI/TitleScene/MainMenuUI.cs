using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;

namespace JinWon
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> buttonList = new List<GameObject>();

        [SerializeField]
        private GameObject dataChoiceUI;
        [SerializeField]
        private GameObject dataInfoUI;
        [SerializeField]
        private GameObject deleteUI;
        [SerializeField]
        private GameObject refreshUI;


        [SerializeField]
        private FadeInOut fade; 

        public void Init()
        {
            DataInit();
        }

        public void DataInit() // ������ �����Ͱ� �ִ��� Ȯ���ϴ� �Լ�
        {
            dataChoiceUI.SetActive(false);
            dataInfoUI.SetActive(false);
            refreshUI.SetActive(false);
            // ������ Ű�� ������ ����
            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].SetActive(true);
            }
            buttonList[0].SetActive(false); // �ӽ÷� ��
        }

        #region ���θ޴� Button_Click�Լ�
        public void Continue_Click()
        {
            Debug.Log("���� ���� �ȵƽ��ϴ�!!");
            // ����� ������ ���� ��! ���� �÷��� �����Ͱ� ���� ���� ǥ������ �ʾƾ� ��!
        }

        public void Refresh_Click()
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].SetActive(false);
            }
            refreshUI.SetActive(true);
            // ������ ���ϴ� �����ͽ����� ���� �� Ķ������ �ٷ� �Ѿ��.
            // �ϸ� �ó����� �� Ķ���� �ý����� ��µ� �� Ʃ�丮���� ����Ǿ� ��!
        }

        public void RefreshStart_Click()
        {
            fade.Fade_InOut(false, 3.0f); // 3�� Fade�ϰ� �Ѿ
            Invoke("NextScene", 3.0f);
        }

        public void DataSelection_Click()
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].SetActive(false);
            }
            dataChoiceUI.SetActive(true);
        }

        public void Option_Click()
        {
            Debug.Log("���� ���� �ȵƽ��ϴ�!!");
            // ������ �ý��� �ɼ��� ����! ���� �߰��� ����.
        }
        #endregion

        #region ������ ���̽� UI
        public void DataButton_Click(int num) // ������ ������ ���
        {
            // �ؽ�Ʈ���� ������Ű�� ���� ��!!
            dataInfoUI.SetActive(true);
        }

        public void Goback_Click()
        {
            DataInit();
        }

        public void Delete_Click()
        {
            dataChoiceUI.SetActive(false);
            dataInfoUI.SetActive(false);
            deleteUI.SetActive(true);
        }
        #endregion

        #region ������ ���� UI
        public void Load_Click() // �޷¾����� �Ѿ�� ��!
        {
            fade.Fade_InOut(false, 3.0f); // 3�� Fade�ϰ� �Ѿ
            Invoke("NextScene", 3.0f);
        }

        public void NextScene()
        {
            GameManager.Inst.AsyncLoadNextScene(SceneName.CalendarScene);
        }

        public void Cancel_Click()
        {
            dataInfoUI.SetActive(false);
        }
        #endregion

        #region ������ ���� UI
        public void DeleteCancel()
        {
            deleteUI.SetActive(false);
            dataChoiceUI.SetActive(true);
        }

        public void LastDelete()
        {
            // ���������� �����ϴ� �ڵ� ���� ��!
        }

        #endregion
    }
}

