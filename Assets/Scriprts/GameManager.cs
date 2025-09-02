using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public Follower follower;
    public Velocity velocity;

    public Transform Stairs;
    public Transform startStairs;

    public float movementOffset;

    public bool isStoped;

    public float trajectoryWidth;

    private void Awake()
    {
        instance = this;
        isStoped = true;
        follower = FindObjectOfType<Follower>();
        velocity = FindObjectOfType<Velocity>();
    }


    public void StartGame()
    {
        isStoped = false;
        follower.Move();
    }

    public void Win()
    {
        print("You win");
        UiManager.instance.WinPanel();
        Stop();
    }

    public void Lose()
    {
        print("Failed!");
        UiManager.instance.LosePanel();
        Stop();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene(0);
    }

    public void ChangeCameraTarget(Transform newTarget)
    {
        Camera.main.GetComponent<CameraController>().ChangeTarget(newTarget);
    }

    public void Stop()
    {
        isStoped = true;
        follower.Stop();
    }

}
