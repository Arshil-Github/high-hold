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
    public AudioSource source;
    public float volume;

    public void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("volume"))
        {
            volume = PlayerPrefs.GetFloat("volume");
        }
        else
        {
            volume = 1;
        }
        ResumeMusic();

    }
    public void StopMusic()
    {
        source.volume = 0.7f;
    }
    public void UpdateVolume()
    {
        volume = PlayerPrefs.GetFloat("volume");
    }
    public void ResumeMusic()
    {
        source.volume = volume;
    }
}
