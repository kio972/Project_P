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

        // HP �ʱ� ����
        public void Init(Controller target)
        {
            this.target = target;
            hpbar.fillAmount = target._CurrHp / target._MaxHp;
        }

        // Update is called once per frame
        void Update()
        {
            if (target == null)
                return;

            HandleHP();
        }

        // HP ������ �Ҹ�
        private void HandleHP()
        {
            hpbar.fillAmount = Mathf.Lerp(hpbar.fillAmount, target._CurrHp / target._MaxHp, Time.deltaTime * 10);
        }
    }
}

