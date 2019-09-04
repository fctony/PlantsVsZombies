using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour {

    public float time;
    private SpriteRenderer renderer;
    private float curTime = 0f;

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        enabled = false;
    }

    public void Begin()
    {
        enabled = true;
    }

    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime <= time)
        {
            renderer.color = new Color(1, 1, 1, 1-curTime / time);
        }
        else
        {
            Destroy(this);
        }
    }
}
