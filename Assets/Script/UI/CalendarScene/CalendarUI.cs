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

        public void MapBtnOnClick()
        {
            calendarCam.SetActive(false);
            mapUI.RegionInit();
        }
    }
}

