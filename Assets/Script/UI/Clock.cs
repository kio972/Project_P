using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField]
    private GameObject clock;

    private Quaternion start;
    private Quaternion end;
    bool day = true;

    public IEnumerator IChangeTime()
    {// 시계가 회전하는 코루틴
        float elapsedTime = 0;
        float targetTime = 2.0f;
        day = !day;
        if (day)
        {
            start = Quaternion.Euler(0, 0, 180);
            end = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            start = Quaternion.Euler(0, 0, 360);
            end = Quaternion.Euler(0, 0, 180);
        }
        while (elapsedTime < 2.0f)
        {
            elapsedTime += Time.deltaTime / targetTime;
            clock.transform.rotation = Quaternion.Lerp(start, end, elapsedTime);
            yield return null;
        }
    }

    public void ChangeTime()
    {
        StartCoroutine(IChangeTime());
    }
}
