using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset_Line_CarPos : MonoBehaviour
{
    private bool resetCarPos = false;
    public delegate void onClickAction();
    public static event onClickAction onClicked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(Time.time > 0)
        {
            Debug.Log("Clicked");
            resetCarPos = true;
            onClicked?.Invoke();
        }
        
    }

}
