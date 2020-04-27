using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController _instance;
    AudioSource source;
    public AudioClip chillMusic;
    public AudioClip intenseMusic;
    // Start is called before the first frame update
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            _instance = null;
        }
        source = GetComponent<AudioSource>();
    }
    public void PlayChillMusic()
    {
        source.clip = chillMusic;
        source.Play();


    }

    public void PlayIntensemusic()
    {
        source.clip = intenseMusic;
        source.Play();
    }
}
