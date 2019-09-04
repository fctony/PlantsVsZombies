using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public bool musicOn = true;
    public bool soundOn = true;
    private float _musicVolume = 1f;
    private float _soundVolume = 1f;

    private GameObject obj;
    private AudioSource mainMusic;
    private ArrayList sounds=new ArrayList();

    public float musicVolume
    {
        get { return _musicVolume; }
        set
        {
            if (value!=_musicVolume)
            {
                _musicVolume = value;
                mainMusic.volume = value;
            }
        }
    }

    public float soundVolume
    {
        get { return _soundVolume; }
        set
        {
            if (_soundVolume!=value)
            {
                _soundVolume = value;
                foreach (AudioSource src in sounds)
                {
                    src.volume = value;
                }
            }
        }
    }



    public static AudioManager instance;

    public static AudioManager GetInstance()
    {
        if (instance==null)
        {
            GameObject obj=new GameObject("AudioManager");
            DontDestroyOnLoad(obj);
            instance = obj.AddComponent<AudioManager>();
            instance.obj = obj;
            instance.mainMusic = obj.AddComponent<AudioSource>();

        }
        return instance;
    }

    public void PlayMusic(AudioClip music)
    {
        PlayMusic(music,true);
    }

    public void PlayMusic(AudioClip music, bool loop)
    {
        mainMusic.Stop();
        mainMusic.clip = music;
        mainMusic.volume = musicVolume;
        mainMusic.loop = loop;
        if (musicOn&&Time.timeScale!=0)
        {
            mainMusic.Play();
        }
    }

    public void StopMusic()
    {
        mainMusic.Stop();
    }

    public void PauseMusic()
    {
        mainMusic.Pause();
    }

    public void ResumeMusic()
    {
        if (musicOn&&Time.timeScale!=0)
        {
            mainMusic.Play();
        }
    }

    public AudioSource PlaySound(AudioClip sound)
    {
        return PlaySound(sound, false);
    }

    public AudioSource PlaySound(AudioClip sound, bool loop)
    {
        AudioSource source = obj.AddComponent<AudioSource>();
        source.clip = sound;
        source.volume = soundVolume;
        source.loop = loop;
        sounds.Add(source);
        if (soundOn&&Time.timeScale!=0)
        {
            source.Play();
        }
        if (!loop)
        {
            StartCoroutine(DoDestroy(source, sound.length));
        }

        return source;
    }

    IEnumerator DoDestroy(AudioSource sound, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(sound);
        sounds.Remove(sound);
    }

    public void StopSound(AudioSource sound)
    {
        StartCoroutine(DoDestroy(sound, 0));
    }


    public void PauseAllSounds()
    {
        foreach (AudioSource src in sounds)
        {
            src.Pause();
        }
    }

    public void ResumeAllSounds()
    {
        if (soundOn&&Time.timeScale!=0)
        {
            foreach (AudioSource src in sounds)
            {
                src.Play();
            }
        }
    }

    public void Clear()
    {
        foreach (AudioSource src in sounds)
        {
           Destroy(src);
        }
        sounds.Clear();
    }
}
