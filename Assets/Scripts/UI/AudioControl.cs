using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{

    public Toggle musicToggle;
    public Toggle soundToggle;

    public Slider musicSlider;
    public Slider soundSlider;

    private AudioManager am;

    void Awake()
    {
        am=AudioManager.GetInstance();
    }

    void Start()
    {
        am.Clear();
        am.musicOn = musicToggle.isOn;
        am.soundOn = soundToggle.isOn;
        am.musicVolume = musicSlider.value;
        am.soundVolume = soundSlider.value;
    }

    public void OnMusicChanged()
    {
        am.musicOn = musicToggle.isOn;
        if (musicToggle.isOn)
        {
            am.ResumeMusic();
        }
        else
        {
            am.PauseMusic();
        }
    }

    public void OnSoundChanged()
    {
        am.soundOn = soundToggle.isOn;
        if (soundToggle.isOn)
        {
            am.ResumeAllSounds();
        }
        else
        {
            am.PauseAllSounds();
        }
    }

    public void OnMusicSliderValueChange()
    {
        am.musicVolume = musicSlider.value;
    }

    public void OnSoundSliderValueChange()
    {
        am.soundVolume = soundSlider.value;
    }
}
