using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    Dictionary<string, AudioClip> clipsByName = new();

    static SoundManager main;

    void Start()
    {
        main = this;
        foreach (var clip in clips)
        {
            clipsByName[clip.name] = clip;
        }
        
        DontDestroyOnLoad(this.gameObject);
    }

    public static void PlayAtPosition(string soundName, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(main.clipsByName[soundName], position);
    }
    
    public static void PlayAtPosition(string soundName, Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(main.clipsByName[soundName], position, volume);
    }
}
