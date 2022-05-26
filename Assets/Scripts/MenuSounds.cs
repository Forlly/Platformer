using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuSounds : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup mixer;
    private SettingsSystem _settingSystem;
    private Settings _settings;
    void Start()
    {
        _settingSystem = FindObjectOfType<SettingsSystem>();
        _settings = _settingSystem.Settings;
        ChangeVolume(_settings.volume);
        ToggleMusic(_settings.musicEnabled);
    }
    public void ToggleMusic(bool enabledMusic)
    {
        mixer.audioMixer.SetFloat("MusicVolume", enabledMusic ? 0 : -80);
    }

    public void ChangeVolume(float volume)
    {
        mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, volume));
    }

}
