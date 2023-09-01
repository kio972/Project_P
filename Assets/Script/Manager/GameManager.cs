using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JinWon;

public enum SceneName
{
    TitleScene,
    CalendarScene,
    LoadingScene, // �ӽ�
}

public class PlayerData
{
    // ������ �÷��̾� ������
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

        public void AsyncLoadNextScene(SceneName scene) // �� �Ѿ������ ��Ȱ���ϴ� �ڵ�
        {
            nextScene = scene;
            SceneManager.LoadScene(nextScene.ToString());
            //SceneManager.LoadScene(SceneName.LoadingScene.ToString()); // �ε����� ���� �Ǹ� Ȱ��
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

