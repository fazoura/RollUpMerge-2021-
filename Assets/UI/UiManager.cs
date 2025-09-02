using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{

    public static UiManager instance;

    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject restartButton;
    public GameObject ContinueButton;

    public GameObject editPanel;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void StartGame()
    {
       
        GameManager.instance.StartGame();
    }

    public void WinPanel()
    {
        winPanel.SetActive(true);
        Invoke("ShowContinueButton", 1.5f);
    }

    public void LosePanel()
    {
        losePanel.SetActive(true);
        Invoke("ShowRestartButton", 1.5f);
    }

    private void ShowContinueButton()
    {
        ContinueButton.SetActive(true);
    }

    private void ShowRestartButton()
    {
        restartButton.SetActive(true);
    }

    public void Restart()
    {
        GameManager.instance.Restart();

    }

    public void NextLevel()
    {
        GameManager.instance.NextLevel();
    }
}
