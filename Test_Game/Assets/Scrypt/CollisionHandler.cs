using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
    Rigidbody rb;
    MeshRenderer obj;
    public GameObject[] ground;
    float groundCount;
    float redCount = 0f;

    public Image image;
    public GameObject  WinPanel;

    int sceneIndex;
    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnCollisionEnter(Collision collision)
    {
        obj = collision.gameObject.GetComponent<MeshRenderer>();

        ground = GameObject.FindGameObjectsWithTag("ground");

        groundCount = ground.Length;
        Debug.Log("groundCount = " + groundCount);
        
        if (obj.gameObject.tag == "ground")
        {
            if(obj.material.color != Color.red)
            {
                redCount++;
                image.fillAmount =  redCount / groundCount;
                Debug.Log("redCount = " + redCount);
                Debug.Log("image.fillAmount " + image.fillAmount);

                if (image.fillAmount == 1)
                {
                    WinPanel.SetActive(true);
                    Debug.Log("WinPanel.enabled = " + true);

                    /*SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);*/
                    /*if(SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
                    {
                        SceneManager.LoadScene(0);
                    }
                    image.fillAmount = 0;*/
                }
            }
            obj.material.color = Color.red;

        }

    }


}
