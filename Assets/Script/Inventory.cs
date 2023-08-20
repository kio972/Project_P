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
        [SerializeField] private GameObject slotPrefab; // ���� ������
        [SerializeField] private Transform slotParent; // ���� �׸���
        [SerializeField] private GameObject textn; // �Ϲ� �ؽ�Ʈ
        [SerializeField] private GameObject texti; // �߿� �ؽ�Ʈ
        public Slot[] slots;
        private bool mode = false; // �κ��丮 ���
        [SerializeField] private GameObject Menu;
        [SerializeField] private GameObject Assistant;
        [SerializeField] private GameObject selectCaracter; // ���� ȿ��
        [SerializeField] private GameObject selectPocket;
        [SerializeField] private GameObject selectItem;

        private RaycastHit hit;
        private int layerMask;

        [SerializeField]
        private Pocket[] pockets; // ĳ���� �ָӴ�

        public InvenState state = new InvenState();
        public Slot selectSlot = null; // ���õ� ����

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
            mode = !mode;
            textn.SetActive(!mode);
            texti.SetActive(mode);
            RefreshSlot();
        }

        public void Sort()
        {// ���� ����
            if (mode)
                iSort(); // �߿� ������ ����
            else
                nSort(); // �Ϲ� ������ ����
            RefreshSlot();
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

        public bool GetItem(Item _item)
        {// �������� �κ��丮 �迭�� �߰��ϴ� �ڵ�
            int n = InvenData.iItem.Length;
            if (_item.type == ItemType.important)
            {// �߿� ������
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
            {// �Ϲ� ������
                for (int i = 0; i < InvenData.iItem.Length; i++)
                {
                    if (InvenData.nItem[i] == null && i < n)
                        n = i;
                    else if (InvenData.nItem[i] == _item && InvenData.nItem[i].count < InvenData.nItem[i].maxCount)
                    {// ���� �������̸� count����
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
        {// �������� Ŭ�� ���� ��
            selectSlot = slots[number];
            if (mode)
            {// �߿� �������� ���
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
            {// �Ϲ� �������� ���
                //if (InvenData.nItem[number] != null)
                //{
                    Menu.SetActive(true);
                    if ((number + 1) % 8 == 0)
                        Menu.transform.position = new Vector3(slots[number].transform.position.x - 160, slots[number].transform.position.y - 120, 0);
                    else
                        Menu.transform.position = new Vector3(slots[number].transform.position.x + 160, slots[number].transform.position.y - 120, 0);
                    // ����� ���� ���� ��Ȱ��ȭ
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
                    {// ȭ��ǥ�� ������ ���� ����
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
