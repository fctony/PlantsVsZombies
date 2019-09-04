using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{

    private SpriteRenderer renderer;
    private float time;
    private float curtime;

    void Awake()
    {
        enabled = false;
        renderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        curtime += Time.deltaTime;
        Color color = renderer.color;
        if (curtime<time)
        {
            color.a = 1 - curtime/time;
        }
        else
        {
            color.a = curtime/time - 1;
            if (curtime>time*2)
            {
                enabled = false;
            }
        }
        renderer.color = color;
    }

    public void Begin(float t)
    {
        enabled = true;
        time = t/2;
        curtime = 0f;
    }
}
