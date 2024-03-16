using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{
    public Animator[] animator;
    public void Prev()
    {
        for (int i = 0; i < animator.Length; i++)
            animator[i].SetTrigger("prev");
    }

    public void Next()
    {
        for (int i = 0; i < animator.Length; i++)
            animator[i].SetTrigger("next");
    }
}
