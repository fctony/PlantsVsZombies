using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPause : MonoBehaviour
{

    public AudioClip pauseSound;
    public Text text;

    private AudioManager am;

    void Awake()
    {
        am=AudioManager.GetInstance();
    }

    public void OnClick()
    {
        if ("暂停游戏"==text.text)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        text.text = "继续游戏";
        am.PauseAllSounds();
        am.PlaySound(pauseSound);
        Time.timeScale = 0;
        am.PauseMusic();
    }

    public void ResumeGame()
    {
        text.text = "暂停游戏";
        Time.timeScale = 1;
        am.ResumeAllSounds();
        am.PlaySound(pauseSound);
        am.ResumeMusic();
    }
}
