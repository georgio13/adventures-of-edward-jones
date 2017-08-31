using UnityEngine;
using UnityEngine.UI;

public class MainMenuSettingsManager : MonoBehaviour
{
    private Slider musicSlider;
    private Slider soundEffectsSlider;
    private Slider speechSlider;
    private AudioSource musicSource;
    private AudioSource soundEffectsSource;

    private void Awake()
    {
        musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");

        soundEffectsSlider = GameObject.Find("SoundEffectsSlider").GetComponent<Slider>();
        soundEffectsSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolume");

        speechSlider = GameObject.Find("SpeechSlider").GetComponent<Slider>();
        speechSlider.value = PlayerPrefs.GetFloat("SpeechVolume");

        musicSource = GameObject.Find("MainMenuManager").GetComponent<AudioSource>();

        soundEffectsSource = GameObject.Find("SoundEffects").GetComponent<AudioSource>();
    }

    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
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
    }
}
