using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateMenu : MonoBehaviour {

    public void RestartStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackHome()
    {
        SceneManager.LoadScene("MainScene");
    }
}
