using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed;
    public float range;
    public int atk;
    public GameObject effect;
    protected int _row;
    protected GameModel model;
    protected SearchZombie search;

    protected GameObject target;

    
    public int row
    {
        get { return _row; }
        set
        {
            _row = value;
            if (_row>=0&&_row<StageMap.ROW_MAX)
            {
                model.bulletList[_row].Add(gameObject);
            }
        }
    }

    void Awake()
    {
        search = GetComponent<SearchZombie>();
        model=GameModel.GetInstance();
        _row = -1;
    }

    void Update()
    {
        transform.Translate(speed*Time.deltaTime,0,0);
        if (_row<0||StageMap.ROW_MAX<=_row)
        {
            target = null;
        }
        target = search.SearchClosetZombie(_row, 0, range);
        if (target)
        {
            target.GetComponent<ZombieHealthy>().Damage(atk);
            HitEffect();
        }
    }

    protected virtual void HitEffect()
    {
        if (effect)
        {
            GameObject newEffect = Instantiate(effect);
            newEffect.transform.position = transform.position;
            Destroy(newEffect,0.2f);
        }
        if (target)
        {
            target.GetComponent<AbnormalState>().RemoveSeedDown();
        }   
        DoDestroy();
    }

    public void DoDestroy()
    {
        if (_row>=0&&_row<StageMap.ROW_MAX)
        {
            model.bulletList[row].Remove(gameObject);
        }
        Destroy(gameObject);
    }
}
