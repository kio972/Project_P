using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryArea : MonoBehaviour
{
    // ī�޶� ���� ������ ����� ������Ʈ�� �Ҹ� �����ִ� ����. 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))// "Obstacle" �±׸� ���� ������Ʈ�� �ۿ�
        {
            if (collision.TryGetComponent<ObjectPool_Label>(out ObjectPool_Label label))
            {
                label.Push();
            }
            else
            {
                Destroy(collision.gameObject); // �ش� ���� ������Ʈ�� �ı�.
            }

        }
    }

}
