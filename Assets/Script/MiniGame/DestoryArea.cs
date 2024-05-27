using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryArea : MonoBehaviour
{
    // 카메라 랜더 영역을 벗어나는 오브젝트를 소멸 시켜주는 역할. 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))// "Obstacle" 태그를 가진 오브젝트와 작용
        {
            if (collision.TryGetComponent<ObjectPool_Label>(out ObjectPool_Label label))
            {
                label.Push();
            }
            else
            {
                Destroy(collision.gameObject); // 해당 게임 오브젝트를 파괴.
            }

        }
    }

}
