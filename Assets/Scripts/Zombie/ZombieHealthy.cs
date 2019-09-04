using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthy : MonoBehaviour
{
    public AudioClip damageSound;
    public GameObject head;
    public int hp;
    public int lostHeadHp = 40;
    public Vector3 headOffset;
    protected bool hasHead=true;
    protected Animator animator;
    protected Blink blink;
    protected void Awake()
    {
        animator = transform.Find("zombie").GetComponent<Animator>();
        blink = transform.Find("zombie").GetComponent<Blink>();
    }

    public virtual void Damage(int value)
    {
        if (hp<=0)
        {
            return;
        }
        AudioManager.GetInstance().PlaySound(damageSound);

        hp -= value;
        animator.SetInteger("hp",hp);
        blink.Begin(0.15f);
        if (hp<=lostHeadHp&&hasHead)
        {
            LostHead();
        }
        if (hp<=0)
        {
            Die();
        }
    }

    protected virtual void LostHead()
    {
        GameObject newHead = Instantiate(head);
        newHead.transform.position = transform.position+headOffset;
        Destroy(newHead,3f);
        hasHead = false;
    }

    protected void Die()
    {
        ZombieMove move = GetComponentInChildren<ZombieMove>();
        GameModel.GetInstance().zombieList[move.row].Remove(gameObject);
        move.enabled = false;    
        GetComponent<ZombieAttack>().StopAttack();
        Destroy(gameObject,3.0f);
    }

    public void BoomDie()
    {
        if (hp<=0)
        {
            return;
        }
        animator.SetTrigger("boomDie");
        Die();
    }

    public void DieByMine()
    {
        ZombieMove move = GetComponentInChildren<ZombieMove>();
        GameModel.GetInstance().zombieList[move.row].Remove(gameObject);
        move.enabled = false;
        GetComponent<ZombieAttack>().StopAttack();
        Destroy(gameObject);
    }
}
