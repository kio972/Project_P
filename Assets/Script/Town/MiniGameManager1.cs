using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniGameManager1 : MonoBehaviour
{
    [SerializeField] private Sprite noteImg1; // ��Ʈ�� ���� �̹���
    [SerializeField] private Sprite noteImg2;
    [SerializeField] private Sprite noteImg3;
    [SerializeField] private Sprite noteImg4;
    [SerializeField] private int[] notesNum; // �� ��Ʈ�� ����
    [SerializeField] private Image[] notesImg; // �� ��Ʈ�� �̹���

    private int score = 0; // ����
    private float time = 60; // ���ѽð�
    private int level = 1; // ����
    private bool isPlay = false; // ���� ���࿩��
    private bool isControll = false; // ���� ���ɿ���
    private int progress = 0; // �������

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Animation failAnim;
    [SerializeField] private GameObject gameStart;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private TextMeshProUGUI totalScore;

    private Color normalColor = new Color(1, 1, 1, 1); // �Էµ��� ���� ����
    private Color clearColor = new Color(1, 1, 1, 0.3f); // �Է��� ����


    void Awake()
    {
        notesNum = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    }

    public void StartButton()
    {
        gameStart.SetActive(true);
        gameStart.GetComponent<Animation>().Play();
        Invoke("StartGame", 4f);
    }

    public void StartGame()
    {// ���� ����
        gameStart.SetActive(false);
        isPlay = true;
        isControll = true;
        SetGame();
    }

    void SetGame()
    {// ��Ʈ ���
        for (int i = 0; i < 2 + (level * 2); i++)
        {
            notesNum[i] = Random.Range(0, 4);
            notesImg[i].gameObject.SetActive(true);
            switch (notesNum[i])
            {// �� ��Ʈ ���⼳��
                case 0:
                    notesImg[i].sprite = noteImg1;
                    break;
                case 1:
                    notesImg[i].sprite = noteImg2;
                    break;
                case 2:
                    notesImg[i].sprite = noteImg3;
                    break;
                case 3:
                    notesImg[i].sprite = noteImg4;
                    break;
            }
            notesImg[i].color = normalColor;
        }
    }

    void ClearNote()
    {// ��Ʈ �Է� ���� ��
        notesImg[progress].color = clearColor;
        progress++;
        if (progress == 2 + (level * 2))
            ClearGame();
    }

    void FailNote()
    {// ��Ʈ �Է� ���� ��
        failAnim.Play();
        isControll = false;
        Invoke("ReControll", 1f);
    }

    void ReControll()
    {
        isControll = true;
    }

    void ClearGame()
    {// ��Ʈ ���� Ŭ���� ��
        score++;
        scoreText.text = score.ToString();
        if (level < 4 && score % 5 == 0)
            level++;
        progress = 0;
        SetGame();
    }

    void InputButton(int number)
    {
        if (notesNum[progress] == number)
            ClearNote();
        else
            FailNote();
    }

    void EndGame()
    {// ���� ����
        isPlay = false;
        gameOver.SetActive(true);
        totalScore.text = score.ToString();
    }

    public void Exit()
    {
        gameOver.SetActive(false);
    }

    void Update()
    {
        if (isPlay)
        {
            if(time > 0)
            {
                time -= Time.deltaTime;
                timeText.text = time.ToString("F0");
            }
            if (isControll)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    InputButton(0);
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    InputButton(1);
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    InputButton(2);
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                    InputButton(3);
            }
            if(time <= 0)
            {
                isPlay = false;
                EndGame();
            }
        }
    }
}
