using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTips : MonoBehaviour {

	public Sprite[] startLabels=new Sprite[3];
    public Sprite approachingLabel;
    public Sprite lostLabel;
    public Sprite finalLabel;

    private SpriteRenderer renderer;

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false;
    }

    public void ShowStartTip()
    {
        StartCoroutine(DoShowStart());
    }

    IEnumerator DoShowStart()
    {
        renderer.enabled = true;
        for (int i = 0; i < startLabels.Length; i++)
        {
            renderer.sprite = startLabels[i];
            yield return new WaitForSeconds(0.6f);
        }
        yield return new WaitForSeconds(0.6f);
        renderer.enabled = false;
    }


    public void ShowApproachingTip()
    {
        StartCoroutine(DoShowLabel(approachingLabel, 3f));
    }

    public void ShowFinalTip()
    {
        StartCoroutine(DoShowLabel(finalLabel, 3f));
    }

    public void ShowLostTip()
    {
        renderer.enabled = true;
        renderer.sprite = lostLabel;
    }

    IEnumerator DoShowLabel(Sprite label, float time)
    {
        renderer.enabled = true;
        renderer.sprite = label;
        yield return new WaitForSeconds(time);
        renderer.enabled = false;
    }
}
