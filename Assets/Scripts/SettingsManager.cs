using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    private Resolution[] resolutions;
    private List<string> resolutionsList;
    private Dropdown resolutionDropdown;
    private int resolutionWidth;
    private int resolutionHeight;

    private Slider musicSlider;
    private Slider soundEffectsSlider;
    private Slider speechSlider;

    private AudioSource musicSource;
    private AudioSource soundEffectsSource;
    private AudioSource speechSource;

    private void Awake()
    {
        resolutions = Screen.resolutions;
        resolutionsList = new List<string>();
        resolutionDropdown = GameObject.Find("ResolutionDropdown").GetComponent<Dropdown>();
        
        foreach (Resolution resolution in resolutions)
        {
            string[] results = resolution.ToString().Split(' ');
            if (!resolutionsList.Contains(results[0] + "x" + results[2]))
            {
                resolutionsList.Add(results[0] + "x" + results[2]);
                resolutionDropdown.options.Add(new Dropdown.OptionData(results[0] + "x" + results[2]));
            }
        }

        if (resolutionsList.Contains(Screen.currentResolution.width.ToString() + "x" + Screen.currentResolution.height.ToString()))
        {
            resolutionDropdown.value = resolutionsList.IndexOf(Screen.currentResolution.width.ToString() + "x" + Screen.currentResolution.height.ToString());
        }

        musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");

        soundEffectsSlider = GameObject.Find("SoundEffectsSlider").GetComponent<Slider>();
        soundEffectsSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolume");

        speechSlider = GameObject.Find("SpeechSlider").GetComponent<Slider>();
        speechSlider.value = PlayerPrefs.GetFloat("SpeechVolume");

        musicSource = GameObject.Find("Music").GetComponent<AudioSource>();

        soundEffectsSource = GameObject.Find("SoundEffects").GetComponent<AudioSource>();

        speechSource = GameObject.Find("Speech").GetComponent<AudioSource>();
    }

    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void ResolutionChange()
    {
        string[] results = resolutionsList[resolutionDropdown.value].Split('x');
        Screen.SetResolution(int.Parse(results[0].ToString()), int.Parse(results[1].ToString()), Screen.fullScreen);
    }

    public void MusicChange()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SoundEffectsChange()
    {
        PlayerPrefs.SetFloat("SoundEffectsVolume", soundEffectsSlider.value);
    }

    public void SpeechChange()
    {
        PlayerPrefs.SetFloat("SpeechVolume", speechSlider.value);
    }

    private void Update()
    {
        musicSource.volume = musicSlider.value;
        soundEffectsSource.volume = soundEffectsSlider.value;
        speechSource.volume = speechSlider.value;
    }
}
