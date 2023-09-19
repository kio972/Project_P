using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YeongJun
{
    public enum InvenState
    {
        main, // �⺻ ����
        control, // �������� Ŭ������ ��
        use, // ����� ĳ���͸� ������ ��
        equip, // ������ ������ ������ ��
        equip_count, // ������ �������� ������ ������ ��
        move, // �̵��� ������ ������ ������ ��
        moveItem, // �������� �ű� ��ġ�� ������ ��
        unequip, // ������ ������ ������ ��
        fill, // ������ ä��⸦ ������ ��
        drop, // ������ �����⸦ ������ ��
    }

    public class Inventory : MonoBehaviour
    {
        public InvenState state = new InvenState();

        [SerializeField] private GameObject slotPrefab; // ���� ������
        [SerializeField] private Transform slotParent; // ���� �׸���
        [SerializeField] private GameObject textn; // �Ϲ� �ؽ�Ʈ
        [SerializeField] private GameObject texti; // �߿� �ؽ�Ʈ
        public Slot[] slots;
        private bool mode = false; // �κ��丮 ���
        [SerializeField] private GameObject Menu;
        [SerializeField] private GameObject assistant;
        [SerializeField] private GameObject equipMenu;
        [SerializeField] private GameObject cancel;
        [SerializeField] private GameObject selectCaracter; // ���� ȿ��
        [SerializeField] private GameObject selectPocket;
        [SerializeField] private GameObject selectItem;
        [SerializeField] private GameObject selectMenu;
        [SerializeField] private GameObject selectEquipMenu;
        [SerializeField] private GameObject movingItem; // �ű� ������ UI
        public Item pocketItem;
        public int assistCount = -1;
        public int pocketNum = 0;

        private RaycastHit hit;
        private int layerMask;

        [SerializeField] private Portrait[] character; // ĳ���� �ʻ�ȭ
        [SerializeField] private Pocket[] pockets; // ĳ���� �ָӴ�

        public Slot selectSlot = null; // �������� ����
        public Item tempItem = null; // �ű� ������
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
        {// �κ��丮�� ���� �Լ�
            gameObject.SetActive(!gameObject.activeSelf);
            if (gameObject.activeSelf)
                Main();
        }

        public void Main()
        {// �κ��丮�� ������ �� ����
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
        {// ��ü ���� ���ΰ�ħ
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
        {// ���� ����
            if (state == InvenState.main)
            {
                if (mode)
                    iSort(); // �߿� ������ ����
                else
                    nSort(); // �Ϲ� ������ ����
                RefreshSlot();
            }
        }

        private void nSort()
        {
            int i, j;
            Item key;
            // ������ ���� ������
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
            // ������ ����
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
        {// �������� �κ��丮 �迭�� �߰��ϴ� �ڵ�
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
            {// �߿� ������
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
            {// �Ϲ� ������
                for (int i = 0; i < InvenData.iItem.Length; i++)
                {
                    if (InvenData.nItem[i] == null)
                    {
                        if (i < n)
                            n = i;
                    }
                    else if (InvenData.nItem[i].uid == tempItem.uid && InvenData.nItem[i].count < InvenData.nItem[i].maxCount)
                    {// ���� �������̸� count����
                        if(InvenData.nItem[i].maxCount < InvenData.nItem[i].count + tempItem.count)
                        {// ȹ���� �������� ���� ���� ���� ���� ���� �ִ� �������� ���ٸ�
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
        {// �������� Ŭ�� ���� ��
            selectSlot = slots[number];
            if (mode)
            {// �߿� �������� ���
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
            {// �Ϲ� �������� ���
                if (InvenData.nItem[number] != null)
                {
                    Menu.SetActive(true);
                    if ((number + 1) % 8 == 0)
                        Menu.transform.position = slots[number].transform.position + new Vector3(-160, -120, 0);
                    else
                        Menu.transform.position = slots[number].transform.position + new Vector3(160, -120, 0);
                    // ����� ���� ���� ��Ȱ��ȭ
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
        {// ���Կ� ���콺�� �÷��� �� ����ǥ��
            selectSlot = slots[number];
            selectItem.SetActive(true);
            selectItem.GetComponent<Image>().color = Color.yellow;
            selectItem.transform.position = selectSlot.transform.position;
        }

        public void EquipMenu()
        {// ������ ������ �޴� ���
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
        {// �޴��� ���콺�� �÷��� �� ����ǥ��
            selectMenu.SetActive(true);
            selectMenu.GetComponent<Image>().color = Color.yellow;
            selectMenu.transform.position = t.position;
        }
        #endregion

        #region Btn_Use
        public void Btn_Use()
        {// ����ư Ŭ��
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
        {// ĳ���� �ʻ�ȭ�� ���콺�� �÷��� �� ����ǥ��
            selectCaracter.SetActive(true);
            selectCaracter.transform.position = t.position;
        }

        public void SelectPortrait(int number)
        {// ĳ���Ϳ� ������ ���
            switch (number)
            {
                case 0:
                    // 1�� ĳ���Ϳ� �������� ����ϴ� �Լ�
                    break;
                case 1:
                    // 2�� ĳ���Ϳ� �������� ����ϴ� �Լ�
                    break;
                case 2:
                    // 3�� ĳ���Ϳ� �������� ����ϴ� �Լ�
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
        {// ĳ���� �ָӴϿ� ���콺�� �÷��� �� ����ǥ��
            selectPocket.SetActive(true);
            selectPocket.GetComponent<Image>().color = Color.yellow;
            selectPocket.transform.position = t.position;
        }

        public void SelectPocket(int number)
        {// �ָӴϸ� �������� �� ���� �Է� â ����
            state = InvenState.equip_count;
            pocketNum = number;
            cancel.SetActive(false);
            assistant.SetActive(true);
            assistant.transform.position = pockets[number].transform.position + new Vector3(100, 0, 0);
            assistant.GetComponent<Assistant>().Init();
        }

        public bool EquipItem(int count)
        {// ������ ����
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
        {// ������ �̵�
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
        {// ������ ���� ����
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
                    {// ȭ��ǥ�� ������ ���� ����
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
