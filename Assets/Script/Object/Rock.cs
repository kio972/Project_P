using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private SpriteRenderer sr;

    private Color color = new Color(1, 1, 1, 0.8f);
    private Color color2 = new Color(1, 1, 1, 0.5f);

    private int breakenCount;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        breakenCount = 1;
    }

    public void BreakenRock()
    {
        if(breakenCount == 1)
        {
            breakenCount--;
            sr.color = color;
        }
        else if(breakenCount == 0)
        {
            breakenCount--;
            sr.color = color2;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
