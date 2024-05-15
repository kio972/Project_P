using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosphy : MonController
{
    public override void AttackBasic()
    {
        LookAtDir();
        basicAttackCool = false;
        chaseSpeed = 0f;
        anim.SetBool("Run", false);
        StartCoroutine(CoolTimeBasic(3f));
        anim.SetTrigger("BasicAttack");
        SoundManager.Inst.PlaySFX("Spear_Day_Sting");
    }
}
