using JinWon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RunManager : Singleton<RunManager>
{
    private GameObject back1;
    private GameObject back2;
    private GameObject player;
    private GameObject enemy;
    private GameObject obj;

    private int score;

    private int hp;
    public int HP
    {
        get => hp;
        set => hp = value;
    }
    private int gold;
    private int gem;


    [SerializeField]
    private TextMeshProUGUI scoreText;


    [SerializeField]
    private GameObject gameFrame;


    [SerializeField]
    private float scrollSpeed = 3f;
    [SerializeField]
    private Vector3 startPos;

    [SerializeField]
    private List<GameObject> heartImage;

    private GameObject result;

    private TextMeshProUGUI scoreboard;

    private TextMeshProUGUI goldText;

    private TextMeshProUGUI gemText;

    private Button backBtn;


    private bool start;
    public bool Start
    {
        get => start;
        set => start = value;
    }

    private void Awake()
    {
        hp = 2;
        start = false;
        score = 0;

        result = GameObject.Find("ResultFrame");

        if (gameFrame == null)
        {
            Debug.Log("111");
        }

        //scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreboard = GameObject.Find("ScoreBoard").GetComponent<TextMeshProUGUI>();
        goldText = GameObject.Find("GoldText").GetComponent<TextMeshProUGUI>();
        gemText = GameObject.Find("GemText").GetComponent<TextMeshProUGUI>();

        back1 = GameObject.Find("Background1");
        back2 = GameObject.Find("Background2");
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");

        backBtn = GameObject.Find("BackBtn").GetComponent<Button>();
        backBtn.onClick.AddListener(OnClick_BackBtn);
    }

    // Update is called once per frame
    void Update()
    {
        back1.transform.position += scrollSpeed * Time.deltaTime * Vector3.left;
        back2.transform.position += scrollSpeed * Time.deltaTime * Vector3.left;


        if(enemy.transform.position.x < 8.8)
        {
            player.transform.position += 8f * Time.deltaTime * Vector3.right;
            enemy.transform.position += 8f * Time.deltaTime * Vector3.right;
        }
        else if(enemy.transform.position.x >= 8.8 && hp == 2)
        {
            start = true;
        }

        if(start)
        {
            gameFrame.SetActive(true);
            score = score++;
            scoreText.SetText("Score : " + score++ / 100);
        }
        else if(hp < 0 && !start)
        {

        }


        if (back1.transform.position.x <= -25.6)
        {
            back1.transform.position = startPos;
        }
        if (back2.transform.position.x <= -25.6)
        {
            back2.transform.position = startPos;
        }
    }
    public void TakeDamage(int HP)
    {
        hp--;
        heartImage[HP].SetActive(false);
    }

    public void GameOver()
    {
        gold = score / 100;
        gem = score / 100;
        gemText.text = gem.ToString();
        goldText.text = gold.ToString();
        scoreboard.text = "Score\n" + (score/100).ToString();
        LeanTween.scale(result, Vector3.one, 0.7f).setEase(LeanTweenType.clamp);
    }

    private void OnClick_BackBtn()
    {
        GameManager.Inst.AsyncLoadNextScene(SceneName.Guild);
    }
}
