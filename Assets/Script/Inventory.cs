using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YeongJun
{
    public enum InvenState
    {
        main,
        click,
        use,
        equip,
        move,
    }

    public class Inventory : MonoBehaviour
    {
        [SerializeField] private GameObject slotPrefab; // 슬롯 프리팹
        [SerializeField] private Transform slotParent; // 슬롯 그리드
        [SerializeField] private GameObject textn; // 일반 텍스트
        [SerializeField] private GameObject texti; // 중요 텍스트
        public Slot[] slots;
        private bool mode = false; // 인벤토리 모드
        [SerializeField] private GameObject Menu;
        [SerializeField] private GameObject Assistant;
        [SerializeField] private GameObject selectCaracter; // 강조 효과
        [SerializeField] private GameObject selectPocket;
        [SerializeField] private GameObject selectItem;

        private RaycastHit hit;
        private int layerMask;

        [SerializeField]
        private Pocket[] pockets; // 캐릭터 주머니

        public InvenState state = new InvenState();
        public Slot selectSlot = null; // 선택된 슬롯

        private void Awake()
        {
            slots = new Slot[24];
            for (int i = 0; i < slots.Length; i++)
            {
                GameObject obj = Instantiate(slotPrefab);
                obj.transform.parent = slotParent;
                slots[i] = obj.GetComponent<Slot>();
                slots[i].slotNumber = i;
                slots[i].inventory = this;
            }
            layerMask = 1 << LayerMask.NameToLayer("UI");
        }

        public void ActiveInventory()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        public void RefreshSlot()
        {// 전체 슬롯 새로고침
            for (int i = 0; i < 24; i++)
                slots[i].Refresh(mode);
        }

        public void RefreshPocket()
        {
            for (int i = 0; i < pockets.Length; i++)
                pockets[i].Refresh();
        }

        public void ChangeMode()
        {
            mode = !mode;
            textn.SetActive(!mode);
            texti.SetActive(mode);
            RefreshSlot();
        }

        public void Sort()
        {// 슬롯 정렬
            if (mode)
                iSort(); // 중요 아이템 정렬
            else
                nSort(); // 일반 아이템 정렬
            RefreshSlot();
        }

        private void nSort()
        {
            int i, j;
            Item key;
            // 아이템 위로 모으기
            for (i = 0; i < InvenData.nItem.Length - 1; i++)
            {
                for (j = i + 1; j < InvenData.nItem.Length; j++)
                {
                    if (InvenData.nItem[i] == null)
                    {
                        if (InvenData.nItem[j] == null)
                            continue;
                        else
                        {
                            InvenData.nItem[i] = InvenData.nItem[j];
                            InvenData.nItem[j] = null;
                        }
                    }
                    else
                        break;
                }
            }
            // 아이템 정렬
            for (i = 1; i < InvenData.nItem.Length; i++)
            {
                key = InvenData.nItem[i];
                if (key != null)
                {
                    for (j = i - 1; j >= 0 && InvenData.nItem[j].uid > key.uid; j--)
                        InvenData.nItem[j + 1] = InvenData.nItem[j];
                    InvenData.nItem[j + 1] = key;
                }
                else
                    break;
            }
        }

        private void iSort()
        {
            int i, j;
            for (i = 0; i < InvenData.iItem.Length - 1; i++)
            {
                for (j = i + 1; j < InvenData.iItem.Length; j++)
                {
                    if (InvenData.iItem[i] == null)
                    {
                        if (InvenData.iItem[j] == null)
                            continue;
                        else
                        {
                            InvenData.iItem[i] = InvenData.iItem[j];
                            InvenData.iItem[j] = null;
                        }
                    }
                    else
                        break;
                }
            }
        }

        public bool GetItem(Item _item)
        {// 아이템을 인벤토리 배열에 추가하는 코드
            int n = InvenData.iItem.Length;
            if (_item.type == ItemType.important)
            {// 중요 아이템
                for (int i = 0; i < InvenData.iItem.Length; i++)
                {
                    if (InvenData.iItem[i] == null)
                    {
                        InvenData.iItem[i] = new Item();
                        InvenData.iItem[i].uid = _item.uid;
                        InvenData.iItem[i].name = _item.name;
                        InvenData.iItem[i].iconImg = _item.iconImg;
                        InvenData.iItem[i].count = _item.count;
                        InvenData.iItem[i].maxCount = _item.maxCount;
                        InvenData.iItem[i].type = _item.type;
                        InvenData.iItem[i].grade = _item.grade;
                        return true;
                    }
                }
            }
            else
            {// 일반 아이템
                for (int i = 0; i < InvenData.iItem.Length; i++)
                {
                    if (InvenData.nItem[i] == null && i < n)
                        n = i;
                    else if (InvenData.nItem[i] == _item && InvenData.nItem[i].count < InvenData.nItem[i].maxCount)
                    {// 같은 아이템이면 count증가
                        InvenData.nItem[i].count += _item.count;
                        return true;
                    }
                }
                InvenData.nItem[n] = new Item();
                InvenData.nItem[n].uid = _item.uid;
                InvenData.nItem[n].name = _item.name;
                InvenData.nItem[n].iconImg = _item.iconImg;
                InvenData.nItem[n].count = _item.count;
                InvenData.nItem[n].maxCount = _item.maxCount;
                InvenData.nItem[n].type = _item.type;
                InvenData.nItem[n].grade = _item.grade;
                return true;
            }
            return false;
        }

        public void SelectItem(int number)
        {// 아이템을 클릭 했을 때
            selectSlot = slots[number];
            if (mode)
            {// 중요 아이템일 경우
                //if (InvenData.iItem[number] != null)
                //{
                    Menu.SetActive(true);
                    if ((number + 1) % 8 == 0)
                        Menu.transform.position = new Vector3(slots[number].transform.position.x - 160, slots[number].transform.position.y - 120, 0);
                    else
                        Menu.transform.position = new Vector3(slots[number].transform.position.x + 160, slots[number].transform.position.y - 120, 0);
                    Menu.transform.GetChild(0).GetComponent<Button>().interactable = true;
                    Menu.transform.GetChild(1).GetComponent<Button>().interactable = true;
                //}
            }
            else
            {// 일반 아이템일 경우
                //if (InvenData.nItem[number] != null)
                //{
                    Menu.SetActive(true);
                    if ((number + 1) % 8 == 0)
                        Menu.transform.position = new Vector3(slots[number].transform.position.x - 160, slots[number].transform.position.y - 120, 0);
                    else
                        Menu.transform.position = new Vector3(slots[number].transform.position.x + 160, slots[number].transform.position.y - 120, 0);
                    // 장비라면 사용과 장착 비활성화
                    //if(InvenData.nItem[number].type == ItemType.equip)
                    //{
                        Menu.transform.GetChild(0).GetComponent<Button>().interactable = false;
                        Menu.transform.GetChild(1).GetComponent<Button>().interactable = false;
                    //}
                    //else
                    //{
                        Menu.transform.GetChild(0).GetComponent<Button>().interactable = true;
                        Menu.transform.GetChild(1).GetComponent<Button>().interactable = true;
                    //}
                //}
            }
            selectItem.SetActive(true);
            selectItem.GetComponent<Image>().color = Color.gray;
            selectItem.transform.position = selectSlot.transform.position;
            state = InvenState.click;
        }

        public void EnterSlot(int number)
        {
            selectSlot = slots[number];
            selectItem.SetActive(true);
            selectItem.GetComponent<Image>().color = Color.yellow;
            selectItem.transform.position = selectSlot.transform.position;
        }

        public void Btn_Use()
        {
            state = InvenState.use;
        }

        public void Btn_Equip()
        {

        }

        public void Btn_Move()
        {

        }

        public void Btn_Cancel()
        {
            Menu.SetActive(false);
            state = InvenState.main;
        }

        void Update()
        {
            switch (state)
            {
                case InvenState.main:
                    if (selectSlot != null)
                    {// 화살표로 선택한 슬롯 변경
                        EnterSlot(selectSlot.slotNumber);
                        if (Input.GetKeyDown(KeyCode.Space))
                            SelectItem(selectSlot.slotNumber);
                        if (Input.GetKeyDown(KeyCode.UpArrow))
                            selectSlot = slots[Mathf.Clamp(selectSlot.slotNumber - 8, 0, slots.Length)];
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                            selectSlot = slots[Mathf.Clamp(selectSlot.slotNumber + 8, 0, slots.Length)];
                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                            selectSlot = slots[Mathf.Clamp(selectSlot.slotNumber - 1, 0, slots.Length)];
                        if (Input.GetKeyDown(KeyCode.RightArrow))
                            selectSlot = slots[Mathf.Clamp(selectSlot.slotNumber + 1, 0, slots.Length)];
                    }
                    break;
                case InvenState.click:
                    if (Input.GetKeyDown(KeyCode.Escape))
                        Btn_Cancel();
                    break;
                case InvenState.use:
                    break;
                case InvenState.equip:
                    break;
                case InvenState.move:
                    break;
            }
        }
    }
}
