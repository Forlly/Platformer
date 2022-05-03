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
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject pausePanel;
    private OptionsSystem OptionSystem;
    private Options _options;
    private void Start()
    {
        OptionSystem = FindObjectOfType<OptionsSystem>();
        _options = OptionSystem.Options;
        volumeSlider.value = _options.volume;
        toggleMusic.isOn = _options.musicEnabled;
        
        ChangeVolume(volumeSlider.value);
        ToggleMusic(toggleMusic.isOn);
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
        toggleMusic.onValueChanged.AddListener(ToggleMusic);
        pauseButton.onClick.AddListener(Pause);
        continueButton.onClick.AddListener(ContinueGame);
    }

    private void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void ContinueGame()
    {
        _options.volume = volumeSlider.value;
        _options.musicEnabled = toggleMusic.isOn;
        OptionSystem.Options = _options;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ToggleMusic(bool enabledMusic)
    {
        mixer.audioMixer.SetFloat("MusicVolume", enabledMusic ? 0 : -80);
    }

    public void ChangeVolume(float volume)
    {
        mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, volumeSlider.value));
    }
}
