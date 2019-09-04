using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBy : MonoBehaviour
{

    public Vector3 offset;
    public float height;
    public float time;
    private Vector3 oriPos;
    private Vector3 offsetSpeed;
    private float curTime;

    void Awake()
    {
        enabled = false;
    }

    public void Begin()
    {
        offsetSpeed = offset/time;
        oriPos = transform.position;
        enabled = true;
    }

    void Update()
    {
        if (curTime<time)
        {
            curTime += Time.deltaTime;
            Vector3 newPos = oriPos + offsetSpeed*curTime;
            newPos.y += Mathf.Cos(curTime/time*Mathf.PI - Mathf.PI/2)*height;
            transform.position = newPos;
        }
        else
        {
            Destroy(this);
        }
    }
}
