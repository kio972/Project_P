using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jun
{
    public class PlayerHPScript : MonoBehaviour
    {
        [SerializeField]
        private Image hpbar;
        private Controller target;

        [SerializeField]
        private Image mpbar;

        // HP 초기 설정
        public void Init(Controller target)
        {
            this.target = target;
            hpbar.fillAmount = target._CurrHp / target._MaxHp;
            mpbar.fillAmount = target._CurrMP / target._MaxMP;

        }

        // Update is called once per frame
        /*void Update()
        {
            if (target == null)
                return;

            HandleHP();
        }*/

        // HP 게이지 소모
        public void HandleHP()
        {
            hpbar.fillAmount = target._CurrHp / target._MaxHp;
            //hpbar.fillAmount = Mathf.Lerp(hpbar.fillAmount, target._CurrHp / target._MaxHp, Time.deltaTime * 10);
            Debug.Log("플레이어의의 체력 : " + target._CurrHp + " 최대 : " + target._MaxHp);
        }

        public void HandleMP()
        {
            mpbar.fillAmount = target._CurrMP / target._MaxMP;

            //mpbar.fillAmount = Mathf.Lerp(mpbar.fillAmount, target._CurrMP / target._MaxMP, Time.deltaTime * 10);
            Debug.Log("플레이어의의 마나 : " + target._CurrMP + " 최대 : " + target._MaxMP);
        }
    }
}

