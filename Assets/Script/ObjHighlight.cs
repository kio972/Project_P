using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using JinWon;

public class ObjHighlight : MonoBehaviour, IPointerEnterHandler
{
    public Sprite highlightImg;
    private CalendarScene calendarScene;

    private void Awake()
    {
        calendarScene = FindObjectOfType<CalendarScene>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        calendarScene.RefreshHighlight(this);
    }
}
