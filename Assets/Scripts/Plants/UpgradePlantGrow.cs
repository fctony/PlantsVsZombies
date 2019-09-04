using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePlantGrow : PlantGrow
{

    public string targetPlantTag;

    public override bool CanGrowInmap(int row, int col)
    {
        GameObject plant = model.map[row, col];
        if (plant)
        {
            if (plant.tag==targetPlantTag)
            {
                return true;
            }
        }
        return false;
    }

    public override void Grow(int _row, int _col)
    {
        row = _row;
        col = _col;
        Destroy(model.map[row,col]);
        model.map[row, col] = gameObject;
        display.SetOrderByRow(row);

        if (shadow)
        {
            shadow.gameObject.SetActive(true);
        }
        if (soil)
        {
            GameObject temp = Instantiate(soil);
            temp.transform.position = transform.position;
            Destroy(temp,0.2f);
        }
        gameObject.SendMessage("AfterGrow");
    }
}
