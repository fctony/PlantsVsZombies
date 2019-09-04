using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{

    public Transform shovel;
    private Vector3 oriPos;
    private Quaternion oriRot;

    void Awake()
    {
        oriPos = shovel.position;
        oriRot = shovel.rotation;
    }

    void OnSelect()
    {
        shovel.Rotate(0,0,45f);
    }

    public void CancelSelect()
    {
        shovel.position = oriPos;
        shovel.rotation = oriRot;
    }
}
