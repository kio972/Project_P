using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{

    [SerializeField] float speed = 200f;
    float moveX, moveY;
    Rigidbody2D rb;
    


    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(moveX * speed * Time.deltaTime, moveY * speed * Time.deltaTime);
    }
}
