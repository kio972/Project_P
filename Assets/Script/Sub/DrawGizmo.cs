using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PivotType
{
    basic,
    skill1,
    skill2,
}

public class DrawGizmo : MonoBehaviour
{
    [SerializeField]
    public Vector3 size = Vector3.one;
    public Color color = Color.red;

    [SerializeField]
    private string tagName;

    [SerializeField]
    private PivotType pivotType;

    private MonController monController;

    private void Awake()
    {
        monController = GetComponentInParent<MonController>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireCube(transform.position, size);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(tagName))
        {
            switch(pivotType)
            {
                case PivotType.basic:
                    {
                        monController.BasicTarget = true;
                        break;
                    }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(tagName))
        {
            switch (pivotType)
            {
                case PivotType.basic:
                    {
                        monController.BasicTarget = false;
                        break;
                    }
            }
        }
    }
}
