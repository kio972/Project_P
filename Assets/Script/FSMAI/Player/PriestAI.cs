using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestAI : Controller
{
    public DrawGizmo attackPivot;
    [SerializeField]
    public LayerMask targetLayer;

    private void Awake()
    {
        Transform t = transform.Find("BasicPivot");
        if (t != null)
            attackPivot = t.GetComponent<DrawGizmo>();
    }

    public void BasicAttack()
    {

        Collider2D[] enemyArr =
            Physics2D.OverlapBoxAll(attackPivot.transform.position,
                                 attackPivot.size,
                                 0,
                                 targetLayer);

        foreach (Collider2D enemy in enemyArr)
        {
            if (enemy.TryGetComponent<MonController>(out MonController monController))
            {
                monController.TakeDamage(1);
            }
        }
    }
}
