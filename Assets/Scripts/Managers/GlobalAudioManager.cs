using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        PlayMusic(this, "main_theme");
    }

    public void PlayMusic(Component sender, object data)
    {
        if (data is not string)
        {
            Debug.LogError("Music name not string");
            return;
        }

        string musicName = (string)data;

        Sound sound = Array.Find(musicSounds, x => x.soundName == musicName);

        if (sound == null)
            Debug.Log("Sound Not Found");
        else
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(Component sender, object data)
    {
        if (data is not string)
        {
            Debug.LogError("Music name not string");
            return;
        }

        string sfxName = (string)data;

        Sound sound = Array.Find(sfxSounds, x => x.soundName == sfxName);

        if (sound == null)
            Debug.Log("Sound Not Found");
        else
        {
            sfxSource.PlayOneShot(sound.clip);
        }
    }
}
