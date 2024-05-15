using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public List<Controller> controllerList;

    public void Init()
    {
        ControllerListAdd();
    }

    private void ControllerListAdd()
    {
        if(transform.Find("Warrior"))
        {
            controllerList.Add(transform.Find("Warrior").GetComponent<Controller>());
        }
        /*if (transform.Find("Priest"))
        {
            controllerList.Add(transform.Find("Priest").GetComponentInChildren<Controller>());
        }
        if (transform.Find("Archer"))
        {
            controllerList.Add(transform.Find("Archer").GetComponentInChildren<Controller>());
        }*/

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

    public void ControllerChange(Controller who)
    {
        foreach(Controller controller in controllerList)
        {
            bool value = (controller == who);
            controller.ControllMode = value;
            controller.InitState(value);
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
}
