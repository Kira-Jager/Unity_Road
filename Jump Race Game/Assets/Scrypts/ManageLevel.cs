using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManageLevel : MonoBehaviour
{
    
    public Canvas canvas;
    public void onClickOnStartButton()
    {
        resumeGame();
        canvas.transform.GetChild(3).gameObject.SetActive(false);
        jumpMove.isCanvasActive = false;
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
        jumpMove.isCanvasActive = false;

    }

    public void onClickOnContinuetButton()
    {
        resumeGame();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;

        if (currentSceneIndex < totalScenes - 1)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }

        jumpMove.isCanvasActive = false;
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
