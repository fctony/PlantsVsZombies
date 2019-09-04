using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSun : MonoBehaviour
{

    public GameObject sun;
    public int sunCount;
    public float produceCD;
    private float cdTime;

    void Awake()
    {
        cdTime = produceCD/4;
        enabled = false;
    }

    void AfterGrow()
    {
        enabled = true;
    }

    void Update()
    {
        if (cdTime>=0)
        {
            cdTime -= Time.deltaTime;
        }
        else
        {
            cdTime = produceCD;
            ProduceSun();
        }
    }

    void ProduceSun()
    {
        for (int i = 0; i < sunCount; i++)
        {
            GameObject newsun = Instantiate(sun);
            newsun.GetComponent<SpriteRenderer>().sortingOrder = 10000;
            newsun.transform.position = transform.position;

            float dis = StageMap.GRID_WIDTH;
            Vector3 offset=new Vector3(Random.Range(-dis,dis),Random.Range(-dis,dis),0);
            JumpBy jump = newsun.AddComponent<JumpBy>();
            jump.offset = offset;
            jump.height = Random.Range(0.3f, 0.6f);
            jump.time = Random.Range(0.4f, 0.6f);
            jump.Begin();
        }
    }
}
