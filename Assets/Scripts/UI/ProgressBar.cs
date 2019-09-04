using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{

    public GameObject flagPrefab;
    public const float leftX = -0.69f;
    public const float rightX = 0.69f;
    private Material FullMaterial;
    private GameObject head;

    void Awake()
    {
        FullMaterial = transform.Find("full").GetComponent<SpriteRenderer>().material;
        head = transform.Find("head").gameObject;
    }

    public void InitWithFlag(float[] perentage)
    {
        for (int i = 0; i < perentage.Length; i++)
        {
            GameObject flag = Instantiate(flagPrefab);
            flag.transform.parent = transform;
            float val = Mathf.Clamp(perentage[i], 0f, 1f);
            float x = Mathf.Lerp(rightX, leftX, val);
            flag.transform.localPosition=new Vector3(x,0.06f,0);
        }
    }

    public void SetProgress(float ratio)
    {
        ratio = Mathf.Clamp(ratio, 0f, 1f);
        FullMaterial.SetFloat("_Progress",ratio);
        float x = Mathf.Lerp(rightX, leftX, ratio);
        head.transform.localPosition=new Vector3(x,0,0);
    }
}
