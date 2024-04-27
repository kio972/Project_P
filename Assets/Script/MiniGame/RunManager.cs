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

    private int life;
    private int score;

    [SerializeField]
    private TextMeshProUGUI scoreText;


    [SerializeField]
    private GameObject gameFrame;


    [SerializeField]
    private float scrollSpeed = 3f;
    [SerializeField]
    private Vector3 startPos;

    private bool start;
    public bool Start
    {
        get => start;
        set => start = value;
    }

    private void Awake()
    {
        start = false;
        score = 0;
        life = 3;

        if (gameFrame == null)
        {
            Debug.Log("111");
        }

        //scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();

        back1 = GameObject.Find("Background1");
        back2 = GameObject.Find("Background2");
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
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
        else if(enemy.transform.position.x >= 8.8 && life == 3)
        {
            start = true;
        }

        if(start)
        {
            gameFrame.SetActive(true);
            score = score++;
            scoreText.SetText("Score : " + score++ / 100);
        }
        else if(life <= 0 && !start)
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
}
