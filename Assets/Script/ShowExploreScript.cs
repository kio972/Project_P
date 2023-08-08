using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Jun
{
    public class ShowExploreScript : MonoBehaviour
    {

        public bool isUIActive = false;

        public GameObject ExploreUI;

        private void Start()
        {
            ExploreUI.SetActive(false);
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
        }
    }


}


