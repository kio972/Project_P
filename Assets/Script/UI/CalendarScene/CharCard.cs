using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using JinWon;

public class CharCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 pos = new Vector2(-350, 1040);

    [SerializeField]
    private PlayerType playerType;


    Transform root;

    private bool init;

    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI levelText;
    [SerializeField]
    private TextMeshProUGUI InfoText;

    public string charName;
    private string CharName
    {
        get { return charName; }
    }
    public int charLv;
    private int CharLv
    {
        get { return charLv; }
    }
    public string charInfo;
    private string CharInfo
    {
        get { return charInfo; }
    }
    public List<int> charStat = new List<int>(); // Str, Int, Vit, Dex, Luk, 
    private List<int> CharStat
    {
        get { return charStat; }
    }
    public float charHP;
    private float CharHP
    {
        get { return charHP; }
    }
    public float charMP;
    private float CharMP
    {
        get { return charMP; }
    }

    [SerializeField]
    public List<string> hiddenList = new List<string>();
    private List<string> HiddenList
    {
        get { return hiddenList; }
    }

    private void Start()
    {
        root = transform.root;
        PlayerInfo();
    }

    

    private void PlayerInfo()
    {
        switch(playerType)
        {
            case PlayerType.Warrior:
                {
                    WarriorInfo playerInfo = new WarriorInfo();
                    nameText.text = charName = playerInfo.Name;
                    levelText.text = "Lv. " + (playerInfo.Lv);
                    charLv = playerInfo.Lv;
                    InfoText.text = charInfo = playerInfo.Info;
                    charStat.Add(playerInfo.Str);
                    charStat.Add(playerInfo.Int);
                    charStat.Add(playerInfo.Vit);
                    charStat.Add(playerInfo.Dex);
                    charStat.Add(playerInfo.Luk);
                    charHP = playerInfo.HP;
                    charMP = playerInfo.MP;
                    hiddenList.Add("- 단단해지기!");
                    break;
                }
            case PlayerType.Priest:
                {
                    PriestInfo playerInfo = new PriestInfo();
                    nameText.text = charName = playerInfo.Name;
                    levelText.text = "Lv. " + (playerInfo.Lv);
                    charLv = playerInfo.Lv;
                    InfoText.text = charInfo = playerInfo.Info;
                    charStat.Add(playerInfo.Str);
                    charStat.Add(playerInfo.Int);
                    charStat.Add(playerInfo.Vit);
                    charStat.Add(playerInfo.Dex);
                    charStat.Add(playerInfo.Luk);
                    charHP = playerInfo.HP;
                    charMP = playerInfo.MP;
                    hiddenList.Add("- 기도드리기!");
                    break;
                }
            case PlayerType.Archer:
                {
                    ArcherInfo playerInfo = new ArcherInfo();
                    nameText.text = charName = playerInfo.Name;
                    levelText.text = "Lv. " + (playerInfo.Lv);
                    charLv = playerInfo.Lv;
                    InfoText.text = charInfo = playerInfo.Info;
                    charStat.Add(playerInfo.Str);
                    charStat.Add(playerInfo.Int);
                    charStat.Add(playerInfo.Vit);
                    charStat.Add(playerInfo.Dex);
                    charStat.Add(playerInfo.Luk);
                    charHP = playerInfo.HP;
                    charMP = playerInfo.MP;
                    hiddenList.Add("- 활쏘고 도망가기!");
                    break;
                }
        }
        
    }

    public void CharCardInit(bool value)
    {
        if (value)
            init = true;
        else
            init = false;

        //Debug.Log(transform.name + " 상태 : " + init);
    }

    public void OnBeginDrag(PointerEventData eventData) // 눌렀을때
    {
        if(init)
            root.transform.SendMessageUpwards("BeginDrag", transform, SendMessageOptions.DontRequireReceiver);
    }

    public void OnDrag(PointerEventData eventData) // 잡고 있을때
    {
        if (init)
        {
            root.transform.SendMessageUpwards("Drag", transform, SendMessageOptions.DontRequireReceiver);
            transform.position = eventData.position + pos;
        }
    }

    public void OnEndDrag(PointerEventData eventData) // 뗐을 때
    {
        if(init)
            root.transform.SendMessageUpwards("EndDrag", transform, SendMessageOptions.DontRequireReceiver);
    }

}
