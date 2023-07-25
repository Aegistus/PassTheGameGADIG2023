using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager
using UnityEngine.UI; // 'Image'

// Used to fade in and out of scenes.
// A very simple transition, but could be improved if you like.
// IDK if there's a better way to do this is Unity, but this will
// work reasonably well... just drop the prefab in every scene,
//
// Then to change scenes do:
// GameObject.FindWithTag("SceneTransitionCanvas").ChangeScene(...)
//
// Or just use the static methods SceneTransitionCanvas.ChangeSceneStatic(), etc,
// which will use the tag to find the game object for you.
public class SceneTransitionCanvas : MonoBehaviour
{
    string sceneToChangeTo = null;

    float timer = -1.0f;

    // If this is enabled, the canvas will fade from white to transparent as
    // soon as its script starts going.
    //
    // It should almost certainly always be enabled. If not, make it public.
    //
    // The very first time a SceneTransitionCanvas fades in, WILL BE
    // turned off, by the below static bool firstSceneStartCall flag.
    bool fadingIn = true;

    // Time for the transition animation to play.
    // Scenes will generally take 2 transition times to become playable --
    // One fade out, and one fade in.
    const float TRANSITION_TIME = 0.25f;

    // Marks the very first time
    static bool firstSceneStartCall = true;

    Image fadingImage;

    // Start is called before the first frame update
    void Start()
    {
        // Grab the image
        fadingImage = GetComponentInChildren<Image>();

        if(firstSceneStartCall)
        {
            // For the first ever fade-in, just disable the fade-in.
            fadingIn = false;
            // Then, every other Start() after this won't be the first, so we *should* fade-in.
            firstSceneStartCall = false;
        }

        if(fadingIn)
        {
            timer = TRANSITION_TIME;
            // Make sure the image is active.
            fadingImage.gameObject.SetActive(true);
        }
        else
        {
            // The only scene that shouldn't fade in is probably the main menu, when
            // it is first loaded. This is accomplished by the firstSceneStartCall
            //
            // We hide the image so that it isn't visible and Unity doesn't have to
            // bother rendering it, etc (note IDK if this matters)
            //
            // DO NOT deactivate the SceneTransitionCanvas, otherwise GameObject.FindWithTag
            // will not work
            fadingImage.gameObject.SetActive(false);
        }
        // In case the object was deactivated, make sure its active
        gameObject.SetActive(true);
    }

    void SetOpacity(float a)
    {
        Color c = fadingImage.color;
        c.a = a;
        fadingImage.color = c;
    }

    void TimerExpired()
    {
        if (fadingIn)
        {
            // Hide.
            fadingImage.gameObject.SetActive(false);
        }
        else
        {
            // If the timer is 0, and we're fading out, change to the new scene.
            SceneManager.LoadScene(sceneToChangeTo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Perform fading animation, if the timer is positive.
        // Otherwise we assume nothing is happening.
        if(timer > 0)
        {
            float t = timer / TRANSITION_TIME;
            // Opacity goes in a different direction for fading in & out
            float alpha = fadingIn ? t : 1.0f - t;

            SetOpacity(alpha);
            
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                // This must be called exactly once, when the timer
                // expires, in case we are loading a new scene.
                TimerExpired();
            }
        }
    }

    // Starts the Scene Transition animation and will cause the scene to change
    // shortly.
    public void ChangeScene(string scene)
    {
        // Don't let us change scenes if we're already swapping to a new scene.
        if (timer > 0 && !fadingIn)
        {
            return;
        }

        // Reset the image opacity to 0
        SetOpacity(0.0f);
        // Show the image so it can fade
        fadingImage.gameObject.SetActive(true);

        // Keep track of the scene to change to
        sceneToChangeTo = scene;

        // Must reset the timer
        timer = TRANSITION_TIME;

        fadingIn = false;
    }

    public static void ChangeSceneStatic(string scene)
    {
        // Note: this might be slower than e.g. having a singleton, or
        // doing a DontDestroyOnLoad, or something...
        // 
        // But it should be fine... it's only called *approximately* once per scene load.
        GameObject canvas = GameObject.FindWithTag("SceneTransitionCanvas");
        canvas.GetComponent<SceneTransitionCanvas>().ChangeScene(scene);
    }

    public static void ReloadSceneStatic()
    {
        string scene = SceneManager.GetActiveScene().path;
        ChangeSceneStatic(scene);
    }
}
