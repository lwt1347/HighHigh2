using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour {

    private void Awake()
    {
        //해상도 600:1024 고정
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(1024, 600, true);
    }

    public void OnStartClick()
    {
        //Play 씬 작동
        SceneManager.LoadScene("Play");
    }

    public void OnExitClick()
    {
        Application.Quit();
    }

}
