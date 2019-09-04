using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class MoveBy : MonoBehaviour
{

    public Vector3 offset;
    public float time;

    private Vector3 speed;
    private float curTime = 0;

    void Awake()
    {
        enabled = false;
    }

    public void Begin()
    {
        enabled = true;
        speed = offset/time;
    }
	
	// Update is called once per frame
	void Update () {
	    if (curTime<time)
	    {
	        curTime += Time.deltaTime;
            transform.Translate(speed*Time.deltaTime);
	    }
	    else
	    {
	        Destroy(this);
	    }
	}
}
