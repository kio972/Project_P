using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JinWon
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject continueObj; // 이어하기 버튼

        public void Init()
        {
            DataInit();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DataInit() // 기존의 데이터가 있는지 확인하는 함수
        {
            // 있으면 키고 없으면 끄기
            continueObj.SetActive(false); // 임시로 끔
        }
    }
}

