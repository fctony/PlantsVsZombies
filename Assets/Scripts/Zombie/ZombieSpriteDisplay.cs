using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpriteDisplay : MonoBehaviour,SpriteDisplay
{

    private SpriteRenderer shadow;
    private SpriteRenderer zombie;

    public int orderOffset = 2;

    void Awake()
    {
        shadow = transform.Find("shadow").GetComponent<SpriteRenderer>();
        zombie = transform.Find("zombie").GetComponent<SpriteRenderer>();
    }

    public void SetAlpha(float a)
    {
        Color color = zombie.color;
        color.a = a;
        shadow.color = color;
        zombie.color = color;
    }

    public void SetOrder(int order)
    {
        zombie.sortingOrder = order;
    }

    public void SetOrderByRow(int row)
    {
        zombie.sortingOrder = 1000*(row + 1) + orderOffset;
    }

    public void SetColor(float r, float g, float b)
    {
        Color color=new Color(r,g,b,zombie.color.a);
        zombie.color = color;
    }
}
