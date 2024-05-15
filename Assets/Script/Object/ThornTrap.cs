using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinWon;

public class ThornTrap : MonoBehaviour
{

    private bool isDamage = true;

    private Vector2 vec;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isDamage)
        {
            if (collision.CompareTag("Player") && collision.TryGetComponent<Controller>(out Controller player))
            {
                isDamage = false;
                Debug.Log("ÇÃ·¹ÀÌ¾î¿Í Á¢ÃË µÊ");
                player.TakeDamage(10, vec);
                Debug.Log(vec);
                StartCoroutine(TrapCool(2f));
            }
        }
    }

    IEnumerator TrapCool(float cool)
    {
        yield return YieldInstructionCache.WaitForSeconds(cool);
        isDamage = true;
    }
}
