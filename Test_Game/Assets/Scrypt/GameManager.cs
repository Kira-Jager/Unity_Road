using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;  


public class GameManager : MonoBehaviour
{
    int sceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;   
    }

    // Update is called once per frame
    void Update()
    {
        gameLevelManager();
    }

    void gameLevelManager()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Next")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
                {
                    SceneManager.LoadScene(0);
                }
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Retry")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Quit")
            {
                Application.Quit();
            }
        }

    }
}
