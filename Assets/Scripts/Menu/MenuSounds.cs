using UnityEngine;
using UnityEngine.Audio;

public class MenuSounds : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup mixer;
    [SerializeField] private SettingsSystem _settingSystem;
    private Settings _settings;
    void Start()
    {
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
