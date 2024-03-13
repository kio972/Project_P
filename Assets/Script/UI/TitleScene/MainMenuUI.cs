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
        private GameObject titleUI;

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
            SoundManager.Inst.PlaySFX("Click_on");
            Debug.Log("���� ���� �ȵƽ��ϴ�!!");
            // ����� ������ ���� ��! ���� �÷��� �����Ͱ� ���� ���� ǥ������ �ʾƾ� ��!
        }

        public void Refresh_Click()
        {
            SoundManager.Inst.PlaySFX("Click_on");
            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].SetActive(false);
            }
            titleUI.SetActive(false);
            refreshUI.SetActive(true);
            // ������ ���ϴ� �����ͽ����� ���� �� Ķ������ �ٷ� �Ѿ��.
            // �ϸ� �ó����� �� Ķ���� �ý����� ��µ� �� Ʃ�丮���� ����Ǿ� ��!
        }

        public void RefreshStart_Click()
        {
            GameManager.Inst.Fade_InOut(false, 1.0f);
            Invoke("NextScene", 1.0f);
        }

        public void DataSelection_Click()
        {
            SoundManager.Inst.PlaySFX("Click_off");
            /*for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].SetActive(false);
            }
            dataChoiceUI.SetActive(true);*/
        }

        public void Option_Click()
        {
            SoundManager.Inst.PlaySFX("Click_off");
            Debug.Log("���� ���� �ȵƽ��ϴ�!!");
            // ������ �ý��� �ɼ��� ����! ���� �߰��� ����.
        }
        #endregion

        #region ������ ���̽� UI
        public void DataButton_Click(int num) // ������ ������ ���
        {
            SoundManager.Inst.PlaySFX("Click_on");
            // �ؽ�Ʈ���� ������Ű�� ���� ��!!
            dataInfoUI.SetActive(true);
        }

        public void Goback_Click()
        {
            SoundManager.Inst.PlaySFX("Click_on");
            DataInit();
        }

        public void Delete_Click()
        {
            SoundManager.Inst.PlaySFX("Click_on");
            dataChoiceUI.SetActive(false);
            dataInfoUI.SetActive(false);
            deleteUI.SetActive(true);
        }
        #endregion

        #region ������ ���� UI
        public void Load_Click() // �޷¾����� �Ѿ�� ��!
        {
            GameManager.Inst.Fade_InOut(false, 3.0f);
            Invoke("NextScene", 3.0f);
        }

        public void NextScene()
        {
<<<<<<< HEAD
            GameManager.Inst.AsyncLoadNextScene(SceneName.CalendarScene);
=======
            SoundManager.Inst.PlaySFX("Click_on");
            //GameManager.Inst.AsyncLoadNextScene("Forest3");
            GameManager.Inst.AsyncLoadNextScene("CalendarScene");
>>>>>>> Jun
        }

        public void Cancel_Click()
        {
            SoundManager.Inst.PlaySFX("Click_on");
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

