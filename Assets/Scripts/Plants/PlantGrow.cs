using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrow : MonoBehaviour
{

    public GameObject soil;

    [HideInInspector]
    public int row, col;

    [HideInInspector]
    public int price; 
    protected GameModel model;
    protected Transform shadow;
    protected PlantSpriteDisplay display;
    protected void Awake()
    {
        model=GameModel.GetInstance();
        shadow = transform.Find("shadow");
        display = GetComponent<PlantSpriteDisplay>();
    }

    public virtual bool CanGrowInmap(int row, int col)
    {
        GameObject plant = model.map[row, col];       
        if (!plant)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
   
    public virtual void Grow(int _row, int _col)
    {
        row = _row;
        col = _col;
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
