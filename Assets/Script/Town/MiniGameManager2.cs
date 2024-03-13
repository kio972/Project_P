using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniGameManager2 : MonoBehaviour
{
    [SerializeField] private GameObject Bucket;
    [SerializeField] private GameObject bottleObj1; // ���� ������Ʈ
    [SerializeField] private GameObject bottleObj2;
    [SerializeField] private GameObject road; // ��
    [SerializeField] private Transform bottlePos1; // ���� ��ġ
    [SerializeField] private Transform bottlePos2;
    [SerializeField] private GameObject cam; // ���� ī�޶�
    [SerializeField] private float angle; // ���� ����

    private float score = 0; // ����
    private float time = 60; // ���ѽð�
    private bool isPlay = false; // ���� ���࿩��
    private bool inclination = true; // true�� �������� ������

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Animation failAnim;
    [SerializeField] private GameObject gameStart;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private TextMeshProUGUI totalScore;

    void Start()
    {
        
    }

    public void StartButton()
    {
        gameStart.SetActive(true);
        gameStart.GetComponent<Animation>().Play();
        Invoke("StartGame", 4f);
    }

    public void StartGame()
    {// ���� ����
        Bucket.transform.rotation = Quaternion.Euler(0, 0, 0);
        gameStart.SetActive(false);
        isPlay = true;
        SetGame();
    }

    void SetGame()
    {
        score = 0;
        time = 60;
        inclination = true;
    }

    void EndGame()
    {// ���� ����
        isPlay = false;
        gameOver.SetActive(true);
        totalScore.text = score.ToString("F0");
    }
    
    void Update()
    {
        angle = Bucket.transform.rotation.eulerAngles.z;
        if (isPlay)
        {
            if (time > 0)
            {
                cam.transform.position = Bucket.transform.position; // ī�޶� �̵�
                time -= Time.deltaTime;
                timeText.text = time.ToString("F0");
                // ���� ���� ����
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    road.transform.position += Vector3.up * Time.deltaTime * 150;
                    score += Time.deltaTime * 5;
                    scoreText.text = score.ToString("F0");
                    if (inclination)
                    {
                        if (Input.GetKey(KeyCode.LeftArrow))
                            Bucket.transform.Rotate(Vector3.forward * Time.deltaTime * 150);
                        else if (Input.GetKey(KeyCode.RightArrow))
                            Bucket.transform.Rotate(Vector3.back * Time.deltaTime * 100);
                        else
                            Bucket.transform.Rotate(Vector3.forward * Time.deltaTime * 150);
                    }
                    else
                    {
                        if (Input.GetKey(KeyCode.LeftArrow))
                            Bucket.transform.Rotate(Vector3.forward * Time.deltaTime * 100);
                        else if (Input.GetKey(KeyCode.RightArrow))
                            Bucket.transform.Rotate(Vector3.back * Time.deltaTime * 150);
                        else
                            Bucket.transform.Rotate(Vector3.back * Time.deltaTime * 150);
                    }
                }
                else
                {
                    if (inclination)
                    {
                        if (Input.GetKey(KeyCode.LeftArrow))
                            Bucket.transform.Rotate(Vector3.forward * Time.deltaTime * 80);
                        else if (Input.GetKey(KeyCode.RightArrow))
                            Bucket.transform.Rotate(Vector3.back * Time.deltaTime * 30);
                        else
                            Bucket.transform.Rotate(Vector3.forward * Time.deltaTime * 100);
                    }
                    else
                    {
                        if (Input.GetKey(KeyCode.LeftArrow))
                            Bucket.transform.Rotate(Vector3.forward * Time.deltaTime * 30);
                        else if (Input.GetKey(KeyCode.RightArrow))
                            Bucket.transform.Rotate(Vector3.back * Time.deltaTime * 80);
                        else
                            Bucket.transform.Rotate(Vector3.back * Time.deltaTime * 100);
                    }
                }
                bottleObj1.transform.position = bottlePos1.position;
                bottleObj2.transform.position = bottlePos2.position;

                if (angle >= 60 && angle <= 300)
                {// ���� ����
                    isPlay = false;
                    EndGame();
                }
                else if (angle > 0 && angle < 2)
                    inclination = true;
                else if (angle > 358 && angle < 360)
                    inclination = false;
                if (score >= 100)
                    EndGame();
            }
            else if (time <= 0)
            {// Ÿ�� ����
                EndGame();
            }
        }
    }
}
