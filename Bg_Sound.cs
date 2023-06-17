using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bg_Sound : MonoBehaviour
{
    #region GlobalCrap
    //For making this a global Script
    public static Bg_Sound Instance;
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion
    public AudioClip mainSong;
    public AudioClip intenseSong;

    private AudioSource source; //Audio Source
    [HideInInspector] public float volume;
    AudioClip currentSong;

    public void Start()
    {
        source = GetComponent<AudioSource>();

        //This is the volume is setted up in the SEttings
        if (PlayerPrefs.HasKey("volume"))
        {
            volume = PlayerPrefs.GetFloat("volume");
        }
        else
        {
            volume = 1;
        }


        StartMusic(mainSong);

    }
    public void StopMusic()
    {
        source.volume = 0.7f;
    }
    public void UpdateVolume()
    {
        volume = PlayerPrefs.GetFloat("volume");
    }
    
    public void StartMusic(AudioClip song)
    {
        if (song == currentSong)
            return;

        source.clip = song;
        currentSong = song;
        source.volume = volume;
    }
}
