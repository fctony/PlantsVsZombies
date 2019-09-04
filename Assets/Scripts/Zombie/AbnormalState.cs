using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbnormalState : MonoBehaviour
{
    [HideInInspector]
    public float ratio = 1f;

    private ZombieSpriteDisplay display;
    private Animator animator;
    private int state;
    private float speedDownRatio;

    void Awake()
    {
        display = GetComponent<ZombieSpriteDisplay>();
        animator = transform.Find("zombie").GetComponent<Animator>();
        state = 0;
    }

    public void SpeedDown(float time, float val)
    {
        speedDownRatio = val;
        state = 1;
        UpdateAction();
        if (IsInvoking("RemoveSeedDown"))
        {
            CancelInvoke("RemoveSeedDown");
        }
        Invoke("RemoveSeedDown",time);
    }

    public void RemoveSeedDown()
    {
        state = 0;
        UpdateAction();
    }
    void UpdateAction()
    {
        float val;
        if (state==1)
        {
            val = speedDownRatio;
            display.SetColor(0.5f,0.5f,1f);
        }
        else
        {
            val = 1;
            display.SetColor(1f,1f,1f);
        }
        ratio = val;
        animator.speed = val;
    }
}
