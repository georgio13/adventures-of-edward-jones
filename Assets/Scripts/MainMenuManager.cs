using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public AudioClip backgroundMusic;
    private AudioSource musicSource;
    private AudioSource soundEffectsSource;
    private GameObject startingImage;
    private GameObject loadingImage;
    private GameObject buttonsPanel;
    private GameObject settingsPanel;

    private void Awake()
    {
        musicSource = transform.GetComponent<AudioSource>();
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");

        soundEffectsSource = GameObject.Find("SoundEffects").GetComponent<AudioSource>();
        soundEffectsSource.volume = PlayerPrefs.GetFloat("SoundEffectsVolume");

        startingImage = GameObject.Find("StartingImage");
        loadingImage = GameObject.Find("LoadingImage");
        buttonsPanel = GameObject.Find("ButtonsPanel");
        settingsPanel = GameObject.Find("SettingsPanel");

        loadingImage.SetActive(false);
        settingsPanel.SetActive(false);
        
        StartCoroutine(OpenScene());
    }

    IEnumerator OpenScene()
    {
        yield return new WaitForSeconds(transform.GetComponent<AudioSource>().clip.length);

        startingImage.GetComponent<Animation>().Play();

        transform.GetComponent<AudioSource>().clip = backgroundMusic;
        transform.GetComponent<AudioSource>().loop = true;
        transform.GetComponent<AudioSource>().Play();

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

    public void Settings()
    {
        buttonsPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void goBack()
    {
        buttonsPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
