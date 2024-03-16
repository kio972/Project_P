using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniGameManager2 : MonoBehaviour
{
    [SerializeField] private GameObject Bucket;
    [SerializeField] private GameObject bottleObj1; // 물통 오브젝트
    [SerializeField] private GameObject bottleObj2;
    [SerializeField] private GameObject road; // 길
    [SerializeField] private Transform bottlePos1; // 물통 위치
    [SerializeField] private Transform bottlePos2;
    [SerializeField] private GameObject cam; // 메인 카메라
    [SerializeField] private float angle; // 물통 각도

    private float score = 0; // 점수
    private float time = 60; // 제한시간
    private bool isPlay = false; // 게임 진행여부
    private bool inclination = true; // true면 왼쪽으로 기울어짐

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
    {// 게임 시작
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
    {// 게임 종료
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
                cam.transform.position = Bucket.transform.position; // 카메라 이동
                time -= Time.deltaTime;
                timeText.text = time.ToString("F0");
                // 물통 각도 조정
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
                {// 게임 오버
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
            {// 타임 오버
                EndGame();
            }
        }
    }
}
