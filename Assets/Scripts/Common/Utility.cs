using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{

    public static Vector3 GetMouseWorldPos()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }
}
