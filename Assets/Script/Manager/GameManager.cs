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

public enum PlayerType
{
    Warrior,
    Priest,
    Archer,
}

public class PlayerData
{
    // ������ �÷��̾� ������
}

public class WarriorInfo
{
    public string Name = "Warrior";
    public int Lv = 1;
    public float HP = 220;
    public float MP = 100;
    public int Str = 13;
    public int Int = 7;
    public int Vit = 15;
    public int Dex = 8;
    public int Luk = 7;
    public string Info = "�ΰ� (��) ����";
}

public class PriestInfo
{
    public string Name = "Priest";
    public int Lv = 1;
    public float HP = 150;
    public float MP = 130;
    public int Str = 6;
    public int Int = 13;
    public int Vit = 12;
    public int Dex = 8;
    public int Luk = 11;
    public string Info = "�ΰ� (��) �Ű�";
}

public class ArcherInfo
{
    public string Name = "Archer";
    public int Lv = 1;
    public float HP = 100;
    public float MP = 85;
    public int Str = 16;
    public int Int = 6;
    public int Vit = 9;
    public int Dex = 10;
    public int Luk = 9;
    public string Info = "���� (��) �ü�";
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

