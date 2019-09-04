using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public AudioClip attackSound;

    public int atk = 100;
    public float cd = 1f;
    public float range;
    protected Animator animator;
    protected GameModel model;
    protected ZombieMove move;
    protected AudioSource audioSource;

    protected GameObject target;
    void Awake()
    {
        animator = transform.Find("zombie").GetComponent<Animator>();
        model=GameModel.GetInstance();
        move = GetComponent<ZombieMove>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (target==null)
        {
            target = SearchPlant();
        }
        if (target&&move.enabled)
        {
            move.enabled = false;
            animator.SetBool("isAttacking",true);
            audioSource = AudioManager.GetInstance().PlaySound(attackSound, true);
            Invoke("DoAttack",cd);
        }
        else if (!target&&!move.enabled)
        {
            move.enabled = true;
            animator.SetBool("isAttacking",false);
            AudioManager.GetInstance().StopSound(audioSource);
            CancelInvoke("DoAttack");
        }

    }
   
    public void DoAttack()
    {
        if (target)
        {
            target.GetComponent<PlantHealthy>().Damage(atk);
        }
        Invoke("DoAttack", cd);
    }

    public void StopAttack()
    {
        AudioManager.GetInstance().StopSound(audioSource);
        enabled = false;
    }

    public GameObject SearchPlant()
    {
        GameObject target = null;
        float minDis = 100000;
        for (int col = 0; col < StageMap.COL_MAX; ++col)
        {
            GameObject plant = model.map[move.row, col];
            if (plant&&plant.GetComponent<PlantHealthy>())
            {
                float dis = transform.position.x - plant.transform.position.x;
                if (dis>=0&&dis<=range)
                {
                    if (minDis>dis)
                    {
                        minDis = dis;
                        target = plant;
                    }
                }
            }
        }
        return target;
    }
}
