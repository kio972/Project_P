using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YeongJun
{
    public class BattleSceneManager : MonoBehaviour
    {
        public int area = 0; // ���� ������ȣ
        [SerializeField]
        private GameObject camGroup; // ����� ī�޶� �׷�

        private void Awake()
        {
            
        }

        public void ChangeCam(int number)
        {
            for (int i = 0; i < camGroup.transform.childCount; i++)
                camGroup.transform.GetChild(i).gameObject.SetActive(false);
            camGroup.transform.GetChild(number).gameObject.SetActive(true);
        }
    }
}
