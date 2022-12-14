using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public GameObject main;
    public GameObject credits;
    public GameObject howToPlay;

    private void Start()
    {
        showMain();
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void showCredits()
    {
        credits.SetActive(true);
        main.SetActive(false);
        howToPlay.SetActive(false);
    }
    public void showMain()
    {
        credits.SetActive(false);
        main.SetActive(true);
        howToPlay.SetActive(false);
    }

    public void showHowTo()
    {
        credits.SetActive(false);
        main.SetActive(false);
        howToPlay.SetActive(true);
    }
}
