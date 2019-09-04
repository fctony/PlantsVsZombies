using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawnMower : MonoBehaviour
{

    public AudioClip sound;
    public int row;
    public float speed;
    private SearchZombie search;
    private bool start = false;
    private float range = 0.2f;

    void Awake()
    {
        search = GetComponent<SearchZombie>();
    }

    void Update()
    {
        if (start)
        {
            transform.Translate(Time.deltaTime*speed,0,0);
            object[] zombies = search.SearchZombieInRange(row, 0, range);
            foreach (GameObject zombie in zombies)
            {
                zombie.GetComponent<ZombieHealthy>().Damage(10000);
            }
            if (transform.position.x>(StageMap.GRID_RIGTH+0.5f))
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (search.IsZombieInRange(row,0,range))
            {
                start = true;
                AudioManager.GetInstance().PlaySound(sound);
            }
        }
    }
}
