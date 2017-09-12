/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        11/9/2017
 *  Last updated:   12/9/2017
 *
 *  File:           SettingsManager.cs
 *
 *  This class implements all the functionality of the settings
 *  menu.
 *
 *----------------------------------------------------------------*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    private Resolution[] resolutions;           // This is the array with the available resolutions.
    private List<string> resolutionsList;
    private Dropdown resolutionDropdown;        // This is the reference to the resolution dropdown menu.
    private int resolutionWidth;                // This is the current resolution width.
    private int resolutionHeight;               // This is the current resolution height.
    private Slider musicSlider;                 // This is the reference to the musicSlider.
    private Slider soundEffectsSlider;          // This is the reference to the sound effects slider.
    private Slider speechSlider;                // This is the reference to the speech slider.
    private AudioSource musicSource;            // This is the reference to the music audio source.
    private AudioSource soundEffectsSource;     // This is the reference to the sound effects audio source.
    private AudioSource speechSource;           // This is the reference to the speech audio source.

    private void Awake()
    {
        // We get all the available resolutions and add them to a string List
        resolutions = Screen.resolutions;
        resolutionsList = new List<string>();
        resolutionDropdown = GameObject.Find("ResolutionDropdown").GetComponent<Dropdown>();
        
        // We search all the available resolutions and delete the duplicates of them and we change and their format
        foreach (Resolution resolution in resolutions)
        {
            string[] results = resolution.ToString().Split(' ');
            if (!resolutionsList.Contains(results[0] + "x" + results[2]))
            {
                resolutionsList.Add(results[0] + "x" + results[2]);
                resolutionDropdown.options.Add(new Dropdown.OptionData(results[0] + "x" + results[2]));
            }
        }

        // We check if the current resolution of the screen there is to the available options and if there is we choose it for the initialization of the screen
        if (resolutionsList.Contains(Screen.currentResolution.width.ToString() + "x" + Screen.currentResolution.height.ToString()))
            resolutionDropdown.value = resolutionsList.IndexOf(Screen.currentResolution.width.ToString() + "x" + Screen.currentResolution.height.ToString());

        // Set the value of the music slider
        musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");

        // Set the value of the sound effects slider
        soundEffectsSlider = GameObject.Find("SoundEffectsSlider").GetComponent<Slider>();
        soundEffectsSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolume");

        // Set the value of the speech slider
        speechSlider = GameObject.Find("SpeechSlider").GetComponent<Slider>();
        speechSlider.value = PlayerPrefs.GetFloat("SpeechVolume");

        // Get the references of the audio sources
        musicSource = GameObject.Find("Music").GetComponent<AudioSource>();
        soundEffectsSource = GameObject.Find("SoundEffects").GetComponent<AudioSource>();
        speechSource = GameObject.Find("Speech").GetComponent<AudioSource>();
    }

    /// <summary>
    /// When we click the button of fullscreen we toggle it to fullscreen and vise versa.
    /// </summary>
    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    /// <summary>
    /// When we select other resolution from the dropdown menu, we split its value so we can change
    /// the resolution of the screen.
    /// </summary>
    public void ResolutionChange()
    {
        string[] results = resolutionsList[resolutionDropdown.value].Split('x');
        Screen.SetResolution(int.Parse(results[0].ToString()), int.Parse(results[1].ToString()), Screen.fullScreen);
    }

    /// <summary>
    /// When we change the value of the music slider, we save it to the propriate variable.
    /// </summary>
    public void MusicChange()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    /// <summary>
    /// When we change the value of the sound effects slider, we save it to the propriate variable.
    /// </summary>
    public void SoundEffectsChange()
    {
        PlayerPrefs.SetFloat("SoundEffectsVolume", soundEffectsSlider.value);
    }

    /// <summary>
    /// When we change the value of the speech slider, we save it to the propriate variable.
    /// </summary>
    public void SpeechChange()
    {
        PlayerPrefs.SetFloat("SpeechVolume", speechSlider.value);
    }

    /// <summary>
    /// On the Update method we change the volume of all audio sources to be
    /// equal values of the corresponding sliders.
    /// </summary>
    private void Update()
    {
        musicSource.volume = musicSlider.value;
        soundEffectsSource.volume = soundEffectsSlider.value;
        speechSource.volume = speechSlider.value;
    }
}
