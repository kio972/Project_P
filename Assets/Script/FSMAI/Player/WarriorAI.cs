using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAI : MonoBehaviour
{
    [SerializeField]
    public DrawGizmo attackPivot;
    [SerializeField]
    public DrawGizmo skill1Pivot;
    [SerializeField]
    public LayerMask targetLayer;

    [SerializeField]
    private ParticleSystem skillEffect;

    [SerializeField]
    Controller controller;

    private void Awake()
    {
        //controller = GetComponentInParent<Controller>();

        /*Transform basicT = controller.transform.Find("BasicPivot");
        if (basicT != null)
            attackPivot = basicT.GetComponent<DrawGizmo>();

        Transform skill1T = controller.transform.Find("Skill1Pivot");
        if (skill1T != null)
            skill1Pivot = skill1T.GetComponent<DrawGizmo>();*/

    }

    private int damage;

    public void BasicAttack()
    {
        damage = controller.DamageCalculate(1f);

        Collider2D[] enemyArr =
            Physics2D.OverlapBoxAll(attackPivot.transform.position,
                                 attackPivot.size,
                                 0,
                                 targetLayer);

        foreach (Collider2D enemy in enemyArr)
        {
            if (enemy.TryGetComponent<MonController>(out MonController monController))
            {
                monController.TakeDamage(damage, transform);
            }
            else if (enemy.TryGetComponent<Rock>(out Rock rock))
            {
                rock.BreakenRock();
            }
        }
    }

    public void Skill1Attack()
    {
        damage = controller.DamageCalculate(1.8f);

        skillEffect.Play();

        Collider2D[] enemyArr =
            Physics2D.OverlapBoxAll(skill1Pivot.transform.position,
                                 skill1Pivot.size,
                                 0,
                                 targetLayer);

        foreach (Collider2D enemy in enemyArr)
        {
            if (enemy.TryGetComponent<MonController>(out MonController monController))
            {
                monController.TakeDamage(damage, transform);
            }
            else if(enemy.TryGetComponent<Rock>(out Rock rock))
            {
                rock.BreakenRock();
            }
        }
    }
}
