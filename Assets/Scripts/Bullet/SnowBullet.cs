using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBullet : Bullet
{
    [Range(0f,1f)]
    public float downRatio;

    public float effectiveTime;

    protected override void HitEffect()
    {
        base.HitEffect();
        if (target)
        {
            target.GetComponent<AbnormalState>().SpeedDown(effectiveTime,downRatio);
        }
    }
}
