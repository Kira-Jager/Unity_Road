using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManger : MonoBehaviour
{
    public Canvas canvas;
    //public GameObject car;

    //public GameObject line;

    private void Start()
    {
        mainMenuCanvas();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Draw draw;
        //car collision
        if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("You win");
            nextLevel();
        }
    }


    private void mainMenuCanvas()
    {
        pauseGame();
        canvas.transform.GetChild(3).gameObject.SetActive(true);
        //isCanvasActive = true;
    }

    private void nextLevel()
    {
        pauseGame();
        canvas.transform.GetChild(1).gameObject.SetActive(true);
    }

    private void RelaodLevel()
    {

    }

    private void pauseGame()
    {
        Time.timeScale = 0;
        Debug.Log("Game paused");
    }
}
