using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public delegate void OnKeyPressedAction();
    public static event OnKeyPressedAction onKeyPressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void getKeyPressed(string key)
    {
        onKeyPressed?.Invoke();
        Debug.Log("Key Pressed = "+ key);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            getKeyPressed("W");
        }
    }
}
