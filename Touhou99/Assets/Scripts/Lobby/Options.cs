using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] Slider soundSlider;
    [SerializeField] Slider musicSlider;

    [SerializeField] AudioSource[] soundSources;
    [SerializeField] AudioSource[] musicSources;

    void Start()
    {
        SetDefaults();
    }

    void SetDefaults()
    {
        soundSlider.value = 0.3f;
        musicSlider.value = 0.5f;
    }

    void Update()
    {
        SetSoundsVolume();
        SetMusicVolume();
    }

    private void SetMusicVolume()
    {
        for (int i = 0; i < soundSources.Length; i++)
        {
            musicSources[i].volume = musicSlider.value;
        }
    }

    private void SetSoundsVolume()
    {
        for (int i = 0; i < soundSources.Length; i++)
        {
            soundSources[i].volume = soundSlider.value;
        }
    }
}
