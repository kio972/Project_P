using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JinWon;
using TMPro;


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
    public Dictionary<int, bool> forestClear = new Dictionary<int, bool>();
    public float currHP;
    // ������ �÷��̾� ������

    public string charactorVec; // �ӽ�, �� �� ���� ��������.

    public int gold;
    public int ston;

    public float bgm;
    public float sfx;
    public List<string> sfxList;
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
    public string Info = "����";
    public string hidden1 = "�ܷõ� �� : �޴� ������ ����";
    public string hidden2 = "������ : Ž�� ����ġ ȹ��� ����";
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
    public string hidden1 = "�ö�ú� : ���� Ȯ���� ������ �Һ� ��ȿ";
    public string hidden2 = "��� : �ִ� ���� ���� ����";
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
    public string hidden1 = "����� �˹ٻ� : �Ƹ�����Ʈ ��Ŀ���� ����";
    public string hidden2 = "�߸��� : �� ���� ��    ���߷� ����";
}

namespace JinWon
{
    public class GameManager : Singleton<GameManager>
    {
        private bool calendarProd = false; // Ķ������ ������ �����ΰ�.
        public bool CalendarProd
        {
            get { return calendarProd; }
            set { calendarProd = value; }
        }

        private int calendarProdRegion;
        public int CalendarProdRegion
        {
            get { return calendarProdRegion; }
            set { calendarProdRegion = value; }
        }

        private int calendarProdCloud;
        public int CalendarProdCloud
        {
            get { return calendarProdCloud; }
            set { calendarProdCloud = value; }
        }

        private FadeInOut fade;


        private PlayerData pData;
        public PlayerData PlayerInfo
        {
            get { return pData; }
            set { pData = value; }
        }

        private SceneName nextScene;
        public SceneName NextScene
        {
            get { return nextScene; }
        }

        private bool sceneLoad;
        private string strNextScene;

        public void AsyncLoadNextScene(SceneName scene) // �� �Ѿ������ ��Ȱ���ϴ� �ڵ�
        {
            nextScene = scene;
            SceneManager.LoadScene(nextScene.ToString());
            //SceneManager.LoadScene(SceneName.LoadingScene.ToString()); // �ε����� ���� �Ǹ� Ȱ��
        }

        
        public void AsyncLoadNextScene(string scene)
        {
            if(!sceneLoad)
            {
                sceneLoad = true;
                strNextScene = scene;
                StartCoroutine(NextSceneCoroutine());
            }
        }

        IEnumerator NextSceneCoroutine()
        {
            fade.Fade_InOut(false, 3.0f);
            yield return YieldInstructionCache.WaitForSeconds(3.0f);
            sceneLoad = false;
            SceneManager.LoadScene(strNextScene);
        }

        public Texture2D cursorTexture;
        public Vector2 hotspot = Vector2.zero;
        public CursorMode cursorMode = CursorMode.Auto;

        void Start()
        {
            base.Awake();
            GameManagerInit();
        }

        public void GameManagerInit()
        {
            pData = new PlayerData();
            Fade_InOut(true, 3.0f);
            sceneLoad = false;
            Debug.Log("���̵� ��");
            Cursor.SetCursor(cursorTexture, hotspot, cursorMode);

            PlayerDataInit();
        }

        public void PlayerDataInit()
        {
            pData.charactorVec = "Left";
            pData.currHP = 220f;
            pData.gold = pData.ston = 10;
            SoundInit();

            pData.forestClear.Clear();
            // ������Ʈ ��Ʋ�� ��Ŭ����� ����
            for (int i = 1; i <= 3; i++)
            {
                pData.forestClear.Add(i, false);
            }
        }

        private void SoundInit()
        {
            pData.bgm = 1f;
            pData.sfx = 1f;
            pData.sfxList = new List<string>
            {
                "Warrior_BasicAttack",
                "Smash_TakeDamage",
                "Warrior_Dash",
                "Warrior_Jump",
                "PotalWarp",
                "Spear_Day_Sting",
                "Spear_Sting_TakeDamage",
                "Click_on",
                "Click_off",
                "Coin_Drop",
                "Walk_1.4"
            };
        }

        public void Fade_InOut(bool init, float time)
        {
            fade = GameObject.Find("Fade").GetComponent<FadeInOut>();
            fade.Fade_InOut(init, time);
        }

        public void CharHPInit(float hp)
        {
            pData.currHP = hp;
            Debug.Log("@@@@@@@@�÷��̾� ü�� : " + pData.currHP);
        }

        public void StageClear(int stage)
        {
            if(pData.forestClear[stage] == false)
                pData.forestClear[stage] = true;
        }

        public void CharactorVec(string vec)
        {
            if (vec == "Right")
                pData.charactorVec = vec;
            else if (vec == "Left")
                pData.charactorVec = vec;
            else
                Debug.Log("���縵�� Ʋ�ȴ��� Ȯ��!!");
        }

    }
}

