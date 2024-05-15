using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VespaWaspa : MonController
{
    private ProjectileSpawn spawn;

    public override void AttackBasic()
    {
        LookAtDir();
        basicAttackCool = false;
        chaseSpeed = 0f;
        anim.SetBool("Run", false);
        StartCoroutine(CoolTimeBasic(3f));
        anim.SetTrigger("BasicAttack");
    }

    private float projectileScale;

    public void PoisonDart()
    {
        if (transform.localScale.x >= 0f)
            projectileScale = 1;
        else
            projectileScale = -1;

        if(spawn != null)
            spawn.SpawnProjectile("PoisonDart", 5f, projectileScale);
        else
        {
            spawn = GetComponentInChildren<ProjectileSpawn>();
            spawn.SpawnProjectile("PoisonDart", 5f, projectileScale);
        }
    }
}
