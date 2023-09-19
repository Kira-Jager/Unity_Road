using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManageLevel : MonoBehaviour
{


    private void Start()
    {
        Debug.Log("I'm workiong");
    }
    public Canvas canvas;
    public void onClickOnStartButton()
    {
        //jumpMove.isCanvasActive = false;
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
        //jumpMove.isCanvasActive = false;
        resumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void onClickOnContinuetButton()
    {
        resumeGame();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;

        //jumpMove.isCanvasActive = false;

        if (currentSceneIndex < totalScenes - 1)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
            //jumpMove.isCanvasActive = false;
            resumeGame();
        }
        else
        {
            SceneManager.LoadScene(0);
            //jumpMove.isCanvasActive = false;
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
