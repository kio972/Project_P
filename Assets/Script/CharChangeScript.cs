using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YeongJun;

namespace Jun
{
    public class CharChangeScript : MonoBehaviour
    {
        private BattleSceneManager battleSceneManager;

        [SerializeField]
        private GameObject changeZone;
        private bool readyForChange = false;

        private int targetCharacter = 0;

        [SerializeField]
        private Sprite aiImg;
        [SerializeField]
        private Sprite mainImg;

        private Controller main;
        private Controller ai_1;
        private Controller ai_2;


        [SerializeField]
        private Image main_Image;
        [SerializeField]
        private Image ai_1_Image;
        [SerializeField]
        private Image ai_2_Image;
        [SerializeField]
        private Image main_Mark;
        [SerializeField]
        private Image ai_1_Mark;
        [SerializeField]
        private Image ai_2_Mark;

        [SerializeField]
        private CharStateUI main_CharState;
        [SerializeField]
        private CharStateUI ai_1_CharState;
        [SerializeField]
        private CharStateUI ai_2_CharState;

        private bool initState = false;

        //스킬창 변경을 위한 임시코드
        [SerializeField]
        private GameObject char1Skill;
        [SerializeField]
        private GameObject char2Skill;
        [SerializeField]
        private GameObject char3Skill;
        private Dictionary<Controller, GameObject> skillDic;
        //

        public void Init(Controller main, Controller ai_1, Controller ai_2, BattleSceneManager battleSceneManager)
        {
            this.main = main;
            this.ai_1 = ai_1;
            this.ai_2 = ai_2;
            this.battleSceneManager = battleSceneManager;

            main_CharState.Init(main);
            ai_1_CharState.Init(ai_1);
            ai_2_CharState.Init(ai_2);

            initState = true;

            //스킬창 변경을 위한 임시코드
            skillDic = new Dictionary<Controller, GameObject>();
            skillDic.Add(main, char1Skill);
            skillDic.Add(ai_1, char2Skill);
            skillDic.Add(ai_2, char3Skill);
        }

        private void ChangeSlotImage(int targetCharacter)
        {
            main_Image.sprite = aiImg;
            ai_1_Image.sprite = aiImg;
            ai_2_Image.sprite = aiImg;

            switch(targetCharacter)
            {
                case 0:
                    main_Image.sprite = mainImg;
                    break;
                case 1:
                    ai_1_Image.sprite = mainImg;
                    break;
                case 2:
                    ai_2_Image.sprite = mainImg;
                    break;
            }
        }

        void Update()
        {
            if (!initState)
                return;

            if (Input.GetKey(KeyCode.Tab))
            {
                changeZone.gameObject.SetActive(true);
                readyForChange = true;
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                    targetCharacter = 1;
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                    targetCharacter = 2;
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                    targetCharacter = 0;
                ChangeSlotImage(targetCharacter);
            }

            if(Input.GetKeyUp(KeyCode.Tab))
            {
                changeZone.gameObject.SetActive(false);
                ChangeCharacter(targetCharacter);
                targetCharacter = 0;
            }
        }

        private void SwapController(int target)
        {
            if (target == 1)
            {
                Controller temp = ai_1;
                ai_1 = main;
                main = temp;
            }
            else if (target == 2)
            {
                Controller temp = ai_2;
                ai_2 = main;
                main = temp;
            }
        }

        private void SwapMark(int target)
        {
            if (target == 1)
            {
                Sprite temp = ai_1_Mark.sprite;
                ai_1_Mark.sprite = main_Mark.sprite;
                main_Mark.sprite = temp;
            }
            else if (target == 2)
            {
                Sprite temp = ai_2_Mark.sprite;
                ai_2_Mark.sprite = main_Mark.sprite;
                main_Mark.sprite = temp;
            }
        }

        private void SwapCharState(int target)
        {
            if (target == 1)
            {
                ai_1_CharState.SwapCharState(main_CharState);
            }
            else if (target == 2)
            {
                ai_2_CharState.SwapCharState(main_CharState);
            }
        }

        private void SwapSkillSlot()
        {
            //스킬변경을위한 임시코드
            char1Skill?.SetActive(false);
            char2Skill?.SetActive(false);
            char3Skill?.SetActive(false);

            if(skillDic.ContainsKey(main))
                skillDic[main].SetActive(true);

        }

        private void ChangeCharacter(int target)
        {
            if (target == 0)
                return;

            //메인 Controller변경
            SwapController(target);
            //캐릭터 상태 변경
            battleSceneManager.ChangeMainChar(main);

            //UI업데이트
            SwapMark(target);
            SwapCharState(target);
            SwapSkillSlot();

            ChangeSlotImage(0);
        }
    }

}
