using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Options : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] Slider soundSlider;
    [SerializeField] Slider musicSlider;

    [Header("Audio Sources")]
    [SerializeField] AudioSource[] soundSources;
    [SerializeField] AudioSource musicSource;

    [Header("Texts")]
    [SerializeField] TextMeshProUGUI soundText;
    [SerializeField] TextMeshProUGUI musicText;

    void Awake()
    {
        SetDefaults();
    }

    void SetDefaults()
    {
        soundSlider.value = 0.1f;
        musicSlider.value = 0.2f;
        soundText.text = "1";
    }

    void Update()
    {
        SetSoundsVolume();
        SetMusicVolume();
        SetTexts();
    }

    private void SetTexts()
    {
        soundText.text = Convert.ToString(Mathf.Round(soundSlider.value *100f) / 100f);
        musicText.text = Convert.ToString(Mathf.Round(musicSlider.value * 100f) / 100f);
    }

    private void SetSoundsVolume()
    {
        for (int i = 0; i < soundSources.Length; i++)
        {
            soundSources[i].volume = soundSlider.value;
        }
    }

    private void SetMusicVolume()
    {     
            musicSource.volume = musicSlider.value;       
    }   
}
