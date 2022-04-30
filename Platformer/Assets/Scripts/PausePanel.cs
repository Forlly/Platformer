using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup mixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle toggleMusic;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
        toggleMusic.onValueChanged.AddListener(ToggleMusic);
    }

    public void ToggleMusic(bool enabledMusic)
    {
        mixer.audioMixer.SetFloat("MusicVolume", enabledMusic ? 0 : -80);
    }

    public void ChangeVolume(float volume)
    {
        Debug.Log(volumeSlider.value);
        mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, volumeSlider.value));
    }
}
