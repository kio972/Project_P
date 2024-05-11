using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpController : MonoBehaviour
{

    [SerializeField]
    public float moveSpeed = 5f;


    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");

        Vector3 moveDirection = new Vector3(moveInput, 0f, 0f);

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }


    Vector3 newPosition = Vector3.zero;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Up"))
        {
            newPosition += new Vector3(0f, 11f, 0f);
            transform.position = newPosition;
        }
    }
}
