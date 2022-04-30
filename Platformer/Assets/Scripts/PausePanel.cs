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
    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        toggleMusic.isOn = ConvertPlayerPrefsToBool(PlayerPrefs.GetInt("MusicEnabled"));
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
        PlayerPrefs.SetInt("MusicEnabled", ConvertIntToPlayerPrefs(toggleMusic.isOn));
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private bool ConvertPlayerPrefsToBool(int musicEnbl)
    {
        return musicEnbl != 0;
    }
    private int ConvertIntToPlayerPrefs(bool musicEnbl)
    {
        return musicEnbl ? 1 : 0;
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
