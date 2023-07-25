using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Attached to the Play button on the main menu.
    // Note from Ben: IDK if this is the exact way Unity does things, seems to work,
    // this is basically what I would do in Godot so hopefully it's good enough. :)
    public void PlayGamePressed()
    {
        // True tells it to reset to level 1
        GameManager.Instance.LoadNextGameLevel(true);
    }
}
