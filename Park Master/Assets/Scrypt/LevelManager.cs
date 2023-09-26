using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{

    public Canvas canvas;

    private void Start()
    {
        mainMenuCanvas();
    }

    private void mainMenuCanvas()
    {
        pauseGame();
        canvas.transform.GetChild(3).gameObject.SetActive(true);
    }

    public void onClickOnStartButton()
    {
        Debug.Log("Game start");
        resumeGame();
        canvas.transform.GetChild(3).gameObject.SetActive(false);
    }
    public void onClickOnQuitButton()
    {
        pauseGame();
        Debug.Log("Quit");
        Application.Quit();
    }

    public void onClickOnRetryButton()
    {
        resumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void onClickOnContinuetButton()
    {
        resumeGame();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;


        if (currentSceneIndex < totalScenes - 1)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
            resumeGame();
        }
        else
        {
            SceneManager.LoadScene(0);
            resumeGame();
        }

    }


    private void pauseGame()
    {
          Time.timeScale = 0;
    }

    private void resumeGame()
    {
        Time.timeScale = 1;
    }
}