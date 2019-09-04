using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelect : MonoBehaviour {

    public GameObject[] cards;
    public int maxCardNumber;

    private float xOffset = 1.1f, yOffset = 0.6f;
    private ArrayList selectedCards = new ArrayList();
    private ArrayList barCardlist = new ArrayList();
    private GameObject gameController;
    private GameObject carBar;

    void Awake()
    {
        gameController = GameObject.Find("GameController");
        carBar = GameObject.Find("Cards");

        Transform text = transform.Find("Text");
        text.GetComponent<MeshRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
        text.GetComponent<TextMesh>().text += "<color=yellow>" + maxCardNumber + "</color>";
    }

    void Start()
    {
        Transform container = transform.Find("CardContainer");
        for (int i = 0; i < cards.Length; i++)
        {
            float x = (i % 4) * xOffset;
            float y = -(i / 4) * yOffset;
            GameObject card = Instantiate(cards[i]);
            card.transform.parent = container;
            card.transform.localPosition = new Vector3(x, y, 0);
            card.GetComponent<Card>().enabled = false;
            card.tag = "SelectingCard";
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D collider = Physics2D.OverlapPoint(Utility.GetMouseWorldPos());
            if (collider!=null)
            {
                if (collider.gameObject.tag== "SelectingCard")
                {
                    GameObject card = collider.gameObject;
                    if (selectedCards.Contains(card))
                    {
                        selectedCards.Remove(card);
                        card.GetComponent<Card>().SetSprite(true);
                        UpdateCardBar();                    
                    }
                    else if (selectedCards.Count<maxCardNumber)
                    {
                        selectedCards.Add(card);
                        card.GetComponent<Card>().SetSprite(false);
                        UpdateCardBar();
                    }                            
                }
            }
        }
    }

    void UpdateCardBar()
    {
        RemoveAllBarCards();
        float xOff = -0.6f;
        for (int i = 0; i < selectedCards.Count; i++)
        {
            GameObject prefab = selectedCards[i] as GameObject;
            GameObject card = Instantiate(prefab);
            card.tag = "Card";
            card.transform.parent = carBar.transform;
            card.transform.localPosition = new Vector3(0, i * xOff, 0);
            barCardlist.Add(card);
        }
    }

    void RemoveAllBarCards()
    {
        object[] barCards = barCardlist.ToArray();
        foreach (GameObject card in barCards)
        {
            Destroy(card);barCardlist.Clear();
        }
    }

    public void Submit()
    {
        foreach (GameObject card in barCardlist)
        {
            card.GetComponent<Card>().enabled = true;
           
        }
        gameController.GetComponent<GameController>().AfterSelectCard();
    }

    public void Reset()
    {
        foreach (GameObject card in selectedCards)
        {
            card.GetComponent<Card>().SetSprite(true);

        }
        selectedCards.Clear();
        RemoveAllBarCards();
    }
}
