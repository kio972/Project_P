using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class CurrentMap : MonoBehaviour
{
    [SerializeField]
    RawImage cam;

    private float camSpeed = 5f;
    

    void Update()
    {
        float moveDir = Input.GetAxis("Horizontal");
        moveDir += camSpeed;
        transform.position = new Vector3(transform.position.x + moveDir * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
