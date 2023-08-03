using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using JinWon;

namespace JinWon
{
    public class RegionBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Button regionBtn;

        [SerializeField]
        private GameObject selectPoint;

        EventTrigger eventTrigger;

        [SerializeField]
        private JinWon.CalendarScene calendarScene;

        public void Init()
        {
            regionBtn = GetComponent<Button>();
            eventTrigger = GetComponent<EventTrigger>();

            if (selectPoint.activeSelf)
                selectPoint.SetActive(false);

            EventSystemInit();
        }

        private void EventSystemInit()
        {
            // 버튼이 선택될 때 호출되는 함수 등록!!
            EventTrigger.Entry onSelectEntry = new EventTrigger.Entry();
            onSelectEntry.eventID = EventTriggerType.Select;
            onSelectEntry.callback.AddListener((data) => { OnButtonSelected(); });
            eventTrigger.triggers.Add(onSelectEntry);

            // 버튼의 선택이 해제될 때 호출되는 함수 등록
            EventTrigger.Entry onDeselectEntry = new EventTrigger.Entry();
            onDeselectEntry.eventID = EventTriggerType.Deselect;
            onDeselectEntry.callback.AddListener((data) => { OnButtonDeselected(); });
            eventTrigger.triggers.Add(onDeselectEntry);
        }

        public void ForestPointActive()
        {
            selectPoint.SetActive(true);
        }

        private void OnButtonSelected() // 버튼이 셀렉트되면 호출하는 함수
        {
            if (calendarScene.SelectStep == 1)
            {
                selectPoint.SetActive(true);
            }
        }

        private void OnButtonDeselected() // 버튼이 셀렉트 풀리면 호출하는 함수
        {
            if (calendarScene.SelectStep == 1)
            {
                selectPoint.SetActive(false);
            }
        }

        public void OnPointerEnter(PointerEventData eventData) // 버튼 위에 올라갔을때
        {
            if (regionBtn != null)
            {
                EventSystem.current.SetSelectedGameObject(regionBtn.gameObject);
            }
        }

        public void OnPointerExit(PointerEventData eventData) // 버튼에서 나갔을때
        {
            if (regionBtn != null)
            {
                EventSystem.current.SetSelectedGameObject(regionBtn.gameObject);
            }
        }
    }

}

