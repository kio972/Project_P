using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YeongJun
{
    public class Clock : MonoBehaviour
    {
        [SerializeField]
        private GameObject clock;

        private Quaternion start;
        private Quaternion end;
        bool day = true;
        [SerializeField]
        bool active = false;

        public IEnumerator IChangeTime()
        {// �ð谡 ȸ���ϴ� �ڷ�ƾ
            active = true;
            float elapsedTime = 0;
            float targetTime = 2.0f;
            day = !day;
            if (day)
            {
                start = Quaternion.Euler(0, 0, 180);
                end = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                start = Quaternion.Euler(0, 0, 360);
                end = Quaternion.Euler(0, 0, 180);
            }
            while (elapsedTime < targetTime)
            {
                elapsedTime += Time.deltaTime;
                clock.transform.rotation = Quaternion.Lerp(start, end, elapsedTime / targetTime);
                yield return null;
            }
            active = false;
            yield return null;
        }

        public void ChangeTime()
        {
            if (!active)
                StartCoroutine(IChangeTime());
        }
    }
}