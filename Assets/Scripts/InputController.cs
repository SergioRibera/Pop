using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController
{
    public static Vector3 MousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public static Ray RayToObj(Vector3 pos)
    {
        return Camera.main.ScreenPointToRay(pos);
    }
}
