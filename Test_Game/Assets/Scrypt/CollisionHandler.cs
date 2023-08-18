using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CollisionHandler : MonoBehaviour
{
    Rigidbody rb;
    MeshRenderer obj;
    public GameObject[] ground;
    float groundCount;
    float redCount = 0f;

    public Image image;
    public Image WinPanel;

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
                    WinPanel.enabled = true;
                    Debug.Log("WinPanel.enabled = " + WinPanel.enabled);
                }
            }
            obj.material.color = Color.red;

        }

    }


}
