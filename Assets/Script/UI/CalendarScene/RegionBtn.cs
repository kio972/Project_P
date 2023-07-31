using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JinWon
{
    public class RegionBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Button regionBtn;

        private void Start()
        {
            regionBtn = GetComponent<Button>();
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

