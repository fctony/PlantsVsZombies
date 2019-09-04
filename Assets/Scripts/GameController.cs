using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public enum ZombieType
{
    Zombie1,   
    ConeHeadZombie,
    BucketHeadZombie,
}

[Serializable]
public struct Wave
{
    [Serializable]
    public struct Data
    {
        public ZombieType zombieType;
        public uint count;
    }

    public bool isLargeWave;

    [Range(0f,1f)]
    public float percentage;
    public Data[] zombieData;
}


public class GameController : MonoBehaviour
{

    public GameObject zombie1;
    public GameObject BucketheadZombie;
    public GameObject ConeheadZombie;
    private GameModel model;
    public GameObject progressBar;
    public GameObject gameLabel;
    public GameObject sunPrefab;
    public GameObject cardDialog;
    public GameObject sunLabel;
    public GameObject shovelBG;
    public GameObject btnSubmitObj;
    public GameObject btnResetObj;

    public string nextStage;

    public float readyTime;
    public float elapsedTime;
    public float playTime;
    public float sunInterval;
    public AudioClip readySound;
    public AudioClip zombieComing;
    public AudioClip hugeWaveSound;
    public AudioClip finalWaveSound;
    public AudioClip loseMusic;
    public AudioClip winMusic;
    
    public Wave[] waves;

    public int initSun;
    private bool isLostGame = false;

    void Awake()
    {
        model = GameModel.GetInstance();
    }
	void Start ()
	{
        model.Clear();
     
	    model.sun = initSun;
        ArrayList flags=new ArrayList();
	    for (int i = 0; i < waves.Length; i++)
	    {
	        if (waves[i].isLargeWave)
	        {
	            flags.Add(waves[i].percentage);
	        }
	    }
        progressBar.GetComponent<ProgressBar>().InitWithFlag((float[])flags.ToArray(typeof(float)));
	    progressBar.SetActive(false);
        cardDialog.SetActive(false);
        sunLabel.SetActive(false);
        shovelBG.SetActive(false);
        btnResetObj.SetActive(false);
        btnSubmitObj.SetActive(false);
	    GetComponent<HandlerForShovel>().enabled = false;
	    GetComponent<HandlerForPlants>().enabled = false;
        StartCoroutine(GameReady());

    }

   

    Vector3 origin
    {
        get
        {
            return new Vector3(-2f,-2.6f);
        }
    }

    void OnDrawGizmos()
    {
              // DeBugDrawGrid(origin,0.8f,1f,9,5,Color.blue);
    }

    void DeBugDrawGrid(Vector3 _orgin,float x,float y,int col,int row,Color color)
    {
        for (int i = 0; i < col+1; i++)
        {
            Vector3 startPoint = _orgin + Vector3.right*i*x;
            Vector3 endPoint = startPoint + Vector3.up*row*y;
            Debug.DrawLine(startPoint,endPoint,color);
        }
        for (int i = 0; i < row+1; i++)
        {
            Vector3 startPoint = _orgin + Vector3.up * i * y;
            Vector3 endPoint = startPoint + Vector3.right * col * x;
            Debug.DrawLine(startPoint, endPoint, color);
        }
    }



    public void AfterSelectCard()
    {
        btnResetObj.SetActive(false);
        btnSubmitObj.SetActive(false);
        Destroy(cardDialog);
        GetComponent<HandlerForShovel>().enabled = true;
        GetComponent<HandlerForPlants>().enabled = true;
        Camera.main.transform.position=new Vector3(1.1f,0,-1f);
        StartCoroutine(WorkFlow());
        InvokeRepeating("ProduceSun", sunInterval, sunInterval);
    }


    IEnumerator GameReady()
    {
        yield return new WaitForSeconds(0.5f);
        MoveBy move = Camera.main.gameObject.AddComponent<MoveBy>();
        move.offset=new Vector3(3.55f,0,0);
        move.time = 1f;
        move.Begin();
        yield return  new WaitForSeconds(1.5f);
        sunLabel.SetActive(true);
        shovelBG.SetActive(true);
        cardDialog.SetActive(true);
        btnResetObj.SetActive(true);
        btnSubmitObj.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            model.sun += 50;
        }
        if (!isLostGame)
        {
            for (int row = 0; row < model.zombieList.Length; row++)
            {
                foreach (GameObject zombie in model.zombieList[row])
                {
                    if (zombie.transform.position.x<(StageMap.GRID_LEFT-0.4f))
                    {
                        LoseGame();
                        isLostGame = true;
                        return;
                    }
                    
                }
            }  
        }

    }

    IEnumerator WorkFlow()
    {
        gameLabel.GetComponent<GameTips>().ShowStartTip();
        AudioManager.GetInstance().PlaySound(readySound);
        yield return new WaitForSeconds(readyTime);
        ShowProgressBar();
        AudioManager.GetInstance().PlaySound(zombieComing);

        for (int i = 0; i < waves.Length; i++)
        {
            yield return StartCoroutine(WaitForWavePercentage(waves[i].percentage));
            if (waves[i].isLargeWave)
            {
                StopCoroutine(UpdateProgress());
                yield return StartCoroutine(WaitForZombieClear());
                yield return new WaitForSeconds(3.0f);
                gameLabel.GetComponent<GameTips>().ShowApproachingTip();
                AudioManager.GetInstance().PlaySound(hugeWaveSound);
                yield return new WaitForSeconds(3.0f);
                StartCoroutine(UpdateProgress());
            }
            if (i+1==waves.Length)
            {
                gameLabel.GetComponent<GameTips>().ShowFinalTip();
                AudioManager.GetInstance().PlaySound(finalWaveSound);
            }

            yield return StartCoroutine(WaitForZombieClear());
            CreatZombies(ref waves[i]);
        }

        yield return StartCoroutine(WaitForZombieClear());
        yield return new WaitForSeconds(2f);
        WinGame();

    }

    IEnumerator WaitForZombieClear()
    {
        while (true)
        {
            bool hasZombie = false;
            for (int row = 0; row < StageMap.ROW_MAX; row++)
            {
                if (model.zombieList[row].Count!=0)
                {
                    hasZombie = true;
                    break;
                }
            }
            if (hasZombie)
            {
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                break;
            }
        }
    }

    IEnumerator WaitForWavePercentage(float percentage)
    {
        while (true)
        {
            if ((elapsedTime/playTime)>=percentage)
            {
                break;
            }
            else
            {
                yield return 0;
            }
        }
    }

    IEnumerator UpdateProgress()
    {
        while (true)
        {
            elapsedTime += Time.deltaTime;  
            progressBar.GetComponent<ProgressBar>().SetProgress(elapsedTime/playTime);          
            yield return 0;
        }
    }

    void ShowProgressBar()
    {
        progressBar.SetActive(true);
        StartCoroutine(UpdateProgress());
    }
   

    void CreatZombies(ref Wave wave)
    {
        foreach (Wave.Data data in wave.zombieData)
        {
            for (int i = 0; i < data.count; i++)
            {
                CreatOneZombie(data.zombieType);
            }
        }
    }

    void CreatOneZombie(ZombieType type)
    {

        GameObject zombie=null;

        switch (type)
        {
            case ZombieType.Zombie1:
                zombie = Instantiate(zombie1);
                break;            
            case ZombieType.ConeHeadZombie:
                zombie = Instantiate(ConeheadZombie);
                break;
            case ZombieType.BucketHeadZombie:
                zombie = Instantiate(BucketheadZombie);
                break;               
        }

        
        int row = Random.Range(0, StageMap.ROW_MAX);      
        zombie.transform.position = StageMap.SetZombiePos(row);
        zombie.GetComponent<ZombieMove>().row = row;
        zombie.GetComponent<SpriteDisplay>().SetOrderByRow(row);
        model.zombieList[row].Add(zombie);
    }

    void ProduceSun()
    {
        float x = Random.Range(StageMap.GRID_LEFT, StageMap.GRID_RIGTH);
        float y = Random.Range(StageMap.GRID_BOTTOM, StageMap.GRID_TOP);
        float startY = StageMap.GRID_TOP + 1.5f;
        GameObject sun = Instantiate(sunPrefab);
        sun.transform.position=new Vector3(x,startY,0);
        MoveBy move = sun.AddComponent<MoveBy>();
        move.offset=new Vector3(0,y-startY,0);
        move.time = (startY - y)/1.0f;
        move.Begin();
    }

    void LoseGame()
    {
        gameLabel.GetComponent<GameTips>().ShowLostTip();
        GetComponent<HandlerForPlants>().enabled = false;
        CancelInvoke("ProduceSun");
        AudioManager.GetInstance().PlayMusic(loseMusic,false);
    }

    void WinGame()
    {
        CancelInvoke("ProduceSun");
        AudioManager.GetInstance().PlayMusic(winMusic, false);
        Invoke("GotoNextStage",3.0f);
    }

    void GotoNextStage()
    {       
        SceneManager.LoadScene(nextStage);
    }
}
