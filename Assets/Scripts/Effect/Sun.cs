using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Sun : MonoBehaviour
{
    public AudioClip sound;
    public Vector3 disappearPos;
    public int value = 25;
    public float absorbTime = 0.5f;
    public float disappearTime;

    private GameModel model;
    private Vector3 speed;
    private Vector3 scaleSpeed;

    void Awake()
    {
        model=GameModel.GetInstance();
        scaleSpeed=new Vector3(1f,1f,1f)/absorbTime;

        Destroy(gameObject,disappearTime);
        enabled = false;
    }

    void Update()
    {
        transform.Translate(speed*Time.deltaTime);
        transform.localScale = transform.localScale - scaleSpeed*Time.deltaTime;
    }

    void OnMouseDown()
    {
        model.sun += value;
        MoveBy move = GetComponent<MoveBy>();
        if (move)
        {
            move.enabled = false;
        }

        enabled = true;

        speed = (disappearPos - transform.position)/absorbTime;
        AudioManager.GetInstance().PlaySound(sound);
        Destroy(gameObject,absorbTime);
    }
}
