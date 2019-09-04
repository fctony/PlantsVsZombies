using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchZombie : MonoBehaviour
{

    private GameModel model;

    void Awake()
    {
        model = GameModel.GetInstance();
    }

    public bool IsZombieInRange(int row, float min, float max)
    {
        foreach (GameObject zombie in model.zombieList[row])
        {
            float dis = zombie.transform.position.x - transform.position.x;
            if (min<=dis&&dis<=max)
            {
                return true;
            }
        }
        return false;
    }

    public GameObject SearchClosetZombie(int row, float min, float max)
    {
        float minDis = 10000f;
        GameObject closetZombie = null;
        foreach (GameObject zombie in model.zombieList[row])
        {
            float dis = zombie.transform.position.x - transform.position.x;
            if (min<=dis&&dis<=max&&Mathf.Abs(dis)<minDis)
            {
                minDis = Mathf.Abs(dis);
                closetZombie = zombie;
            }
        }
        return closetZombie;
    }


    public object[] SearchZombieInRange(int row, float min, float max)
    {
        ArrayList zombies=new ArrayList();
        foreach (GameObject zombie in model.zombieList[row])
        {
            float dis = zombie.transform.position.x - transform.position.x;
            if (min<=dis&&dis<=max)
            {
                zombies.Add(zombie);
            }
        }
        return zombies.ToArray();
    }
}
