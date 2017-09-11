using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    public AudioClip startingSoundEffects;
    public AudioClip backgroundMusic;
    private AudioSource musicSource;
    private AudioSource soundEffectsSource;

    private GameObject startingImage;
    private GameObject loadingImage;
    private GameObject buttonsPanel;
    private GameObject settingsPanel;

    private static bool animationsPlayed = false;

    private void Awake()
    {
        musicSource = GameObject.Find("Music").GetComponent<AudioSource>();
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        musicSource.clip = backgroundMusic;

        soundEffectsSource = GameObject.Find("SoundEffects").GetComponent<AudioSource>();
        soundEffectsSource.volume = PlayerPrefs.GetFloat("SoundEffectsVolume");
        soundEffectsSource.clip = startingSoundEffects;

        startingImage = GameObject.Find("StartingImage");

        loadingImage = GameObject.Find("LoadingImage");
        loadingImage.SetActive(false);

        buttonsPanel = GameObject.Find("ButtonsPanel");

        settingsPanel = GameObject.Find("SettingsPanel");
        settingsPanel.SetActive(false);
        
        if (!animationsPlayed)
        {
            StartCoroutine(OpenScene());
            animationsPlayed = true;
        }
        else
        {
            musicSource.Play();
            startingImage.SetActive(false);
        }
    }

    IEnumerator OpenScene()
    {
        soundEffectsSource.Play();

        yield return new WaitForSeconds(soundEffectsSource.clip.length);

        startingImage.GetComponent<Animation>().Play();
        
        musicSource.Play();

        yield return new WaitForSeconds(startingImage.GetComponent<Animation>().clip.length);

        startingImage.SetActive(false);
    }

    public void ContinueGame()
    {

    }

    public void NewGame()
    {
        StartCoroutine(LoadNewScene());
    }

    IEnumerator LoadNewScene()
    {
        loadingImage.SetActive(true);
        loadingImage.GetComponent<Animation>().Play();

        AsyncOperation async = SceneManager.LoadSceneAsync("FirstScene");

        while (!async.isDone)
        {
            yield return null;
        }
    }

    public void OpenSettings()
    {
        buttonsPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        buttonsPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
