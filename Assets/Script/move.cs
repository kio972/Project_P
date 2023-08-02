using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YeongJun
{
    public class move : MonoBehaviour
    {
        // 테스트용 스크립트
        public float speed;
        float xMove;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            xMove = 0;
            if (Input.GetKey(KeyCode.RightArrow))
                xMove = speed * Time.deltaTime;
            else if (Input.GetKey(KeyCode.LeftArrow))
                xMove = -speed * Time.deltaTime;
            transform.Translate(new Vector3(xMove, 0, 0));
        }
    }
}
