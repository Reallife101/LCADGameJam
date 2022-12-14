using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pauseMenu : MonoBehaviour
{
    public GameObject pause;
    public GameObject levelSelect;
    public stopwa watch;
    public TMP_Text tm;

    private void Start()
    {
        watch.Begin();
    }

    public void togglePause()
    {
        if (pause.activeInHierarchy)
        {
            pause.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            showPause();
            Time.timeScale = 0f;
            tm.text = "Time: " + (watch.GetMinutes()).ToString("D2") + ":" + (watch.GetSeconds()).ToString("D2");
        }
    }

    public void showLevelSelect()
    {
        levelSelect.SetActive(true);
        pause.SetActive(false);
    }

    public void showPause()
    {
        levelSelect.SetActive(false);
        pause.SetActive(true);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            togglePause();
        }
    }
}
