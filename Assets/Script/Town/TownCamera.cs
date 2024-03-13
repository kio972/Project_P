using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Vector3 camPos = new Vector3(0, 34, 35);
    private float speed = 10f;
    private float inputX;
    private float inputY;
    [SerializeField]
    private Animator playerAnim;

    public void Init()
    {
        playerAnim = player.GetComponent<Animator>();
    }

    public void PlayerMove()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        if (inputX != 0 || inputY != 0)
        {
            player.transform.forward = new Vector3(-inputX, 0, -inputY);
            playerAnim.SetBool("run", true);
        }
        else
            playerAnim.SetBool("run", false);
        player.transform.position -= new Vector3(inputX, 0, inputY) * speed * Time.deltaTime;
        transform.position = player.transform.position + camPos;
    }
}
