using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    public AudioClip succes;
    public AudioClip crash;
    AudioSource audio;

    bool isTransitioning = false;

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) { return; }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                Debug.Log("You finished the level");
                StartSuccessSequence();
                isTransitioning = true;
                break;
            case "Fuel":
                Debug.Log("You got some fuel");
                break;
            case "Obstacle":
                Debug.Log("You Loose");
                StartCrashSequence();
                isTransitioning = true;
                break;
            case "OutOfLimit":
                Debug.Log("You are out of limit");
                break;
            default:
                StartCrashSequence();
                Debug.Log("YOU LOSE");
                break;
        }

       

    }
    
    void stopEverything()
    {
        audio = GetComponent<AudioSource>();
        audio.enabled = false;
        GetComponent<Mover>().enabled = false;
    }
 



    void StartSuccessSequence()
    {
        // todo add SFX upon crash
        // todo add particle effect upon crash
        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(succes, 1f);
        GetComponent<Mover>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);

        
    }

    void StartCrashSequence()
    {
        // todo add SFX upon crash
        // todo add particle effect upon crash
        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(crash, 1f);
        GetComponent<Mover>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
