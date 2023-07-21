using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;

namespace JinWon
{
    public class CalendarScene : MonoBehaviour
    {
        [SerializeField]
        private FadeInOut fade;

        private int whatMonth; // 임시 Month변수 입니다.

        [SerializeField]
        private List<GameObject> calendarObjList = new List<GameObject>();

        void Start()
        {
            fade.Fade_InOut(true, 3.0f);

            whatMonth = Random.Range(0, 12);
            calendarObjList[whatMonth].SetActive(true);
            Debug.Log("몇월일까아아아용? " + (whatMonth + 1));
        }

    }
}
    
