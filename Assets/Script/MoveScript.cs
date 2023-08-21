using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jun
{
    public class MoveScript : MonoBehaviour
    {

        public float moveSpeed = 5f;
        public float jumpForce = 7f;
        private bool isJumping = false;
        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;


        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            // 플레이어 이동
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector2 moveDirection = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
            rb.velocity = moveDirection;

            // Direction sprite
            if (Input.GetButtonDown("Horizontal"))
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

            // 점프 (윗 방향키로 변경)
            if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isJumping = false;
            }
        }


    }
}
