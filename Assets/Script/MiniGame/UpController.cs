using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UpController : MonoBehaviour
{

    [SerializeField]
    public float moveSpeed = 5f;

    private bool flag;
    private bool gameState;

    private GameObject result;

    private void Awake()
    {
        gameState = true;
        result = GameObject.Find("ResultFrame");
        flag = UpManager.Inst.FLAG;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState)
        {
            float moveInput = Input.GetAxis("Horizontal");

            Vector3 moveDirection = new Vector3(moveInput, 0f, 0f);

            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }

    }


    Vector3 newPosition = Vector3.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Up"))
        {
            newPosition += new Vector3(0f, 11f, 0f);
            transform.position = newPosition;
            UpManager.Inst.GenerateRandomBool();
            flag = UpManager.Inst.FLAG;
            UpManager.Inst.SCORE++;
            UpManager.Inst.SCORETEXT.text = "Score : " + UpManager.Inst.SCORE;
            if(UpManager.Inst.SCORE >= 5)
            {
                GameOver();
            }
        }
        else if (collision.gameObject.CompareTag("Down"))
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        UpManager.Inst.GOLD = UpManager.Inst.SCORE * 10;
        UpManager.Inst.GEM = UpManager.Inst.SCORE;
        UpManager.Inst.GEMTEXT.text = UpManager.Inst.GEM.ToString();
        UpManager.Inst.GOLDTEXT.text = UpManager.Inst.GOLD.ToString();
        UpManager.Inst.SCOREBOARD.text = "Score\n"+ UpManager.Inst.SCORE.ToString();
        LeanTween.scale(result, Vector3.one, 0.7f).setEase(LeanTweenType.clamp);
        gameState = false;
    }
}
