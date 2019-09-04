using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunLabel : MonoBehaviour
{

    private GameObject text;
    private GameModel model;
	void Awake ()
	{
	    text = transform.Find("Text").gameObject;
	    text.GetComponent<MeshRenderer>().sortingOrder = 10001;
        model=GameModel.GetInstance();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    text.GetComponent<TextMesh>().text = model.sun.ToString();
	}
}
