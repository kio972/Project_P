using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastList : MonoBehaviour
{
    [SerializeField] private Toast toast;
    private void OnDisable()
    {
        toast.Refresh();
    }
}
