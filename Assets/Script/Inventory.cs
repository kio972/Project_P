using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JinWon;

namespace YeongJun
{
    public enum InvenState
    {
        main, // 기본 상태
        control, // 아이템을 클릭했을 때
        equip, // 장착할 슬롯을 선택할 때
        equip_count, // 장착할 아이템의 수량을 지정할 때
        move1, // 이동할 아이템 수량를 지정할 때
        move2, // 이동할 위치를 지정할 때
        drop1, // 버릴 아이템을 수량을 지정할 때
        drop2, // 아이템 드롭 확인창이 떴을 때
        unequip, // 아이템 장착을 해제할 때
    }

    public class Inventory : MonoBehaviour
    {
        public InvenState state = new InvenState();

        [SerializeField] private GameObject slotPrefab; // 슬롯 프리팹
        [SerializeField] private Transform slotParent; // 슬롯 그리드
        public Slot[] slots;
        [SerializeField] private GameObject Menu;
        [SerializeField] private GameObject assistant;
        [SerializeField] private GameObject equipMenu;
        [SerializeField] private GameObject cancel;
        [SerializeField] private GameObject dropMessage;
        [SerializeField] private GameObject selectPocket; // 강조 효과
        [SerializeField] private GameObject selectItem;
        [SerializeField] private GameObject selectMenu;
        [SerializeField] private GameObject selectEquipMenu;
        public Item equipItem;
        public Item pocketItem;
        public Item selectedItem = null;
        public Image moveItem;
        public int assistCount = -1;
        public int equipNum = 0;
        public int pocketNum = 0;

        private RaycastHit hit;
        private int layerMask;

        [SerializeField] private EquipSlot[] equipSlots = new EquipSlot[6];
        [SerializeField] private Pocket[] pockets = new Pocket[2];
        [SerializeField] private Text goldText;
        [SerializeField] private Text manaText;
        [SerializeField] private ItemInfo ItemInfo;

        public Slot selectSlot = null; // 선택된 슬롯

        public void Init()
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
            selectPocket.SetActive(false);
            selectItem.SetActive(false);
            selectMenu.SetActive(false);
            dropMessage.SetActive(false);
            selectItem.GetComponent<Image>().color = Color.yellow;
        }

        public void RefreshSlot()
        {// 전체 슬롯 새로고침
            for (int i = 0; i < 24; i++)
                slots[i].Refresh();
            goldText.text = GameManager.Inst.PlayerInfo.gold.ToString();
            manaText.text = GameManager.Inst.PlayerInfo.ston.ToString();
        }

        public void RefreshEquipSlots()
        {
            for (int i = 0; i < equipSlots.Length; i++)
                equipSlots[i].Refresh();
        }

        public void RefreshPocket()
        {
            for (int i = 0; i < pockets.Length; i++)
                pockets[i].Refresh();
        }

        public void Sort()
        {// 슬롯 정렬
            if (state == InvenState.main)
            {
                SortItem(); // 일반 아이템 정렬
                RefreshSlot();
            }
        }

        private void SortItem()
        {
            int i, j;
            Item key;
            for (i = 0; i < InvenData.item.Length - 1; i++)
            {
                for (j = i + 1; j < InvenData.item.Length; j++)
                {
                    if (InvenData.item[i] == null)
                    {
                        if (InvenData.item[j] == null)
                            continue;
                        else
                        {
                            InvenData.item[i] = InvenData.item[j];
                            InvenData.item[j] = null;
                        }
                    }
                    else
                        break;
                }
            }
            // 아이템 정렬
            for (i = 1; i < InvenData.item.Length; i++)
            {
                key = InvenData.item[i];
                if (key != null)
                {
                    for (j = i - 1; j >= 0 && InvenData.item[j].uid > key.uid; j--)
                        InvenData.item[j + 1] = InvenData.item[j];
                    InvenData.item[j + 1] = key;
                }
                else
                    break;
            }
        }

        public bool GetItem(Item _item)
        {// 아이템을 인벤토리 배열에 추가하는 코드
            int n = InvenData.item.Length;
            for (int i = 0; i < InvenData.item.Length; i++)
            {
                if (InvenData.item[i] == null)
                {
                    if (i < n)
                        n = i;
                }
                else if (InvenData.item[i].uid == _item.uid && InvenData.item[i].count < InvenData.item[i].maxCount)
                {// 같은 아이템이면 count증가
                    if (InvenData.item[i].maxCount < InvenData.item[i].count + _item.count)
                    {
                        InvenData.item[i].count = InvenData.item[i].maxCount;
                        _item.count -= (InvenData.item[i].count + _item.count) - InvenData.item[i].maxCount;
                        GetItem(_item);
                    }
                    InvenData.item[i].count += _item.count;
                    return true;
                }
            }
            if (InvenData.item[n] == null)
            {
                InvenData.item[n] = new Item();
                InvenData.item[n].uid = _item.uid;
                InvenData.item[n].name = _item.name;
                InvenData.item[n].type = _item.type;
                InvenData.item[n].grade = _item.grade;
                InvenData.item[n].detailsGrade = _item.detailsGrade;
                InvenData.item[n].option = _item.option;
                InvenData.item[n].set = _item.set;
                InvenData.item[n].explain = _item.explain;
                InvenData.item[n].cooldown = _item.cooldown;
                InvenData.item[n].iconImg = _item.iconImg;
                InvenData.item[n].count = _item.count;
                InvenData.item[n].maxCount = _item.maxCount;
                return true;
            }
            return false;
        }

        #region main
        public void SelectItem(int number)
        {// 아이템을 클릭 했을 때
            selectSlot = slots[number];
            if (InvenData.item[number] != null)
            {
                Menu.SetActive(true);
                Menu.transform.position = slots[number].transform.position + new Vector3(50, 40, 0);
                if (InvenData.item[number].type == ItemType.ingredient)
                {// 재료 아이템이면 사용과 장착 비활성화
                    Menu.transform.GetChild(0).gameObject.SetActive(false);
                    Menu.transform.GetChild(1).gameObject.SetActive(false);
                }
                else if(InvenData.item[number].type == ItemType.potion)
                {// 소비 아이템이면 사용과 장착 활성화
                    Menu.transform.GetChild(0).gameObject.SetActive(true);
                    Menu.transform.GetChild(1).gameObject.SetActive(true);
                }
                else
                {// 장비 아이템이면 장착만 활성화
                    Menu.transform.GetChild(0).gameObject.SetActive(false);
                    Menu.transform.GetChild(1).gameObject.SetActive(true);
                }
                EnterMenu(Menu.transform.GetChild(0));
                CloseItemInfo();
                selectItem.SetActive(true);
                selectItem.GetComponent<Image>().color = Color.gray;
                selectItem.transform.position = selectSlot.transform.position;
                state = InvenState.control;
            }
        }

        public void EnterSlot(int number)
        {// 슬롯에 마우스를 올렸을 때 강조표시
            selectSlot = slots[number];
            selectItem.SetActive(true);
            selectItem.GetComponent<Image>().color = Color.yellow;
            selectItem.transform.position = selectSlot.transform.position;
        }

        public void EquipPotionMenu()
        {// 장착한 아이템 메뉴 출력
            state = InvenState.unequip;
            if (pocketItem != null)
            {
                bool sameItem = false;
                switch (pocketNum)
                {
                    case 0:
                        pocketItem = InvenData.pocket[0];
                        break;
                    case 1:
                        pocketItem = InvenData.pocket[1];
                        break;
                }
                selectPocket.GetComponent<Image>().color = Color.gray;
                if (pocketItem.count == pocketItem.maxCount)
                {
                    equipMenu.transform.GetChild(2).GetComponent<Button>().interactable = false;
                    equipMenu.transform.GetChild(2).GetChild(0).GetComponent<Text>().color = Color.red;
                }
                else
                {
                    for (int i = 0; i < InvenData.item.Length; i++)
                    {
                        if (InvenData.item[i] != null && pocketItem.uid == InvenData.item[i].uid)
                        {
                            sameItem = true;
                            selectSlot = slots[i];
                            break;
                        }
                    }
                    if (sameItem)
                    {
                        equipMenu.transform.GetChild(2).GetComponent<Button>().interactable = true;
                        equipMenu.transform.GetChild(2).GetChild(0).GetComponent<Text>().color = Color.white;
                    }
                    else
                    {
                        equipMenu.transform.GetChild(2).GetComponent<Button>().interactable = false;
                        equipMenu.transform.GetChild(2).GetChild(0).GetComponent<Text>().color = Color.red;
                    }
                }
                equipMenu.SetActive(true);
                equipMenu.transform.GetChild(0).gameObject.SetActive(false);
                equipMenu.transform.GetChild(1).gameObject.SetActive(true);
                equipMenu.transform.GetChild(2).gameObject.SetActive(true);
                equipMenu.transform.position = selectPocket.transform.position + new Vector3(160, -90, 0);
            }
        }

        public void EquipmentMenu()
        {// 장착한 아이템 메뉴 출력
            state = InvenState.unequip;
            if (equipItem != null)
            {
                switch (equipNum)
                {
                    case 0:
                        equipItem = InvenData.equip[0];
                        break;
                    case 1:
                        equipItem = InvenData.equip[1];
                        break;
                    case 2:
                        equipItem = InvenData.equip[2];
                        break;
                    case 3:
                        equipItem = InvenData.equip[3];
                        break;
                    case 4:
                        equipItem = InvenData.equip[4];
                        break;
                    case 5:
                        equipItem = InvenData.equip[5];
                        break;
                }
                selectPocket.GetComponent<Image>().color = Color.gray;
                equipMenu.SetActive(true);
                equipMenu.transform.GetChild(0).gameObject.SetActive(true);
                equipMenu.transform.GetChild(1).gameObject.SetActive(false);
                equipMenu.transform.GetChild(2).gameObject.SetActive(false);
                equipMenu.transform.position = selectPocket.transform.position + new Vector3(160, -90, 0);
            }
        }
        #endregion

        #region Menu
        public void EnterMenu(Transform t)
        {// 메뉴에 마우스를 올렸을 때 강조표시
            selectMenu.SetActive(true);
            selectMenu.transform.position = t.position;
            selectMenu.GetComponent<Image>().color = Color.yellow;
        }
        #endregion

        #region Btn_Use
        public void Btn_Use()
        {// 사용버튼 클릭
            if (state == InvenState.control)
            {
                InvenData.item[selectSlot.slotNumber].count--;
                RefreshSlot();
                // 사용 효과 추가
                state = InvenState.main;
                Menu.SetActive(false);
                selectMenu.SetActive(false);
            }
        }
        #endregion

        #region Btn_Equip
        public void Btn_Equip()
        {
            Item tempItem = null;
            switch (InvenData.item[selectSlot.slotNumber].type)
            {
                case ItemType.potion:
                    state = InvenState.equip;
                    Menu.SetActive(false);
                    selectPocket.SetActive(true);
                    EnterPocket(pockets[0].transform);
                    selectMenu.SetActive(false);
                    cancel.SetActive(true);
                    cancel.transform.position = selectSlot.transform.position + new Vector3(0, -90, 0);
                    break;
                case ItemType.equipWeapon:
                    if(InvenData.equip[0] != null)
                    {
                        tempItem = InvenData.equip[0];
                        InvenData.equip[0] = new Item();
                        InvenData.equip[0].uid = InvenData.item[selectSlot.slotNumber].uid;
                        InvenData.equip[0].name = InvenData.item[selectSlot.slotNumber].name;
                        InvenData.equip[0].type = InvenData.item[selectSlot.slotNumber].type;
                        InvenData.equip[0].grade = InvenData.item[selectSlot.slotNumber].grade;
                        InvenData.equip[0].detailsGrade = InvenData.item[selectSlot.slotNumber].detailsGrade;
                        InvenData.equip[0].option = InvenData.item[selectSlot.slotNumber].option;
                        InvenData.equip[0].set = InvenData.item[selectSlot.slotNumber].set;
                        InvenData.equip[0].explain = InvenData.item[selectSlot.slotNumber].explain;
                        InvenData.equip[0].cooldown = InvenData.item[selectSlot.slotNumber].cooldown;
                        InvenData.equip[0].iconImg = InvenData.item[selectSlot.slotNumber].iconImg;
                        InvenData.equip[0].count = InvenData.item[selectSlot.slotNumber].count;
                        InvenData.equip[0].maxCount = InvenData.item[selectSlot.slotNumber].maxCount;
                        GetItem(tempItem);
                    }
                    else
                    {
                        InvenData.equip[0] = new Item();
                        InvenData.equip[0].uid = InvenData.item[selectSlot.slotNumber].uid;
                        InvenData.equip[0].name = InvenData.item[selectSlot.slotNumber].name;
                        InvenData.equip[0].type = InvenData.item[selectSlot.slotNumber].type;
                        InvenData.equip[0].grade = InvenData.item[selectSlot.slotNumber].grade;
                        InvenData.equip[0].detailsGrade = InvenData.item[selectSlot.slotNumber].detailsGrade;
                        InvenData.equip[0].option = InvenData.item[selectSlot.slotNumber].option;
                        InvenData.equip[0].set = InvenData.item[selectSlot.slotNumber].set;
                        InvenData.equip[0].explain = InvenData.item[selectSlot.slotNumber].explain;
                        InvenData.equip[0].cooldown = InvenData.item[selectSlot.slotNumber].cooldown;
                        InvenData.equip[0].iconImg = InvenData.item[selectSlot.slotNumber].iconImg;
                        InvenData.equip[0].count = InvenData.item[selectSlot.slotNumber].count;
                        InvenData.equip[0].maxCount = InvenData.item[selectSlot.slotNumber].maxCount;
                        InvenData.item[selectSlot.slotNumber] = null;
                    }
                    equipSlots[0].Refresh();
                    RefreshSlot();
                    Main();
                    break;
                case ItemType.equipHelmet:
                    if (InvenData.equip[1] != null)
                    {
                        tempItem = InvenData.equip[1];
                        InvenData.equip[1] = new Item();
                        InvenData.equip[1].uid = InvenData.item[selectSlot.slotNumber].uid;
                        InvenData.equip[1].name = InvenData.item[selectSlot.slotNumber].name;
                        InvenData.equip[1].type = InvenData.item[selectSlot.slotNumber].type;
                        InvenData.equip[1].grade = InvenData.item[selectSlot.slotNumber].grade;
                        InvenData.equip[1].detailsGrade = InvenData.item[selectSlot.slotNumber].detailsGrade;
                        InvenData.equip[1].option = InvenData.item[selectSlot.slotNumber].option;
                        InvenData.equip[1].set = InvenData.item[selectSlot.slotNumber].set;
                        InvenData.equip[1].explain = InvenData.item[selectSlot.slotNumber].explain;
                        InvenData.equip[1].cooldown = InvenData.item[selectSlot.slotNumber].cooldown;
                        InvenData.equip[1].iconImg = InvenData.item[selectSlot.slotNumber].iconImg;
                        InvenData.equip[1].count = InvenData.item[selectSlot.slotNumber].count;
                        InvenData.equip[1].maxCount = InvenData.item[selectSlot.slotNumber].maxCount;
                        GetItem(tempItem);
                    }
                    else
                    {
                        InvenData.equip[1] = new Item();
                        InvenData.equip[1].uid = InvenData.item[selectSlot.slotNumber].uid;
                        InvenData.equip[1].name = InvenData.item[selectSlot.slotNumber].name;
                        InvenData.equip[1].type = InvenData.item[selectSlot.slotNumber].type;
                        InvenData.equip[1].grade = InvenData.item[selectSlot.slotNumber].grade;
                        InvenData.equip[1].detailsGrade = InvenData.item[selectSlot.slotNumber].detailsGrade;
                        InvenData.equip[1].option = InvenData.item[selectSlot.slotNumber].option;
                        InvenData.equip[1].set = InvenData.item[selectSlot.slotNumber].set;
                        InvenData.equip[1].explain = InvenData.item[selectSlot.slotNumber].explain;
                        InvenData.equip[1].cooldown = InvenData.item[selectSlot.slotNumber].cooldown;
                        InvenData.equip[1].iconImg = InvenData.item[selectSlot.slotNumber].iconImg;
                        InvenData.equip[1].count = InvenData.item[selectSlot.slotNumber].count;
                        InvenData.equip[1].maxCount = InvenData.item[selectSlot.slotNumber].maxCount;
                        InvenData.item[selectSlot.slotNumber] = null;
                    }
                    equipSlots[1].Refresh();
                    RefreshSlot();
                    Main();
                    break;
                case ItemType.equipNecklace:
                    if (InvenData.equip[2] != null)
                    {
                        tempItem = InvenData.equip[2];
                        InvenData.equip[2] = new Item();
                        InvenData.equip[2].uid = InvenData.item[selectSlot.slotNumber].uid;
                        InvenData.equip[2].name = InvenData.item[selectSlot.slotNumber].name;
                        InvenData.equip[2].type = InvenData.item[selectSlot.slotNumber].type;
                        InvenData.equip[2].grade = InvenData.item[selectSlot.slotNumber].grade;
                        InvenData.equip[2].detailsGrade = InvenData.item[selectSlot.slotNumber].detailsGrade;
                        InvenData.equip[2].option = InvenData.item[selectSlot.slotNumber].option;
                        InvenData.equip[2].set = InvenData.item[selectSlot.slotNumber].set;
                        InvenData.equip[2].explain = InvenData.item[selectSlot.slotNumber].explain;
                        InvenData.equip[2].cooldown = InvenData.item[selectSlot.slotNumber].cooldown;
                        InvenData.equip[2].iconImg = InvenData.item[selectSlot.slotNumber].iconImg;
                        InvenData.equip[2].count = InvenData.item[selectSlot.slotNumber].count;
                        InvenData.equip[2].maxCount = InvenData.item[selectSlot.slotNumber].maxCount;
                        GetItem(tempItem);
                    }
                    else
                    {
                        InvenData.equip[2] = new Item();
                        InvenData.equip[2].uid = InvenData.item[selectSlot.slotNumber].uid;
                        InvenData.equip[2].name = InvenData.item[selectSlot.slotNumber].name;
                        InvenData.equip[2].type = InvenData.item[selectSlot.slotNumber].type;
                        InvenData.equip[2].grade = InvenData.item[selectSlot.slotNumber].grade;
                        InvenData.equip[2].detailsGrade = InvenData.item[selectSlot.slotNumber].detailsGrade;
                        InvenData.equip[2].option = InvenData.item[selectSlot.slotNumber].option;
                        InvenData.equip[2].set = InvenData.item[selectSlot.slotNumber].set;
                        InvenData.equip[2].explain = InvenData.item[selectSlot.slotNumber].explain;
                        InvenData.equip[2].cooldown = InvenData.item[selectSlot.slotNumber].cooldown;
                        InvenData.equip[2].iconImg = InvenData.item[selectSlot.slotNumber].iconImg;
                        InvenData.equip[2].count = InvenData.item[selectSlot.slotNumber].count;
                        InvenData.equip[2].maxCount = InvenData.item[selectSlot.slotNumber].maxCount;
                        InvenData.item[selectSlot.slotNumber] = null;
                    }
                    equipSlots[2].Refresh();
                    RefreshSlot();
                    Main();
                    break;
                case ItemType.equipArmor:
                    if (InvenData.equip[3] != null)
                    {
                        tempItem = InvenData.equip[3];
                        InvenData.equip[3] = new Item();
                        InvenData.equip[3].uid = InvenData.item[selectSlot.slotNumber].uid;
                        InvenData.equip[3].name = InvenData.item[selectSlot.slotNumber].name;
                        InvenData.equip[3].type = InvenData.item[selectSlot.slotNumber].type;
                        InvenData.equip[3].grade = InvenData.item[selectSlot.slotNumber].grade;
                        InvenData.equip[3].detailsGrade = InvenData.item[selectSlot.slotNumber].detailsGrade;
                        InvenData.equip[3].option = InvenData.item[selectSlot.slotNumber].option;
                        InvenData.equip[3].set = InvenData.item[selectSlot.slotNumber].set;
                        InvenData.equip[3].explain = InvenData.item[selectSlot.slotNumber].explain;
                        InvenData.equip[3].cooldown = InvenData.item[selectSlot.slotNumber].cooldown;
                        InvenData.equip[3].iconImg = InvenData.item[selectSlot.slotNumber].iconImg;
                        InvenData.equip[3].count = InvenData.item[selectSlot.slotNumber].count;
                        InvenData.equip[3].maxCount = InvenData.item[selectSlot.slotNumber].maxCount;
                        GetItem(tempItem);
                    }
                    else
                    {
                        InvenData.equip[3] = new Item();
                        InvenData.equip[3].uid = InvenData.item[selectSlot.slotNumber].uid;
                        InvenData.equip[3].name = InvenData.item[selectSlot.slotNumber].name;
                        InvenData.equip[3].type = InvenData.item[selectSlot.slotNumber].type;
                        InvenData.equip[3].grade = InvenData.item[selectSlot.slotNumber].grade;
                        InvenData.equip[3].detailsGrade = InvenData.item[selectSlot.slotNumber].detailsGrade;
                        InvenData.equip[3].option = InvenData.item[selectSlot.slotNumber].option;
                        InvenData.equip[3].set = InvenData.item[selectSlot.slotNumber].set;
                        InvenData.equip[3].explain = InvenData.item[selectSlot.slotNumber].explain;
                        InvenData.equip[3].cooldown = InvenData.item[selectSlot.slotNumber].cooldown;
                        InvenData.equip[3].iconImg = InvenData.item[selectSlot.slotNumber].iconImg;
                        InvenData.equip[3].count = InvenData.item[selectSlot.slotNumber].count;
                        InvenData.equip[3].maxCount = InvenData.item[selectSlot.slotNumber].maxCount;
                        InvenData.item[selectSlot.slotNumber] = null;
                    }
                    equipSlots[3].Refresh();
                    RefreshSlot();
                    Main();
                    break;
                case ItemType.equipRing:
                    if (InvenData.equip[4] != null)
                    {
                        tempItem = InvenData.equip[0];
                        InvenData.equip[4] = new Item();
                        InvenData.equip[4].uid = InvenData.item[selectSlot.slotNumber].uid;
                        InvenData.equip[4].name = InvenData.item[selectSlot.slotNumber].name;
                        InvenData.equip[4].type = InvenData.item[selectSlot.slotNumber].type;
                        InvenData.equip[4].grade = InvenData.item[selectSlot.slotNumber].grade;
                        InvenData.equip[4].detailsGrade = InvenData.item[selectSlot.slotNumber].detailsGrade;
                        InvenData.equip[4].option = InvenData.item[selectSlot.slotNumber].option;
                        InvenData.equip[4].set = InvenData.item[selectSlot.slotNumber].set;
                        InvenData.equip[4].explain = InvenData.item[selectSlot.slotNumber].explain;
                        InvenData.equip[4].cooldown = InvenData.item[selectSlot.slotNumber].cooldown;
                        InvenData.equip[4].iconImg = InvenData.item[selectSlot.slotNumber].iconImg;
                        InvenData.equip[4].count = InvenData.item[selectSlot.slotNumber].count;
                        InvenData.equip[4].maxCount = InvenData.item[selectSlot.slotNumber].maxCount;
                        GetItem(tempItem);
                    }
                    else
                    {
                        InvenData.equip[4] = new Item();
                        InvenData.equip[4].uid = InvenData.item[selectSlot.slotNumber].uid;
                        InvenData.equip[4].name = InvenData.item[selectSlot.slotNumber].name;
                        InvenData.equip[4].type = InvenData.item[selectSlot.slotNumber].type;
                        InvenData.equip[4].grade = InvenData.item[selectSlot.slotNumber].grade;
                        InvenData.equip[4].detailsGrade = InvenData.item[selectSlot.slotNumber].detailsGrade;
                        InvenData.equip[4].option = InvenData.item[selectSlot.slotNumber].option;
                        InvenData.equip[4].set = InvenData.item[selectSlot.slotNumber].set;
                        InvenData.equip[4].explain = InvenData.item[selectSlot.slotNumber].explain;
                        InvenData.equip[4].cooldown = InvenData.item[selectSlot.slotNumber].cooldown;
                        InvenData.equip[4].iconImg = InvenData.item[selectSlot.slotNumber].iconImg;
                        InvenData.equip[4].count = InvenData.item[selectSlot.slotNumber].count;
                        InvenData.equip[4].maxCount = InvenData.item[selectSlot.slotNumber].maxCount;
                        InvenData.item[selectSlot.slotNumber] = null;
                    }
                    equipSlots[4].Refresh();
                    RefreshSlot();
                    Main();
                    break;
                case ItemType.equipShoes:
                    if (InvenData.equip[5] != null)
                    {
                        tempItem = InvenData.equip[0];
                        InvenData.equip[5] = new Item();
                        InvenData.equip[5].uid = InvenData.item[selectSlot.slotNumber].uid;
                        InvenData.equip[5].name = InvenData.item[selectSlot.slotNumber].name;
                        InvenData.equip[5].type = InvenData.item[selectSlot.slotNumber].type;
                        InvenData.equip[5].grade = InvenData.item[selectSlot.slotNumber].grade;
                        InvenData.equip[5].detailsGrade = InvenData.item[selectSlot.slotNumber].detailsGrade;
                        InvenData.equip[5].option = InvenData.item[selectSlot.slotNumber].option;
                        InvenData.equip[5].set = InvenData.item[selectSlot.slotNumber].set;
                        InvenData.equip[5].explain = InvenData.item[selectSlot.slotNumber].explain;
                        InvenData.equip[5].cooldown = InvenData.item[selectSlot.slotNumber].cooldown;
                        InvenData.equip[5].iconImg = InvenData.item[selectSlot.slotNumber].iconImg;
                        InvenData.equip[5].count = InvenData.item[selectSlot.slotNumber].count;
                        InvenData.equip[5].maxCount = InvenData.item[selectSlot.slotNumber].maxCount;
                        GetItem(tempItem);
                    }
                    else
                    {
                        InvenData.equip[5] = new Item();
                        InvenData.equip[5].uid = InvenData.item[selectSlot.slotNumber].uid;
                        InvenData.equip[5].name = InvenData.item[selectSlot.slotNumber].name;
                        InvenData.equip[5].type = InvenData.item[selectSlot.slotNumber].type;
                        InvenData.equip[5].grade = InvenData.item[selectSlot.slotNumber].grade;
                        InvenData.equip[5].detailsGrade = InvenData.item[selectSlot.slotNumber].detailsGrade;
                        InvenData.equip[5].option = InvenData.item[selectSlot.slotNumber].option;
                        InvenData.equip[5].set = InvenData.item[selectSlot.slotNumber].set;
                        InvenData.equip[5].explain = InvenData.item[selectSlot.slotNumber].explain;
                        InvenData.equip[5].cooldown = InvenData.item[selectSlot.slotNumber].cooldown;
                        InvenData.equip[5].iconImg = InvenData.item[selectSlot.slotNumber].iconImg;
                        InvenData.equip[5].count = InvenData.item[selectSlot.slotNumber].count;
                        InvenData.equip[5].maxCount = InvenData.item[selectSlot.slotNumber].maxCount;
                        InvenData.item[selectSlot.slotNumber] = null;
                    }
                    equipSlots[5].Refresh();
                    RefreshSlot();
                    Main();
                    break;
            }
        }

        public void Cancel()
        {
            state = InvenState.main;
            selectPocket.SetActive(false);
            SelectItem(selectSlot.slotNumber);
            cancel.SetActive(false);
            dropMessage.SetActive(false);
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
                    if (InvenData.pocket[0] != null)
                    {
                        if (assistCount != -1 && InvenData.pocket[0].uid == InvenData.item[assistCount].uid)
                        {
                            InvenData.pocket[0].count += count;
                            InvenData.item[selectSlot.slotNumber].count -= count;
                            pockets[pocketNum].Refresh();
                            state = InvenState.main;
                            return true;
                        }
                    }
                    else
                    {
                        InvenData.pocket[0] = new Item();
                        InvenData.pocket[0].uid = InvenData.item[selectSlot.slotNumber].uid;
                        InvenData.pocket[0].name = InvenData.item[selectSlot.slotNumber].name;
                        InvenData.pocket[0].type = InvenData.item[selectSlot.slotNumber].type;
                        InvenData.pocket[0].grade = InvenData.item[selectSlot.slotNumber].grade;
                        InvenData.pocket[0].detailsGrade = InvenData.item[selectSlot.slotNumber].detailsGrade;
                        InvenData.pocket[0].option = InvenData.item[selectSlot.slotNumber].option;
                        InvenData.pocket[0].set = InvenData.item[selectSlot.slotNumber].set;
                        InvenData.pocket[0].explain = InvenData.item[selectSlot.slotNumber].explain;
                        InvenData.pocket[0].cooldown = InvenData.item[selectSlot.slotNumber].cooldown;
                        InvenData.pocket[0].iconImg = InvenData.item[selectSlot.slotNumber].iconImg;
                        InvenData.pocket[0].count = count;
                        InvenData.pocket[0].maxCount = InvenData.item[selectSlot.slotNumber].maxCount;
                        InvenData.item[selectSlot.slotNumber].count -= count;
                        pockets[pocketNum].Refresh();
                        state = InvenState.main;
                        return true;
                    }
                    break;
                case 1:
                    if (InvenData.pocket[1] != null)
                    {
                        if (assistCount != -1 && InvenData.pocket[1].uid == InvenData.item[assistCount].uid)
                        {
                            InvenData.pocket[1].count += count;
                            InvenData.item[selectSlot.slotNumber].count -= count;
                            pockets[pocketNum].Refresh();
                            state = InvenState.main;
                            return true;
                        }
                    }
                    else
                    {
                        InvenData.pocket[1] = new Item();
                        InvenData.pocket[1].uid = InvenData.item[selectSlot.slotNumber].uid;
                        InvenData.pocket[1].name = InvenData.item[selectSlot.slotNumber].name;
                        InvenData.pocket[1].type = InvenData.item[selectSlot.slotNumber].type;
                        InvenData.pocket[1].grade = InvenData.item[selectSlot.slotNumber].grade;
                        InvenData.pocket[1].detailsGrade = InvenData.item[selectSlot.slotNumber].detailsGrade;
                        InvenData.pocket[1].option = InvenData.item[selectSlot.slotNumber].option;
                        InvenData.pocket[1].set = InvenData.item[selectSlot.slotNumber].set;
                        InvenData.pocket[1].explain = InvenData.item[selectSlot.slotNumber].explain;
                        InvenData.pocket[1].cooldown = InvenData.item[selectSlot.slotNumber].cooldown;
                        InvenData.pocket[1].iconImg = InvenData.item[selectSlot.slotNumber].iconImg;
                        InvenData.pocket[1].count = count;
                        InvenData.pocket[1].maxCount = InvenData.item[selectSlot.slotNumber].maxCount;
                        InvenData.item[selectSlot.slotNumber].count -= count;
                        pockets[pocketNum].Refresh();
                        state = InvenState.main;
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
        {
            state = InvenState.move1;
            Menu.SetActive(false);
            selectMenu.SetActive(false);
            assistant.SetActive(true);
            assistant.transform.position = slots[selectSlot.slotNumber].transform.position + new Vector3(100, 0, 0);
            assistant.GetComponent<Assistant>().Init();
        }

        public void Move2()
        {
            assistant.SetActive(false);
            moveItem.gameObject.SetActive(true);
            moveItem.sprite = selectedItem.iconImg;
            state = InvenState.move2;
        }
        #endregion

        #region Btn_Drop
        public void Btn_Drop()
        {
            state = InvenState.drop1;
            Menu.SetActive(false);
            selectMenu.SetActive(false);
            assistant.SetActive(true);
            assistant.transform.position = slots[selectSlot.slotNumber].transform.position + new Vector3(100, 0, 0);
            assistant.GetComponent<Assistant>().Init();
        }

        public void ShowDropMessage()
        {
            dropMessage.SetActive(true);
        }

        public void DropItem()
        {
            InvenData.item[selectSlot.slotNumber].count -= selectedItem.count;
            selectedItem = null;
            RefreshSlot();
            Main();
        }
        #endregion

        #region Btn_Cancel
        public void Btn_Cancel()
        {
            if (state == InvenState.control)
            {
                Menu.SetActive(false);
                selectMenu.GetComponent<Image>().color = Color.yellow;
                selectMenu.SetActive(false);
                state = InvenState.main;
            }
            else if(state == InvenState.unequip)
            {
                equipMenu.SetActive(false);
                selectEquipMenu.GetComponent<Image>().color = Color.yellow;
                selectEquipMenu.SetActive(false);
                state = InvenState.main;
            }
        }
        #endregion

        #region EquipMenu
        public void EnterEquipMenu(Transform t)
        {
            selectEquipMenu.SetActive(true);
            selectEquipMenu.transform.position = t.position;
        }

        public void Btn_UnEquipEquipSlot()
        {// 아이템 장착 해제
            switch (equipNum)
            {
                case 0:
                    if (GetItem(InvenData.equip[0]))
                    {
                        InvenData.equip[0] = null;
                        state = InvenState.main;
                        equipMenu.SetActive(false);
                    }
                    break;
                case 1:
                    if (GetItem(InvenData.equip[1]))
                    {
                        InvenData.equip[1] = null;
                        state = InvenState.main;
                        equipMenu.SetActive(false);
                    }
                    break;
                case 2:
                    if (GetItem(InvenData.equip[2]))
                    {
                        InvenData.equip[2] = null;
                        state = InvenState.main;
                        equipMenu.SetActive(false);
                    }
                    break;
                case 3:
                    if (GetItem(InvenData.equip[3]))
                    {
                        InvenData.equip[3] = null;
                        state = InvenState.main;
                        equipMenu.SetActive(false);
                    }
                    break;
                case 4:
                    if (GetItem(InvenData.equip[4]))
                    {
                        InvenData.equip[4] = null;
                        state = InvenState.main;
                        equipMenu.SetActive(false);
                    }
                    break;
                case 5:
                    if (GetItem(InvenData.equip[5]))
                    {
                        InvenData.equip[5] = null;
                        state = InvenState.main;
                        equipMenu.SetActive(false);
                    }
                    break;
            }
            RefreshSlot();
            RefreshEquipSlots();
        }


        public void Btn_UnEquipPocket()
        {// 아이템 장착 해제
            switch (pocketNum)
            {
                case 0:
                    if (GetItem(InvenData.pocket[0]))
                    {
                        InvenData.pocket[0] = null;
                        state = InvenState.main;
                        equipMenu.SetActive(false);
                    }
                    break;
                case 1:
                    if (GetItem(InvenData.pocket[1]))
                    {
                        InvenData.pocket[1] = null;
                        state = InvenState.main;
                        equipMenu.SetActive(false);
                    }
                    break;
            }
            RefreshSlot();
            RefreshPocket();
        }

        public void Btn_Fill()
        {
            assistant.SetActive(true);
            assistant.transform.position = pockets[pocketNum].transform.position + new Vector3(100, 0, 0);
            assistant.GetComponent<Assistant>().Init();
            equipMenu.SetActive(false);
        }
        #endregion

        public void OpenItemInfo(Item item)
        {
            ItemInfo.gameObject.SetActive(true);
            ItemInfo.ShowInfo(item);
            ItemInfo.transform.position = selectSlot.transform.position + new Vector3(60, 60, 0);
        }

        public void CloseItemInfo()
        {
            ItemInfo.gameObject.SetActive(false);
        }

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

        void Update()
        {
            switch (state)
            {
                case InvenState.main:
                    //if (selectSlot != null)
                    //{// 화살표로 선택한 슬롯 변경
                    //    if (Input.GetKeyDown(KeyCode.Space))
                    //    {
                    //        SelectItem(selectSlot.slotNumber);
                    //        EnterSlot(selectSlot.slotNumber);
                    //    }
                    //    if (Input.GetKeyDown(KeyCode.UpArrow))
                    //    {
                    //        selectSlot = slots[Mathf.Clamp(selectSlot.slotNumber - 6, 0, slots.Length - 1)];
                    //        EnterSlot(selectSlot.slotNumber);
                    //    }
                    //    if (Input.GetKeyDown(KeyCode.DownArrow))
                    //    {
                    //        selectSlot = slots[Mathf.Clamp(selectSlot.slotNumber + 6, 0, slots.Length - 1)];
                    //        EnterSlot(selectSlot.slotNumber);
                    //    }
                    //    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    //    {
                    //        selectSlot = slots[Mathf.Clamp(selectSlot.slotNumber - 1, 0, slots.Length - 1)];
                    //        EnterSlot(selectSlot.slotNumber);
                    //    }
                    //    if (Input.GetKeyDown(KeyCode.RightArrow))
                    //    {
                    //        selectSlot = slots[Mathf.Clamp(selectSlot.slotNumber + 1, 0, slots.Length - 1)];
                    //        EnterSlot(selectSlot.slotNumber);
                    //    }
                    //}
                    break;
                case InvenState.control:
                    if (Input.GetKeyDown(KeyCode.Escape))
                        Main();
                    break;
                case InvenState.equip:
                    if (Input.GetKeyDown(KeyCode.Escape))
                        Cancel();
                    break;
                case InvenState.move2:
                    moveItem.transform.position = Input.mousePosition;
                    break;
                case InvenState.drop2:
                    if (Input.GetKeyDown(KeyCode.Escape))
                        Main();
                    break;

            }
        }
    }
}
