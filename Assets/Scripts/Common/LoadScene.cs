using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{

    public AudioClip bgMusic;
    public GameObject logo;
    public GameObject loadingLayer;
    public GameObject title;
    public GameObject loadBar;
    public Button button;
    public Text text;

    private LoadBar loadbarClass;

    private AsyncOperation async;

    void Awake()
    {
        Color color=Color.white;
        color.a = 0;
        logo.GetComponent<SpriteRenderer>().color = color;

        loadbarClass = loadBar.GetComponent<LoadBar>();
        button.enabled = false;

        loadingLayer.SetActive(false);
    }

    void Start()
    {
        StartCoroutine(WorkFlow());
        AudioManager.GetInstance().PlayMusic(bgMusic);
    }

    IEnumerator WorkFlow()
    {
        FadeIn fadeIn = logo.AddComponent<FadeIn>();
        fadeIn.time = 1f;
        fadeIn.Begin();
        yield return new WaitForSeconds(2f);

        FadeOut fadeOut = logo.AddComponent<FadeOut>();
        fadeOut.time = 1f;
        fadeOut.Begin();
        yield return new WaitForSeconds(1f);
        logo.SetActive(false);
        loadingLayer.SetActive(true);
        yield return new WaitForEndOfFrame();

        MoveBy move = title.AddComponent<MoveBy>();
        move.offset=new Vector3(0,-2f,0);
        move.time = 1f;
        move.Begin();
        yield return new WaitForSeconds(1f);

        async = SceneManager.LoadSceneAsync("MainScene");
        async.allowSceneActivation = false;
        yield return StartCoroutine(Loading());

        text.text = "开始游戏";
        button.enabled = true;
    }

    IEnumerator Loading()
    {
        float curProgress = 0f;
        while (curProgress<=1f)
        {
            float toProgress = async.progress/0.9f;
            while (curProgress<toProgress)
            {
                curProgress += 0.01f;
                loadbarClass.SetProgress(curProgress);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void OnBtnClick()
    {
        async.allowSceneActivation = true;
    }
}
