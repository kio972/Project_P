using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private float zOffset = -10f; // Z���� ������ ������

    private void Awake()
    {
        target = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        // ī�޶��� ��ġ�� ����
        Vector3 newPosition = new Vector3(0f, target.position.y, zOffset);
        transform.position = newPosition;
    }
}