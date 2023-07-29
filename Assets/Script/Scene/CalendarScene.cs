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

        private int whatMonth; // �ӽ� Month���� �Դϴ�.

        [SerializeField]
        private List<GameObject> calendarObjList = new List<GameObject>();

        public Texture2D cursorTexture;
        public Vector2 hotspot = Vector2.zero;
        public CursorMode cursorMode = CursorMode.Auto;

        [SerializeField]
        private JinWon.MapUI mapUI;

        void Start()
        {
            mapUI.Init();
            Cursor.SetCursor(cursorTexture, hotspot, cursorMode);

            fade.Fade_InOut(true, 3.0f);
            whatMonth = Random.Range(0, 12);
            calendarObjList[whatMonth].SetActive(true);
            Debug.Log("����ϱ�ƾƾƿ�? " + (whatMonth + 1));
        }

    }
}
    
