using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove : MonoBehaviour
{

    public float speed = 0.1f;

    [HideInInspector]
    public int row;

    protected AbnormalState state;

    protected void Awake()
    {
        state = GetComponent<AbnormalState>();
    }
	void Update () {
		transform.Translate(-speed*Time.deltaTime*state.ratio,0,0);
	}
}
