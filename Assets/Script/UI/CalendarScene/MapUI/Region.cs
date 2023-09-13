using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> cloudList = new List<GameObject>();

    [SerializeField]
    private GameObject cloud;
    [SerializeField]
    private GameObject load;

    public void CloudInit(int num)
    {
        LeanTween.scale(cloudList[num - 1], Vector3.zero, 2f);
    }

    public void CloudeActive(bool init)
    {
        if (init)
            cloud.transform.localScale = Vector3.one;
        else
            cloud.transform.localScale = Vector3.zero;
    }

    public void LoadActive(bool init)
    {
        if (init)
            load.SetActive(true);
        else
            load.SetActive(false);
    }
}
