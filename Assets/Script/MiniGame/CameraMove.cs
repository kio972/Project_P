using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private float zOffset = -10f; // Z축의 고정된 오프셋

    private void Awake()
    {
        target = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        // 카메라의 위치를 설정
        Vector3 newPosition = new Vector3(0f, target.position.y, zOffset);
        transform.position = newPosition;
    }
}