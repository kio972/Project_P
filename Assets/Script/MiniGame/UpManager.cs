using JinWon;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpManager : JinWon.Singleton<UpManager>
{
    private bool flag;
    public bool FLAG
    {
        get => flag;
    }

    private GameObject left;
    private GameObject right;
    private Button backBtn;
    private int score;
    public int SCORE
    {
        get => score;
        set => score = value;
    }
    private int gold;
    public int GOLD
    {
        get => gold;
        set => gold = value;
    }
    private int gem;
    public int GEM
    {
        get => gem;
        set => gem = value;
    }
    private TextMeshProUGUI scoreText;
    public TextMeshProUGUI SCORETEXT
    {
        get => scoreText;
        set => scoreText = value;
    }
    private TextMeshProUGUI scoreboard;
    public TextMeshProUGUI SCOREBOARD
    {
        get=> scoreboard;
        set => scoreboard = value;
    }
    private TextMeshProUGUI goldText;
    public TextMeshProUGUI GOLDTEXT
    {
        get => goldText;
        set => goldText = value;
    }
    private TextMeshProUGUI gemText;
    public TextMeshProUGUI GEMTEXT
    {
        get => gemText;
        set => gemText = value;
    }

    private void Awake()
    {
        score = 0;
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreboard = GameObject.Find("ScoreBoard").GetComponent<TextMeshProUGUI>();
        goldText = GameObject.Find("GoldText").GetComponent<TextMeshProUGUI>();
        gemText = GameObject.Find("GemText").GetComponent<TextMeshProUGUI>();

        backBtn = GameObject.Find("BackBtn").GetComponent<Button>();
        backBtn.onClick.AddListener(OnClick_BackBtn);
        left = GameObject.Find("Left");
        right = GameObject.Find("Right");
        GenerateRandomBool();
    }

    private void Update()
    {
        
    }

    public void GenerateRandomBool()
    {
        flag = Random.Range(0, 2) == 0;
        if(flag ==  false)
        {
            left.tag = "Up";
            right.tag = "Down";
        }
        else
        {
            left.tag = "Down";
            right.tag = "Up";
        }
        Debug.Log(flag);
    }

    private void OnClick_BackBtn()
    {
        GameManager.Inst.AsyncLoadNextScene(SceneName.Forge);
    }


}
