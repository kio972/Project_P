using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Jun
{
    public class ShowExploreScript : MonoBehaviour
    {

        public bool isUIActive = false;
        public bool isUIActive2 = false;

        public GameObject ExploreUI;
        public GameObject ExploreUI2;

        private void Start()
        {
            ExploreUI.SetActive(false);
            //ExploreUI2.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleUI();
            }
        }

        private void ToggleUI()
        {
            ExploreUI.SetActive(!ExploreUI.activeSelf);
            //ExploreUI2.SetActive(!ExploreUI2.activeSelf);
        }
    }


}


