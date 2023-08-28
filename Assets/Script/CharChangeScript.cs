using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Jun
{
    public class CharChangeScript : MonoBehaviour
    {
        private Mask maskComponent;
        private Image img;
        private bool isMaskOn = false;


        void Start()
        {
            maskComponent = GetComponent<Mask>();

            maskComponent.showMaskGraphic = false;
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ToggleMask();
            }
        }

        private void ToggleMask()
        {
            isMaskOn = !isMaskOn;


            maskComponent.showMaskGraphic = isMaskOn;
        }

    }

}
