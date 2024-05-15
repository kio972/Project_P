using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonScan : MonoBehaviour
{
    [SerializeField]
    private MonController monController;

    private void Awake()
    {
        monController = GetComponentInParent<MonController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !monController.Target)
        {
            if(!monController.Target)
                monController.Target = true;
        }
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && monController.Target)
        {
            monController.Target = false;
        }
    }*/
}
