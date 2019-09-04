using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    public GameObject[,] map;

    private static GameModel instance;      
    public static GameModel GetInstance()
    {
        if (instance==null)
        {
            instance=new GameModel();
        }
        return instance;
    }

    public ArrayList[] bulletList;
    public ArrayList[] zombieList;
    public int sun;

    private GameModel()
    {
        Clear();
    }

    public void Clear()
    {
        map=new GameObject[StageMap.ROW_MAX,StageMap.COL_MAX]; 
        bulletList=new ArrayList[StageMap.ROW_MAX];
        zombieList=new ArrayList[StageMap.ROW_MAX];
        for (int i = 0; i < StageMap.ROW_MAX; i++)
        {
            zombieList[i]=new ArrayList();
            bulletList[i]=new ArrayList();
        }     
    }

}
