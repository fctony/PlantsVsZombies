using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackPlantHealthy : PlantHealthy {
    public override void Damage(int value)
    {
        base.Damage(value);
        animator.SetInteger("hp",hp);
    }

    private void AfterGrow()
    {
    }
}
