using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckGizmos : MonoBehaviour
{
    [SerializeField]
    public Vector3 size = Vector3.one;
    public Color color = Color.red;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
