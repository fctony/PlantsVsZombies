using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantShoot : MonoBehaviour
{
    public GameObject[] bullets;
    public Vector3 bulletOffset;
    public float interval;
    public float cd;
    private float cdTime = 0;

    private PlantGrow grow;
    public float range;
    private SearchZombie search;
    void Awake()
    {
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
        if (cdTime>0)
        {
            cdTime -= Time.deltaTime;
        }
        else
        {
            bool hasZombie = search.IsZombieInRange(grow.row, 0, range);         
            if (hasZombie)
            {
                StartCoroutine(Shoot());
                cdTime = cd;
            }
           
        }
    }

    IEnumerator Shoot()
    {
        Vector3 pos = transform.position + bulletOffset;
        foreach (GameObject bullet in bullets)
        {
            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.position = pos;
            newBullet.GetComponent<Bullet>().row = grow.row;
            newBullet.GetComponent<SpriteRenderer>().sortingOrder = 1000*(grow.row + 1) + 1;
            yield return new WaitForSeconds(interval);
        }
    }
}
