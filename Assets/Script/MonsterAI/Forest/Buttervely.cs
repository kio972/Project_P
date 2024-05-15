using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttervely : MonController
{
    private ProjectileSpawn spawn;

    private float projectileScale = 0f;

    public override void AttackBasic()
    {
        LookAtDir();
        basicAttackCool = false;
        StartCoroutine(CoolTimeBasic(3f));
        anim.SetTrigger("BasicAttack");
    }

    Vector3 playerVec;

    public void SpawnProjectile()
    {
        playerVec = GameObject.Find("Warrior").transform.position;


        if (spawn != null)
            spawn.ButtervelyProjectile("Buttervely_Missile", 2f, projectileScale, playerVec);
        else
        {
            spawn = GetComponentInChildren<ProjectileSpawn>();
            spawn.ButtervelyProjectile("Buttervely_Missile", 2f, projectileScale, playerVec);
        }
    }
}
