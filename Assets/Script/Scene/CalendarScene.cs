using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JinWon
{
    public class CalendarScene : MonoBehaviour
    {
        private int whatMonth; // �ӽ� Month���� �Դϴ�.

        [SerializeField]
        private List<GameObject> calendarObjList = new List<GameObject>();

        void Start()
        {
            whatMonth = Random.Range(0, 12);
            calendarObjList[whatMonth].SetActive(true);
            Debug.Log("����ϱ�ƾƾƿ�? " + (whatMonth + 1));
        }

    }
}
    
