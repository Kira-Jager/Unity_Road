using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset_Line_CarPos : MonoBehaviour
{
    private bool resetCarPos = false;
    public delegate void onClickAction();
    public static event onClickAction onClicked;

    private GameManager gameManager;



    public void Initialize(GameManager manager)
    {
        gameManager = manager;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found!");
            return;
        }

        Initialize(gameManager);

        GameManager.disableObject = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.disableObject == true)
        {
            disableObject();

        }
    }

    private void OnMouseDown()
    {
        if(Time.time != 0)
        {
            Debug.Log("Clicked");
            resetCarPos = true;
            onClicked?.Invoke();
        }

       
        
    }

    private void disableObject()
    {
        Debug.Log("Disable");
        transform.gameObject.SetActive(false);
    }
}
