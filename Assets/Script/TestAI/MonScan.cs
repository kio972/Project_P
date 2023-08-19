using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonScan : MonoBehaviour
{
    [SerializeField]
    private MonController monController;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            monController.Target = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            monController.Target = false;
        }
    }
}
