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
            // ��ư�� ���õ� �� ȣ��Ǵ� �Լ� ���!!
            EventTrigger.Entry onSelectEntry = new EventTrigger.Entry();
            onSelectEntry.eventID = EventTriggerType.Select;
            onSelectEntry.callback.AddListener((data) => { OnButtonSelected(); });
            eventTrigger.triggers.Add(onSelectEntry);

            // ��ư�� ������ ������ �� ȣ��Ǵ� �Լ� ���
            EventTrigger.Entry onDeselectEntry = new EventTrigger.Entry();
            onDeselectEntry.eventID = EventTriggerType.Deselect;
            onDeselectEntry.callback.AddListener((data) => { OnButtonDeselected(); });
            eventTrigger.triggers.Add(onDeselectEntry);
        }

        public void ForestPointActive()
        {
            selectPoint.SetActive(true);
        }

        private void OnButtonSelected() // ��ư�� ����Ʈ�Ǹ� ȣ���ϴ� �Լ�
        {
            if (calendarScene.SelectStep == 1)
            {
                selectPoint.SetActive(true);
            }
        }

        private void OnButtonDeselected() // ��ư�� ����Ʈ Ǯ���� ȣ���ϴ� �Լ�
        {
            if (calendarScene.SelectStep == 1)
            {
                selectPoint.SetActive(false);
            }
        }

        public void OnPointerEnter(PointerEventData eventData) // ��ư ���� �ö�����
        {
            if (regionBtn != null)
            {
                EventSystem.current.SetSelectedGameObject(regionBtn.gameObject);
            }
        }

        public void OnPointerExit(PointerEventData eventData) // ��ư���� ��������
        {
            if (regionBtn != null)
            {
                EventSystem.current.SetSelectedGameObject(regionBtn.gameObject);
            }
        }
    }

}

