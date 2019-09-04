using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBar : MonoBehaviour {

    public GameObject rollCap;
    public GameObject grass;

    private const float endRotationZ = -360f;
    private const float endScale = 0.5f;
    private const float leftX = -1.5f;
    private const float rightX = 1.4f;
    private const float leftY = 0.5f;
    private const float rightY = 0.3f;
    private Material grassMeaterial;

    void Awake()
    {
        grassMeaterial = grass.GetComponent<SpriteRenderer>().material;
        rollCap.transform.localPosition = new Vector3(leftX, leftY,0);
    }

    public void SetProgress(float ratio)
    {
        ratio = Mathf.Clamp(ratio, 0f, 1f);
        grassMeaterial.SetFloat("_Progress", ratio);

        float x = Mathf.Lerp(leftX, rightX, ratio);
        float y = Mathf.Lerp(leftY, rightY, ratio);
        rollCap.transform.localPosition = new Vector3(x, y, 0);

        float roationZ = Mathf.Lerp(0, endRotationZ, ratio);
        rollCap.transform.localEulerAngles = new Vector3(0, 0, roationZ);

        float scale = Mathf.Lerp(1, endScale, ratio);
        rollCap.transform.localScale = new Vector3(scale, scale, 1);
    }
}
