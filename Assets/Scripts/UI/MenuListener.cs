using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuListener : MonoBehaviour {

    public void OnQuitClick()
    {
        Application.Quit();
    }

    public void OnStageClick(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
