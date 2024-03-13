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

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
                return;

            Controller player = collision.GetComponentInParent<Controller>();
            if (player == null || player.CurState != FSMStateEx.Instance)
                return;

            float direction = (player.transform.position.x - transform.position.x);
            int nextArea = portalNumber;

            if (direction > 0)
                nextArea++;

            bsm.area = nextArea;
            bsm.SetBattleScene(bsm.area);
        }
    }
}
