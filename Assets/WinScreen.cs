using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Attached to the 'back to menu' button on the WinScreen.
    public void BackToMenuPressed()
    {
        SceneTransitionCanvas.ChangeSceneStatic("Scenes/MainMenu");
    }
}