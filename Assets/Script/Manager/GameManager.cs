using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JinWon;

public enum SceneName
{
    TitleScene,
    CalendarScene,
    LoadingScene, // 임시
}

public class PlayerData
{
    // 저장할 플레이어 데이터
}

namespace JinWon
{
    public class GameManager : Singleton<GameManager>
    {

        private PlayerData pData;
        public PlayerData PlayerInfo
        {
            get { return pData; }
        }

        private SceneName nextScene;
        public SceneName NextScene
        {
            get { return nextScene; }
        }

        public void AsyncLoadNextScene(SceneName scene) // 씬 넘어갈때마다 재활용하는 코드
        {
            nextScene = scene;
            SceneManager.LoadScene(nextScene.ToString());
            //SceneManager.LoadScene(SceneName.LoadingScene.ToString()); // 로딩씬이 구현 되면 활용
        }

        public void AsyncLoadNextScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }


        void Start()
        {
            base.Awake();
            pData = new PlayerData();
        }

    }
}

