using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup mixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle toggleMusic;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private SettingsSystem _settingSystem;
    private Settings _settings;
    
    [SerializeField] public GameObject mainMenu;
    private void Start()
    {
        Debug.Log(_settingSystem);
        if (_settingSystem != null)
        {
            _settings = _settingSystem.Settings;
            volumeSlider.value = _settings.volume;
            toggleMusic.isOn = _settings.musicEnabled;
        }
        else
        {
            _settings = new Settings();
            _settingSystem.Settings = _settings;
            volumeSlider.value = _settings.volume;
            toggleMusic.isOn = _settings.musicEnabled;
        }

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
        if (mainMenu != null)
        {
            mainMenu.SetActive(false);
        }
        Time.timeScale = 0f;
    }

    private void ContinueGame()
    {
        _settings.volume = volumeSlider.value;
        _settings.musicEnabled = toggleMusic.isOn;
        _settingSystem.Settings = _settings;
        pausePanel.SetActive(false);
        if (mainMenu != null)
        {
            mainMenu.SetActive(true);
        }
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
