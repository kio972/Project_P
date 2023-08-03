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

        private int whatMonth; // �ӽ� Month���� �Դϴ�.

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
        private GameObject calendarMove; // �޷����� �̵��ϴ� ��ư

        private int selectStep; // ����Ʈ �ܰ� ( �ڷΰ��� �Ҷ� Ȱ�� )
        public int SelectStep // RegionBtn ��ũ��Ʈ���� ���!
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
            Debug.Log("����ϱ�ƾƾƿ�? " + (whatMonth + 1));

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
                case 0: // ������ ������ ����
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

                case 1: // �޷����� ������ ����
                    {
                        calendarUIObj.SetActive(true);
                        /*if (!mapCam.activeSelf) // ���ռ� Ȯ���߿� �޷°��� �ڵ带 ���������� �����ϴ� �ӽ��ڵ�
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
                case 2: // ���� Ȯ�밡 Ǯ���� ����
                    {
                        selectStep--;
                        mapCam.SetActive(true);

                        // ���� Ȯ�븦 Ǯ�� ����ķ�� ������ ������ ��� ��������.
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
    
