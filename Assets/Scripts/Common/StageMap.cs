using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMap : MonoBehaviour
{

    public const int ROW_MAX = 5;
    public const int COL_MAX = 9;

    public const float GRID_TOP = 2.3f;
    public const float GRID_LEFT = -2f;
    public const float GRID_BOTTOM = -2.6f;
    public const float GRID_RIGTH = 5.2f;

    public const float GRID_WIDTH = 0.8f;
    public const float GRID_HEIGHT = 1f;

    public static Vector3 SetPlantPos(int row, int col)
    {
        return new Vector3(GRID_LEFT+0.5f+col*GRID_WIDTH,GRID_TOP-0.7f-row*GRID_HEIGHT,0);
    }

    public static Vector3 SetZombiePos(int row)
    {
        float offset = Random.Range(1.0f, 2.0f);
        return new Vector3(GRID_RIGTH+offset,GRID_TOP-0.7f-row*GRID_HEIGHT,0);
    }

    public static bool IsPointInMap(Vector3 point)
    {
        return point.x <= GRID_RIGTH && point.x >= GRID_LEFT && point.y <= GRID_TOP && point.y >= GRID_BOTTOM;
    }

    public static void GetRowAndCol(Vector3 point, out int row, out int col)
    {
        col = Mathf.FloorToInt(((point.x - GRID_LEFT) / GRID_WIDTH));
        row = Mathf.FloorToInt((GRID_TOP - point.y)/GRID_HEIGHT);

    }
}
