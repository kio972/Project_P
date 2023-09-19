using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YeongJun
{
    public enum InvenState
    {
        main, // 기본 상태
        control, // 아이템을 클릭했을 때
        use, // 사용할 캐릭터를 선택할 때
        equip, // 장착할 슬롯을 선택할 때
        equip_count, // 장착할 아이템의 수량을 지정할 때
        move, // 이동할 아이템 개수를 지정할 때
        moveItem, // 아이템을 옮길 위치를 지정할 때
        unequip, // 아이템 장착을 해제할 때
        fill, // 아이템 채우기를 눌렀을 때
        drop, // 아이템 버리기를 눌렀을 때
    }

    public class Inventory : MonoBehaviour
    {
        public InvenState state = new InvenState();

        [SerializeField] private GameObject slotPrefab; // 슬롯 프리팹
        [SerializeField] private Transform slotParent; // 슬롯 그리드
        [SerializeField] private GameObject textn; // 일반 텍스트
        [SerializeField] private GameObject texti; // 중요 텍스트
        public Slot[] slots;
        private bool mode = false; // 인벤토리 모드
        [SerializeField] private GameObject Menu;
        [SerializeField] private GameObject assistant;
        [SerializeField] private GameObject equipMenu;
        [SerializeField] private GameObject cancel;
        [SerializeField] private GameObject selectCaracter; // 강조 효과
        [SerializeField] private GameObject selectPocket;
        [SerializeField] private GameObject selectItem;
        [SerializeField] private GameObject selectMenu;
        [SerializeField] private GameObject selectEquipMenu;
        [SerializeField] private GameObject movingItem; // 옮길 아이템 UI
        public Item pocketItem;
        public int assistCount = -1;
        public int pocketNum = 0;

        private RaycastHit hit;
        private int layerMask;

        [SerializeField] private Portrait[] character; // 캐릭터 초상화
        [SerializeField] private Pocket[] pockets; // 캐릭터 주머니

        public Slot selectSlot = null; // 참조중인 슬롯
        public Item tempItem = null; // 옮길 아이템
        public ItemPool itemPool;
        public Controller[] player;

        private void Awake()
        {
            slots = new Slot[24];
            for (int i = 0; i < slots.Length; i++)
            {
                GameObject obj = Instantiate(slotPrefab);
                obj.transform.SetParent(slotParent);
                slots[i] = obj.GetComponent<Slot>();
                slots[i].slotNumber = i;
                slots[i].inventory = this;
            }
            layerMask = 1 << LayerMask.NameToLayer("UI");
        }

        public void ActiveInventory()
        {// 인벤토리를 여는 함수
            gameObject.SetActive(!gameObject.activeSelf);
            if (gameObject.activeSelf)
                Main();
        }

        public void Main()
        {// 인벤토리를 열었을 때 실행
            state = InvenState.main;
            Menu.SetActive(false);
            assistant.SetActive(false);
            selectCaracter.SetActive(false);
            selectPocket.SetActive(false);
            selectItem.SetActive(false);
            selectMenu.SetActive(false);
            selectItem.GetComponent<Image>().color = Color.yellow;
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
            if (state == InvenState.main)
            {
                mode = !mode;
                textn.SetActive(!mode);
                texti.SetActive(mode);
                RefreshSlot();
            }
        }

        public void Sort()
        {// 슬롯 정렬
            if (state == InvenState.main)
            {
                if (mode)
                    iSort(); // 중요 아이템 정렬
                else
                    nSort(); // 일반 아이템 정렬
                RefreshSlot();
            }
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

        public bool GetItem(Item _Item)
        {// 아이템을 인벤토리 배열에 추가하는 코드
            int n = InvenData.iItem.Length;
            tempItem = new Item();
            tempItem.uid = _Item.uid;
            tempItem.name = _Item.name;
            tempItem.iconImg = _Item.iconImg;
            tempItem.count = _Item.count;
            tempItem.maxCount = _Item.maxCount;
            tempItem.type = _Item.type;
            tempItem.grade = _Item.grade;
            if (tempItem.type == ItemType.important)
            {// 중요 아이템
                for (int i = 0; i < InvenData.iItem.Length; i++)
                {
                    if (InvenData.iItem[i] == null)
                    {
                        InvenData.iItem[i] = new Item();
                        InvenData.iItem[i].uid = tempItem.uid;
                        InvenData.iItem[i].name = tempItem.name;
                        InvenData.iItem[i].iconImg = tempItem.iconImg;
                        InvenData.iItem[i].count = tempItem.count;
                        InvenData.iItem[i].maxCount = tempItem.maxCount;
                        InvenData.iItem[i].type = tempItem.type;
                        InvenData.iItem[i].grade = tempItem.grade;
                        return true;
                    }
                }
            }
            else
            {// 일반 아이템
                for (int i = 0; i < InvenData.iItem.Length; i++)
                {
                    if (InvenData.nItem[i] == null)
                    {
                        if (i < n)
                            n = i;
                    }
                    else if (InvenData.nItem[i].uid == tempItem.uid && InvenData.nItem[i].count < InvenData.nItem[i].maxCount)
                    {// 같은 아이템이면 count증가
                        if(InvenData.nItem[i].maxCount < InvenData.nItem[i].count + tempItem.count)
                        {// 획득한 아이템의 수와 기존 수의 합이 번들 최대 개수보다 많다면
                            tempItem.count = InvenData.nItem[i].count + tempItem.count - InvenData.nItem[i].maxCount;
                            InvenData.nItem[i].count = InvenData.nItem[i].maxCount;
                            n++;
                        }
                        else
                        {
                            InvenData.nItem[i].count += tempItem.count;
                            return true;
                        }
                    }
                }
                InvenData.nItem[n] = new Item();
                InvenData.nItem[n].uid = tempItem.uid;
                InvenData.nItem[n].name = tempItem.name;
                InvenData.nItem[n].iconImg = tempItem.iconImg;
                InvenData.nItem[n].count = tempItem.count;
                InvenData.nItem[n].maxCount = tempItem.maxCount;
                InvenData.nItem[n].type = tempItem.type;
                InvenData.nItem[n].grade = tempItem.grade;
                return true;
            }
            return false;
        }

        #region main
        public void SelectItem(int number)
        {// 아이템을 클릭 했을 때
            selectSlot = slots[number];
            if (mode)
            {// 중요 아이템일 경우
                if (InvenData.iItem[number] != null)
                {
                    Menu.SetActive(true);
                    if ((number + 1) % 8 == 0)
                        Menu.transform.position = slots[number].transform.position + new Vector3(-160, -120, 0);
                    else
                        Menu.transform.position = slots[number].transform.position + new Vector3(160, -120, 0);
                    Menu.transform.GetChild(0).GetComponent<Button>().interactable = true;
                    Menu.transform.GetChild(1).GetComponent<Button>().interactable = true;
                    selectItem.GetComponent<Image>().color = Color.gray;
                    selectItem.transform.position = selectSlot.transform.position;
                    state = InvenState.control;
                }
            }
            else
            {// 일반 아이템일 경우
                if (InvenData.nItem[number] != null)
                {
                    Menu.SetActive(true);
                    if ((number + 1) % 8 == 0)
                        Menu.transform.position = slots[number].transform.position + new Vector3(-160, -120, 0);
                    else
                        Menu.transform.position = slots[number].transform.position + new Vector3(160, -120, 0);
                    // 장비라면 사용과 장착 비활성화
                    if(InvenData.nItem[number].type == ItemType.equip)
                    {
                        Menu.transform.GetChild(0).GetChild(0).GetComponent<Button>().interactable = false;
                        Menu.transform.GetChild(0).GetChild(1).GetComponent<Button>().interactable = false;
                        Menu.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().color = Color.red;
                        Menu.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().color = Color.red;
                    }
                    else
                    {
                        Menu.transform.GetChild(0).GetChild(0).GetComponent<Button>().interactable = true;
                        Menu.transform.GetChild(0).GetChild(1).GetComponent<Button>().interactable = true;
                        Menu.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().color = Color.white;
                        Menu.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().color = Color.white;
                    }
                    selectItem.SetActive(true);
                    selectItem.GetComponent<Image>().color = Color.gray;
                    selectItem.transform.position = selectSlot.transform.position;
                    state = InvenState.control;
                }
            }
        }

        public void EnterSlot(int number)
        {// 슬롯에 마우스를 올렸을 때 강조표시
            selectSlot = slots[number];
            selectItem.SetActive(true);
            selectItem.GetComponent<Image>().color = Color.yellow;
            selectItem.transform.position = selectSlot.transform.position;
        }

        public void EquipMenu()
        {// 장착한 아이템 메뉴 출력
            state = InvenState.unequip;
                bool sameItem = false;
                switch (pocketNum)
                {
                    case 0:
                        pocketItem = InvenData.Pocket1[0];
                        break;
                    case 1:
                        pocketItem = InvenData.Pocket1[1];
                        break;
                    case 2:
                        pocketItem = InvenData.Pocket2[0];
                        break;
                    case 3:
                        pocketItem = InvenData.Pocket2[1];
                        break;
                    case 4:
                        pocketItem = InvenData.Pocket3[0];
                        break;
                    case 5:
                        pocketItem = InvenData.Pocket3[1];
                        break;
                }
                selectPocket.GetComponent<Image>().color = Color.gray;
                if (pocketItem.count == pocketItem.maxCount)
                {
                    equipMenu.transform.GetChild(0).GetChild(1).GetComponent<Button>().interactable = false;
                    equipMenu.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().color = Color.red;
                }
                else
                {
                    for (int i = 0; i < InvenData.nItem.Length; i++)
                    {
                        if (InvenData.nItem[i] != null && pocketItem.uid == InvenData.nItem[i].uid)
                        {
                            sameItem = true;
                            selectSlot = slots[i];
                            break;
                        }
                    }
                    if (sameItem)
                    {
                        equipMenu.transform.GetChild(0).GetChild(1).GetComponent<Button>().interactable = true;
                        equipMenu.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().color = Color.white;
                    }
                    else
                    {
                        equipMenu.transform.GetChild(0).GetChild(1).GetComponent<Button>().interactable = false;
                        equipMenu.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().color = Color.red;
                    }
                }
                equipMenu.SetActive(true);
                equipMenu.transform.position = selectPocket.transform.position + new Vector3(160, -90, 0);
            pocketItem = null;
        }
        #endregion

        #region Menu
        public void EnterMenu(Transform t)
        {// 메뉴에 마우스를 올렸을 때 강조표시
            selectMenu.SetActive(true);
            selectMenu.GetComponent<Image>().color = Color.yellow;
            selectMenu.transform.position = t.position;
        }
        #endregion

        #region Btn_Use
        public void Btn_Use()
        {// 사용버튼 클릭
            if (state == InvenState.control)
            {
                state = InvenState.use;
                Menu.SetActive(false);
                EnterPortrait(character[0].transform);
                cancel.SetActive(true);
                cancel.transform.position = selectSlot.transform.position + new Vector3(0, -90, 0);
            }
        }

        public void EnterPortrait(Transform t)
        {// 캐릭터 초상화에 마우스를 올렸을 때 강조표시
            selectCaracter.SetActive(true);
            selectCaracter.transform.position = t.position;
        }

        public void SelectPortrait(int number)
        {// 캐릭터에 아이템 사용
            switch (number)
            {
                case 0:
                    // 1번 캐릭터에 아이템을 사용하는 함수
                    break;
                case 1:
                    // 2번 캐릭터에 아이템을 사용하는 함수
                    break;
                case 2:
                    // 3번 캐릭터에 아이템을 사용하는 함수
                    break;
            }
            InvenData.iItem[selectSlot.slotNumber].count--;
            selectSlot.Refresh(mode);
        }

        public void Cancel()
        {
            state = InvenState.control;
            selectCaracter.SetActive(false);
            selectPocket.SetActive(false);
            SelectItem(selectSlot.slotNumber);
            cancel.SetActive(false);
        }
        #endregion

        #region Btn_Equip
        public void Btn_Equip()
        {
            state = InvenState.equip;
            Menu.SetActive(false);
            selectPocket.SetActive(true);
            EnterPocket(pockets[0].transform);
            cancel.SetActive(true);
            cancel.transform.position = selectSlot.transform.position + new Vector3(0, -90, 0);
        }

        public void EnterPocket(Transform t)
        {// 캐릭터 주머니에 마우스를 올렸을 때 강조표시
            selectPocket.SetActive(true);
            selectPocket.GetComponent<Image>().color = Color.yellow;
            selectPocket.transform.position = t.position;
        }

        public void SelectPocket(int number)
        {// 주머니를 선택했을 때 수량 입력 창 띄우기
            state = InvenState.equip_count;
            pocketNum = number;
            cancel.SetActive(false);
            assistant.SetActive(true);
            assistant.transform.position = pockets[number].transform.position + new Vector3(100, 0, 0);
            assistant.GetComponent<Assistant>().Init();
        }

        public bool EquipItem(int count)
        {// 아이템 장착
            switch (pocketNum)
            {
                case 0:
                    if (InvenData.Pocket1[0] != null)
                    {
                        if (assistCount != -1 && InvenData.Pocket1[0].uid == InvenData.nItem[assistCount].uid)
                        {
                            InvenData.Pocket1[0].count += count;
                            InvenData.nItem[selectSlot.slotNumber].count -= count;
                            pockets[pocketNum].Refresh();
                            Main();
                            return true;
                        }
                    }
                    else
                    {
                        InvenData.Pocket1[0] = new Item();
                        InvenData.Pocket1[0].uid = InvenData.nItem[selectSlot.slotNumber].uid;
                        InvenData.Pocket1[0].name = InvenData.nItem[selectSlot.slotNumber].name;
                        InvenData.Pocket1[0].iconImg = InvenData.nItem[selectSlot.slotNumber].iconImg;
                        InvenData.Pocket1[0].count = count;
                        InvenData.Pocket1[0].maxCount = InvenData.nItem[selectSlot.slotNumber].maxCount;
                        InvenData.Pocket1[0].type = InvenData.nItem[selectSlot.slotNumber].type;
                        InvenData.Pocket1[0].grade = InvenData.nItem[selectSlot.slotNumber].grade;
                        InvenData.nItem[selectSlot.slotNumber].count -= count;
                        pockets[pocketNum].Refresh();
                        Main();
                        return true;
                    }
                    break;
                case 1:
                    if (InvenData.Pocket1[1] != null)
                    {
                        if (assistCount != -1 && InvenData.Pocket1[1].uid == InvenData.nItem[assistCount].uid)
                        {
                            InvenData.Pocket1[1].count += count;
                            InvenData.nItem[selectSlot.slotNumber].count -= count;
                            pockets[pocketNum].Refresh();
                            Main();
                            return true;
                        }
                    }
                    else
                    {
                        InvenData.Pocket1[1] = new Item();
                        InvenData.Pocket1[1].uid = InvenData.nItem[selectSlot.slotNumber].uid;
                        InvenData.Pocket1[1].name = InvenData.nItem[selectSlot.slotNumber].name;
                        InvenData.Pocket1[1].iconImg = InvenData.nItem[selectSlot.slotNumber].iconImg;
                        InvenData.Pocket1[1].count = count;
                        InvenData.Pocket1[1].maxCount = InvenData.nItem[selectSlot.slotNumber].maxCount;
                        InvenData.Pocket1[1].type = InvenData.nItem[selectSlot.slotNumber].type;
                        InvenData.Pocket1[1].grade = InvenData.nItem[selectSlot.slotNumber].grade;
                        InvenData.nItem[selectSlot.slotNumber].count -= count;
                        pockets[pocketNum].Refresh();
                        Main();
                        return true;
                    }
                    break;
                case 2:
                    if (InvenData.Pocket2[0] != null)
                    {
                        if (assistCount != -1 && InvenData.Pocket2[0].uid == InvenData.nItem[assistCount].uid)
                        {
                            InvenData.Pocket2[0].count += count;
                            InvenData.nItem[selectSlot.slotNumber].count -= count;
                            pockets[pocketNum].Refresh();
                            Main();
                            return true;
                        }
                    }
                    else
                    {
                        InvenData.Pocket2[0] = new Item();
                        InvenData.Pocket2[0].uid = InvenData.nItem[selectSlot.slotNumber].uid;
                        InvenData.Pocket2[0].name = InvenData.nItem[selectSlot.slotNumber].name;
                        InvenData.Pocket2[0].iconImg = InvenData.nItem[selectSlot.slotNumber].iconImg;
                        InvenData.Pocket2[0].count = count;
                        InvenData.Pocket2[0].maxCount = InvenData.nItem[selectSlot.slotNumber].maxCount;
                        InvenData.Pocket2[0].type = InvenData.nItem[selectSlot.slotNumber].type;
                        InvenData.Pocket2[0].grade = InvenData.nItem[selectSlot.slotNumber].grade;
                        InvenData.nItem[selectSlot.slotNumber].count -= count;
                        pockets[pocketNum].Refresh();
                        Main();
                        return true;
                    }
                    break;
                case 3:
                    if (InvenData.Pocket2[1] != null)
                    {
                        if (assistCount != -1 && InvenData.Pocket2[1].uid == InvenData.nItem[assistCount].uid)
                        {
                            InvenData.Pocket2[1].count += count;
                            InvenData.nItem[selectSlot.slotNumber].count -= count;
                            pockets[pocketNum].Refresh();
                            Main();
                            return true;
                        }
                    }
                    else
                    {
                        InvenData.Pocket2[1] = new Item();
                        InvenData.Pocket2[1].uid = InvenData.nItem[selectSlot.slotNumber].uid;
                        InvenData.Pocket2[1].name = InvenData.nItem[selectSlot.slotNumber].name;
                        InvenData.Pocket2[1].iconImg = InvenData.nItem[selectSlot.slotNumber].iconImg;
                        InvenData.Pocket2[1].count = count;
                        InvenData.Pocket2[1].maxCount = InvenData.nItem[selectSlot.slotNumber].maxCount;
                        InvenData.Pocket2[1].type = InvenData.nItem[selectSlot.slotNumber].type;
                        InvenData.Pocket2[1].grade = InvenData.nItem[selectSlot.slotNumber].grade;
                        InvenData.nItem[selectSlot.slotNumber].count -= count;
                        pockets[pocketNum].Refresh();
                        Main();
                        return true;
                    }
                    break;
                case 4:
                    if (InvenData.Pocket3[0] != null)
                    {
                        if (assistCount != -1 && InvenData.Pocket3[0].uid == InvenData.nItem[assistCount].uid)
                        {
                            InvenData.Pocket3[0].count += count;
                            InvenData.nItem[selectSlot.slotNumber].count -= count;
                            pockets[pocketNum].Refresh();
                            Main();
                            return true;
                        }
                    }
                    else
                    {
                        InvenData.Pocket3[0] = new Item();
                        InvenData.Pocket3[0].uid = InvenData.nItem[selectSlot.slotNumber].uid;
                        InvenData.Pocket3[0].name = InvenData.nItem[selectSlot.slotNumber].name;
                        InvenData.Pocket3[0].iconImg = InvenData.nItem[selectSlot.slotNumber].iconImg;
                        InvenData.Pocket3[0].count = count;
                        InvenData.Pocket3[0].maxCount = InvenData.nItem[selectSlot.slotNumber].maxCount;
                        InvenData.Pocket3[0].type = InvenData.nItem[selectSlot.slotNumber].type;
                        InvenData.Pocket3[0].grade = InvenData.nItem[selectSlot.slotNumber].grade;
                        InvenData.nItem[selectSlot.slotNumber].count -= count;
                        pockets[pocketNum].Refresh();
                        Main();
                        return true;
                    }
                    break;
                case 5:
                    if (InvenData.Pocket3[1] != null)
                    {
                        if (assistCount != -1 && InvenData.Pocket3[1].uid == InvenData.nItem[assistCount].uid)
                        {
                            InvenData.Pocket3[1].count += count;
                            InvenData.nItem[selectSlot.slotNumber].count -= count;
                            pockets[pocketNum].Refresh();
                            Main();
                            return true;
                        }
                    }
                    else
                    {
                        InvenData.Pocket3[1] = new Item();
                        InvenData.Pocket3[1].uid = InvenData.nItem[selectSlot.slotNumber].uid;
                        InvenData.Pocket3[1].name = InvenData.nItem[selectSlot.slotNumber].name;
                        InvenData.Pocket3[1].iconImg = InvenData.nItem[selectSlot.slotNumber].iconImg;
                        InvenData.Pocket3[1].count = count;
                        InvenData.Pocket3[1].maxCount = InvenData.nItem[selectSlot.slotNumber].maxCount;
                        InvenData.Pocket3[1].type = InvenData.nItem[selectSlot.slotNumber].type;
                        InvenData.Pocket3[1].grade = InvenData.nItem[selectSlot.slotNumber].grade;
                        InvenData.nItem[selectSlot.slotNumber].count -= count;
                        pockets[pocketNum].Refresh();
                        Main();
                        return true;
                    }
                    break;
                default:
                    break;
            }
            return false;
        }
        #endregion

        #region Btn_Move
        public void Btn_Move()
        {// 아이템 이동
            if(state == InvenState.control)
            {
                state = InvenState.move;
                Menu.SetActive(false);
                assistant.SetActive(true);
                assistant.transform.position = selectSlot.transform.position + new Vector3(100, 0, 0);
                assistant.GetComponent<Assistant>().Init();
            }
        }

        public void SelectItem(Item item, int count)
        {
            state = InvenState.moveItem;
            tempItem = new Item();
            tempItem.uid = item.uid;
            tempItem.name = item.name;
            tempItem.iconImg = item.iconImg;
            tempItem.count = count;
            tempItem.maxCount = item.maxCount;
            tempItem.type = item.type;
            tempItem.grade = item.grade;
            InvenData.nItem[selectSlot.slotNumber].count -= count;
            movingItem.SetActive(true);
            movingItem.GetComponent<Image>().sprite = item.iconImg;
            RefreshSlot();
        }

        public void MoveItem()
        {
            if(InvenData.nItem[selectSlot.slotNumber] == null)
            {
                InvenData.nItem[selectSlot.slotNumber] = new Item();
                InvenData.nItem[selectSlot.slotNumber].uid = tempItem.uid;
                InvenData.nItem[selectSlot.slotNumber].name = tempItem.name;
                InvenData.nItem[selectSlot.slotNumber].iconImg = tempItem.iconImg;
                InvenData.nItem[selectSlot.slotNumber].count = tempItem.count;
                InvenData.nItem[selectSlot.slotNumber].maxCount = tempItem.maxCount;
                InvenData.nItem[selectSlot.slotNumber].type = tempItem.type;
                InvenData.nItem[selectSlot.slotNumber].grade = tempItem.grade;
                movingItem.SetActive(false);
                tempItem = null;
                Main();
                RefreshSlot();
            }
            else
            {
                if(InvenData.nItem[selectSlot.slotNumber].uid == tempItem.uid)
                {
                    if(InvenData.nItem[selectSlot.slotNumber].maxCount < InvenData.nItem[selectSlot.slotNumber].count + tempItem.count)
                    {
                        tempItem.count -= InvenData.nItem[selectSlot.slotNumber].count + tempItem.count - InvenData.nItem[selectSlot.slotNumber].maxCount;
                        InvenData.nItem[selectSlot.slotNumber].count = InvenData.nItem[selectSlot.slotNumber].maxCount;
                        if(tempItem.count <= 0)
                        {
                            movingItem.SetActive(false);
                            tempItem = null;
                            Main();
                        }
                        RefreshSlot();
                    }
                }
            }
        }
        #endregion

        #region Btn_Drop
        public void Btn_Drop()
        {
            state = InvenState.drop;
            assistant.SetActive(true);
            Menu.SetActive(false);
            assistant.transform.position = selectSlot.transform.position + new Vector3(100, 0, 0);
            assistant.GetComponent<Assistant>().Init();
        }
        #endregion

        public void Btn_Cancel()
        {
            if (state == InvenState.control)
            {
                Menu.SetActive(false);
                selectMenu.GetComponent<Image>().color = Color.yellow;
                Main();
            }
            else if(state == InvenState.unequip)
            {
                equipMenu.SetActive(false);
                selectEquipMenu.GetComponent<Image>().color = Color.yellow;
                Main();
            }
        }

        #region EquipMenu
        public void EnterEquipMenu(Transform t)
        {
            selectEquipMenu.SetActive(true);
            selectEquipMenu.transform.position = t.position;
        }

        public void Btn_UnEquip()
        {// 아이템 장착 해제
            switch (pocketNum)
            {
                case 0:
                    if (GetItem(InvenData.Pocket1[0]))
                        InvenData.Pocket1[0] = null;
                    break;
                case 1:
                    if (GetItem(InvenData.Pocket1[1]))
                        InvenData.Pocket1[1] = null;
                    break;
                case 2:
                    if (GetItem(InvenData.Pocket2[0]))
                        InvenData.Pocket2[0] = null;
                    break;
                case 3:
                    if (GetItem(InvenData.Pocket2[1]))
                        InvenData.Pocket2[1] = null;
                    break;
                case 4:
                    if (GetItem(InvenData.Pocket3[0]))
                        InvenData.Pocket3[0] = null;
                    break;
                case 5:
                    if (GetItem(InvenData.Pocket3[1]))
                        InvenData.Pocket3[1] = null;
                    break;
            }
            Main();
            RefreshSlot();
            RefreshPocket();
        }

        public void Btn_Fill()
        {
            state = InvenState.fill;
            assistant.SetActive(true);
            assistant.transform.position = pockets[pocketNum].transform.position + new Vector3(100, 0, 0);
            assistant.GetComponent<Assistant>().Init();
            equipMenu.SetActive(false);
        }
        #endregion

        #region TestItem
        public Item testItem1;
        public void TestBtn1()
        {
            GetItem(testItem1);
            RefreshSlot();
        }
        public Item testItem2;
        public void TestBtn2()
        {
            GetItem(testItem2);
            RefreshSlot();
        }
        public Item testItem3;
        public void TestBtn3()
        {
            GetItem(testItem3);
            RefreshSlot();
        }
        #endregion

        void Update()
        {
            switch (state)
            {
                case InvenState.main:
                    if (selectSlot != null)
                    {// 화살표로 선택한 슬롯 변경
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            SelectItem(selectSlot.slotNumber);
                            EnterSlot(selectSlot.slotNumber);
                        }
                        if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            selectSlot = slots[Mathf.Clamp(selectSlot.slotNumber - 8, 0, slots.Length)];
                            EnterSlot(selectSlot.slotNumber);
                        }
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            selectSlot = slots[Mathf.Clamp(selectSlot.slotNumber + 8, 0, slots.Length)];
                            EnterSlot(selectSlot.slotNumber);
                        }
                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            selectSlot = slots[Mathf.Clamp(selectSlot.slotNumber - 1, 0, slots.Length)];
                            EnterSlot(selectSlot.slotNumber);
                        }
                        if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            selectSlot = slots[Mathf.Clamp(selectSlot.slotNumber + 1, 0, slots.Length)];
                            EnterSlot(selectSlot.slotNumber);
                        }
                    }
                    break;
                case InvenState.control:
                    if (Input.GetKeyDown(KeyCode.Escape))
                        Cancel();
                    break;
                case InvenState.use:
                    break;
                case InvenState.equip:
                    break;
                case InvenState.equip_count:
                    if (Input.GetKeyDown(KeyCode.Escape))
                        Cancel();
                    break;
                case InvenState.move:
                    break;
                case InvenState.moveItem:
                    if (tempItem != null)
                        movingItem.transform.position = Input.mousePosition;
                    break;
            }
        }
    }
}
