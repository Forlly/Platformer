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
    private SaveSystem save;
    private void Start()
    {
        save = FindObjectOfType<SaveSystem>();
        volumeSlider.value = save._options.volume;
        toggleMusic.isOn = save._options.musicEnabled;
        save.UpdateOptions(save._options);
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
        save._options.volume = volumeSlider.value;
        save._options.musicEnabled = toggleMusic.isOn;
        save.UpdateOptions(save._options);
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
