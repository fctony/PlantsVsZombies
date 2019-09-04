using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerForShovel : MonoBehaviour
{

    public AudioClip shovelLift;
    public AudioClip shovelCancel;
    public GameObject shovelBG;

    private GameObject shovel;
    private GameObject selectedPlant;


    void Update()
    {
        HandleMouseMoveForShovel();
        HandleMouseDownForShovel();       
    }

    void HandleMouseDownForShovel()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (shovelBG.GetComponent<Collider2D>().OverlapPoint(Utility.GetMouseWorldPos()))
            {
                CancelSelectShovel();
                shovel = shovelBG.GetComponent<Shovel>().shovel.gameObject;
                shovelBG.SendMessage("OnSelect");
                AudioManager.GetInstance().PlaySound(shovelLift);
            }
            else if (shovel)
            {
                if (selectedPlant)
                {
                    selectedPlant.GetComponent<PlantHealthy>().Die();
                    selectedPlant = null;
                }
                else
                {
                    CancelSelectShovel();
                }

            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            CancelSelectShovel();
        }
    }

    void HandleMouseMoveForShovel()
    {
        if (shovel)
        {
            Vector3 pos = Utility.GetMouseWorldPos();
            Vector3 shovelPos = pos;
            shovelPos.x += 0.1f;
            shovelPos.y += 0.1f;
            shovel.transform.position = shovelPos;

            if (StageMap.IsPointInMap(pos))
            {
                int row, col;
                StageMap.GetRowAndCol(pos,out row,out col);
                GameObject plant = GameModel.GetInstance().map[row, col];
                if (selectedPlant!=plant)
                {
                    if (selectedPlant)
                    {
                        selectedPlant.GetComponent<SpriteDisplay>().SetAlpha(1f);
                    }
                    if (plant)
                    {
                        selectedPlant = plant;
                        selectedPlant.GetComponent<SpriteDisplay>().SetAlpha(0.6f);                        
                    }
                    else
                    {
                        selectedPlant = null;
                    }
                }
            }
        }
    }

    void CancelSelectShovel()
    {
        if (shovel)
        {
           shovelBG.GetComponent<Shovel>().CancelSelect();
            shovel = null;
            AudioManager.GetInstance().PlaySound(shovelCancel);
            if (selectedPlant)
            {
                selectedPlant.GetComponent<SpriteDisplay>().SetAlpha(1f);
                selectedPlant = null;
            }

        }
    }
}
