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

public enum PlayerType
{
    Warrior,
    Priest,
    Archer,
}

public class PlayerData
{
    // 저장할 플레이어 데이터
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
    public string Info = "인간 (남) 전사";
    public string hidden1 = "단련된 몸 : 받는 데미지 감소";
    public string hidden2 = "깨달음 : 탐색 경험치 획득률 증가";
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
    public string Info = "인간 (여) 신관";
    public string hidden1 = "플라시보 : 낮은 확률로 아이템 소비 무효";
    public string hidden2 = "허약 : 주는 피해 소폭 감소";
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
    public string Info = "엘프 (여) 궁수";
    public string hidden1 = "노련한 알바생 : 아르바이트 리커버리 증가";
    public string hidden2 = "야맹증 : 밤 전투 시    명중률 감소";
}

namespace JinWon
{
    public class GameManager : Singleton<GameManager>
    {
        private bool calendarProd = false; // 캘린더씬 연출을 볼것인가.
        public bool CalendarProd
        {
            get { return calendarProd; }
            set { calendarProd = value; }
        }

        private int calendarProdRegion = 1;
        public int CalendarProdRegion
        {
            get { return calendarProdRegion; }
            set { calendarProdRegion = value; }
        }

        private int calendarProdCloud = 1;
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
        }

        private SceneName nextScene;
        public SceneName NextScene
        {
            get { return nextScene; }
        }

        private bool sceneLoad;
        private string strNextScene;

        public void AsyncLoadNextScene(SceneName scene) // 씬 넘어갈때마다 재활용하는 코드
        {
            nextScene = scene;
            SceneManager.LoadScene(nextScene.ToString());
            //SceneManager.LoadScene(SceneName.LoadingScene.ToString()); // 로딩씬이 구현 되면 활용
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


        void Start()
        {
            base.Awake();
            pData = new PlayerData();
            Fade_InOut(true, 3.0f);
            sceneLoad = false;
            Debug.Log("페이드 인");
        }

        public void Fade_InOut(bool init, float time)
        {
            fade = GameObject.Find("Fade").GetComponent<FadeInOut>();
            fade.Fade_InOut(init, time);
        }


    }
}

