using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YeongJun
{
    public class portal : MonoBehaviour
    {
        public int portalNumber;
        [SerializeField]
        private BattleSceneManager bsm;

        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.CompareTag("Player"))
            {
                if (portalNumber < bsm.area)
                    --bsm.area;
                else
                    ++bsm.area;
            }
            bsm.ChangeCam(bsm.area);
        }
    }
}
