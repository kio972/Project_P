using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jun
{
    public class PlayerHPScript : MonoBehaviour
    {
        [SerializeField]
        private Slider HPbar;
        private float maxHP = 100;
        private float curHP = 100;
        float HP;

        // HP 초기 설정
        void Start()
        {
            HPbar.value = (float)curHP / (float)maxHP;
        }

        // Update is called once per frame
        void Update()
        {

            // HP 소모 테스트
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (curHP > 0)
                {
                    curHP -= 10;
                }
                else
                {
                    curHP = 0;
                }
                HP = (float)curHP / (float)maxHP;
            }

            HandleHP();
        }

        // HP 게이지 소모
        private void HandleHP()
        {
            HPbar.value = Mathf.Lerp(HPbar.value, (float)curHP / (float)maxHP, Time.deltaTime * 10);
        }


    }
}

