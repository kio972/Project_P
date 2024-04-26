using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RunManager : MonoBehaviour
{
    private GameObject back1;
    private GameObject back2;


    [SerializeField]
    private float scrollSpeed = 3f;
    [SerializeField]
    private Vector3 startPos;

    private void Awake()
    {
        back1 = GameObject.Find("Background1");
        back2 = GameObject.Find("Background2");
    }

    // Update is called once per frame
    void Update()
    {
        back1.transform.position += scrollSpeed * Time.deltaTime * Vector3.left;
        back2.transform.position += scrollSpeed * Time.deltaTime * Vector3.left;

        if(back1.transform.position.x <= -25.6)
        {
            back1.transform.position = startPos;
        }
    }
}
