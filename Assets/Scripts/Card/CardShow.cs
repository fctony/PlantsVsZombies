using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShow : MonoBehaviour
{

    private Card card;
    private TextMesh cdText;

    void Awake()
    {
        card = GetComponent<Card>();
    }

    void Start()
    {
        int order = GetComponent<SpriteRenderer>().sortingOrder + 1;
        Transform priceText = transform.Find("price");
        priceText.GetComponent<MeshRenderer>().sortingOrder = order;
        priceText.GetComponent<TextMesh>().text = card.price.ToString();

        Transform cd = transform.Find("CD");
        cd.GetComponent<MeshRenderer>().sortingOrder = order;
        cdText = cd.GetComponent<TextMesh>();
    }

    void Update()
    {
        if (card.state==Card.State.CD&&card.isGrowed)
        {
            cdText.text = card.CdTime.ToString("f1") + "s";
        }
        else
        {
            cdText.text = "";
        }
    }
}
