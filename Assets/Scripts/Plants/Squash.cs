using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squash : MonoBehaviour
{

    public AudioClip findZombie;
    public float range;
    public float actionTime;
    public float destroyTime;

    private Animator animator;
    private PlantGrow grow;
    private SearchZombie search;
    private GameObject target;

    void Awake()
    {
        animator = transform.Find("plant").GetComponent<Animator>();
        grow = GetComponent<PlantGrow>();
        search = GetComponent<SearchZombie>();
        enabled = false;
    }

    void AfterGrow()
    {
        enabled = true;
    }

    void Update()
    {
        target = search.SearchClosetZombie(grow.row, -range, range);
        if (target)
        {
            Vector3 newPos = transform.position;
            newPos.x = target.transform.position.x;
            transform.position = newPos;
            animator.SetTrigger("attack");
            AudioManager.GetInstance().PlaySound(findZombie);
            StartCoroutine(DoDie());
            enabled = false;
        }
    }

    IEnumerator DoDie()
    {
        yield return new WaitForSeconds(actionTime);
        if (target)
        {
            target.GetComponent<ZombieHealthy>().DieByMine();
        }
        yield return new WaitForSeconds(destroyTime);
        GetComponent<PlantHealthy>().Die();
    }
}
