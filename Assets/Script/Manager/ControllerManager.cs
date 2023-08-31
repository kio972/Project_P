using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public List<Controller> controllerList;

    // Start is called before the first frame update
    void Start()
    {
        ControllerListAdd();
    }

    private void ControllerListAdd()
    {
        if(transform.Find("Warrior"))
        {
            controllerList.Add(transform.Find("Warrior").GetComponentInChildren<Controller>());
        }
        if (transform.Find("Priest"))
        {
            controllerList.Add(transform.Find("Priest").GetComponentInChildren<Controller>());
        }
        if (transform.Find("Archer"))
        {
            controllerList.Add(transform.Find("Archer").GetComponentInChildren<Controller>());
        }

        ControllerChange(0);
    }

    public void ControllerChange(int who)
    {
        for (int i = 0; i < controllerList.Count; i++)
        {
            if (i == who)
            {
                controllerList[i].ControllMode = true;
                controllerList[i].InitState(true);
            }
            else
            {
                controllerList[i].ControllMode = false;
                controllerList[i].InitState(false);
            }
                

            //controllerList[i].ChangeMode();
        }
    }

    public Transform TargetController()
    {
        for (int i = 0; i < controllerList.Count; i++)
        {
            if (controllerList[i].ControllMode)
                return controllerList[i].transform;
        }
        return null;
    }

    int num = 1;

    private void Test()
    {
        num++;
        if (num > 2)
            num = 0;
    }

    private void Update() // 테스트 코드
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ControllerChange(num);
            Test();
        }
    }

}
