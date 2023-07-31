using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;

namespace JinWon
{
    public class CalendarUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject calendarCam;

        [SerializeField]
        private JinWon.MapUI mapUI;

        [SerializeField]
        private GameObject mapUIObj;

        public void MapBtnOnClick()
        {
            mapUIObj.SetActive(true);
            calendarCam.SetActive(false);
            mapUI.RegionInit();
            Invoke("CalendarUIActive", 2.0f);
        }

        private void CalendarUIActive()
        {
            gameObject.SetActive(false);
        }
    }
}

