using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JinWon
{
    public class CalendarScene : MonoBehaviour
    {
        private int whatMonth; // 임시 Month변수 입니다.

        [SerializeField]
        private List<GameObject> calendarObjList = new List<GameObject>();

        void Start()
        {
            whatMonth = Random.Range(0, 12);
            calendarObjList[whatMonth].SetActive(true);
            Debug.Log("몇월일까아아아용? " + (whatMonth + 1));
        }

    }
}
    
