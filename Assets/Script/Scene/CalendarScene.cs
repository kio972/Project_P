using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

        public Texture2D cursorTexture;
        public Vector2 hotspot = Vector2.zero;
        public CursorMode cursorMode = CursorMode.Auto;

        [SerializeField]
        private JinWon.MapUI mapUI;

        [SerializeField]
        private GameObject calendarCam;
        [SerializeField]
        private GameObject mapCam;

        [SerializeField]
        private GameObject calendarUIObj;
        [SerializeField]
        private GameObject mapUIObj;

        [SerializeField]
        private GameObject calendarMove; // 달력으로 이동하는 버튼

        private int selectStep; // 셀렉트 단계 ( 뒤로가기 할때 활용 )
        public int SelectStep // RegionBtn 스크립트에서 사용!
        {
            get { return selectStep; }
            set { selectStep = value; }
        }

        private Coroutine UIMoveCorutine = null;

        void Start()
        {
            mapUI.Init();
            Cursor.SetCursor(cursorTexture, hotspot, cursorMode);

            fade.Fade_InOut(true, 3.0f);
            whatMonth = Random.Range(0, 12);
            calendarObjList[whatMonth].SetActive(true);
            Debug.Log("몇월일까아아아용? " + (whatMonth + 1));

            selectStep = 0;
        }

        public void UIMove()
        {
            if (UIMoveCorutine != null)
                StopCoroutine(UIMoveCorutine);
            UIMoveCorutine = StartCoroutine(UIChangeCorutine());
            Debug.Log(UIMoveCorutine);
        }

        IEnumerator UIChangeCorutine()
        {
            switch (selectStep)
            {
                case 0: // 맵으로 가지는 상태
                    {
                        mapUIObj.SetActive(true);
                        mapUI.RegionInit();
                        calendarCam.SetActive(false);
                        yield return JinWon.YieldInstructionCache.WaitForSeconds(2.0f);
                        
                        calendarMove.SetActive(true);
                        calendarUIObj.SetActive(false);


                        mapUI.SelectMove = true;
                        selectStep++;
                        break;
                    }

                case 1: // 달력으로 가지는 상태
                    {
                        calendarUIObj.SetActive(true);
                        /*if (!mapCam.activeSelf) // 마왕성 확대중에 달력가는 코드를 눌렀을때를 방지하는 임시코드
                            GobackOnClick("castle");*/

                        calendarCam.SetActive(true);
                        calendarMove.SetActive(false);

                        mapUI.RegionInit();

                        yield return JinWon.YieldInstructionCache.WaitForSeconds(2.0f);
                        mapUIObj.SetActive(false);

                        mapUI.SelectMove = false;
                        selectStep--;
                        mapUI.RegionBtnInteractable(true);
                        break;
                    }
                case 2: // 지역 확대가 풀리는 상태
                    {
                        selectStep--;
                        mapCam.SetActive(true);

                        // 지역 확대를 풀때 지역캠이 켜진게 있으면 모두 꺼버리기.
                        mapUI.RegionCamActive(false);

                        mapUI.RegionBtnInteractable(true);
                        
                        mapUI.SelectMove = true;
                        break;
                    }
            }
            

            //UIMoveCorutine = null;
            yield return null;
        }



        
    }
}
    
