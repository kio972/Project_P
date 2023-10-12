using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using JinWon;

public class LoadingScene : MonoBehaviour
{
    [SerializeField]
    private Image loadingBar;

    [SerializeField]
    private GameObject loadingBarObj;

    private void Awake()
    {
        loadingBar.fillAmount = 0f;
        StartCoroutine("LoadAsyncScene");

        Color color = loadingBar.color;
        color.a = 0f;
    }

    IEnumerator LoadAsyncScene()
    {
        yield return null;

        yield return YieldInstructionCache.WaitForSeconds(4f);
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(GameManager.Inst.NextScene.ToString());
        asyncScene.allowSceneActivation = false;
        float timeC = 0f;
        while (!asyncScene.isDone)
        {
            yield return null;
            timeC += Time.deltaTime;

            if (asyncScene.progress >= 0.9f)
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1f, timeC);
                if (loadingBar.fillAmount >= 0.999f)
                    asyncScene.allowSceneActivation = true;
            }
            else
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, asyncScene.progress, timeC);
                if (loadingBar.fillAmount >= asyncScene.progress)
                    timeC = 0f;
            }
        }
    }
}
