using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{
    public Animator[] animator1;
    public Animator[] animator2;
    public void Prev()
    {
        for (int i = 0; i < animator1.Length; i++)
            animator1[i].SetTrigger("prev");
    }

    public void Next()
    {
        for (int i = 0; i < animator1.Length; i++)
            animator1[i].SetTrigger("next");
    }
    public void Prev2()
    {
        for (int i = 0; i < animator2.Length; i++)
            animator2[i].SetTrigger("prev");
    }

    public void Next2()
    {
        for (int i = 0; i < animator2.Length; i++)
            animator2[i].SetTrigger("next");
    }
}
