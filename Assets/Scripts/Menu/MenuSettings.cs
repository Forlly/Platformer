using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropDown;
    [SerializeField] private TMP_Dropdown qualityDropDown;
    [SerializeField] private Toggle fullScreen;
    [SerializeField] private Button saveButton;
    private GraphicSettings graphicSettings;

    private Resolution[] resolutions;
    
    void Start()
    {
        resolutionDropDown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;

        resolutionDropDown.onValueChanged.AddListener(SetResolution);
        qualityDropDown.onValueChanged.AddListener(SetQuality);
        fullScreen.onValueChanged.AddListener(SetFullScreen);
        saveButton.onClick.AddListener(SaveSettings);

        if (!File.Exists($"{Path.Combine(Application.dataPath, "Json")}/GraphicSettings.json"))
        {
            graphicSettings = new GraphicSettings();
        }
        else
        {
                graphicSettings = SaveSystem.LoadFile<GraphicSettings>(Path.Combine(Application.dataPath, "Json"),
                "GraphicSettings.json");
        }
        
        int currentResolutionIndx = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate +
                            "Hz";
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndx = i;
            }
        }
        
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.RefreshShownValue();

        LoadSettings(currentResolutionIndx);
    }

    private void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        graphicSettings.fullScreen = isFullScreen;
    }
    
    private void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        graphicSettings.resolution = resolutionIndex;
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
    private void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        graphicSettings.quality = qualityIndex;
    }

    private void LoadSettings(int currentResolutionIndex)
    {
        qualityDropDown.value = graphicSettings.quality;
        if (graphicSettings.resolution != 0)
        {
            resolutionDropDown.value = graphicSettings.resolution;
        }
        else
        {
            resolutionDropDown.value = currentResolutionIndex;
        }

        Screen.fullScreen = graphicSettings.fullScreen;
        fullScreen.isOn = graphicSettings.fullScreen;

    }

    public void SaveSettings()
    {
        SaveSystem.SaveFile<GraphicSettings>(graphicSettings, Path.Combine(Application.dataPath, "Json"),
            "GraphicSettings.json");
    }
}
[Serializable]
public class GraphicSettings
{
    public bool fullScreen = true;
    public int resolution = 0;
    public int quality = 3;

}
