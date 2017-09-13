/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        11/9/2017
 *  Last updated:   12/9/2017
 *
 *  File:           MainMenuManager.cs
 *
 *  This class handles all the functionality of the main menu.
 *
 *----------------------------------------------------------------*/

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public AudioClip startingSoundEffects;              // This is the clip of the starting steps.
    public AudioClip backgroundMusic;                   // This is the clip of the background music.
    private AudioSource musicSource;                    // This is the reference to the music audio source.
    private AudioSource soundEffectsSource;             // This is the reference to the sound effects audio source.
    private GameObject startingImage;                   // This is the reference to the starting black image that shown
    private GameObject loadingImage;                    // This is the reference to the image that will be shown when the 
                                                        // new scene is loading. 
    private GameObject buttonsPanel;                    // This is the reference to the buttons of the main menu.
    private GameObject settingsPanel;                   // This is the reference to the settings panel.
    private static bool animationsPlayed = false;       // Is true when the starting animations has been played.
    private GameObject continueButton;

    /// <summary>
    /// When we begin the game, we have to initialize the music and sound effects
    /// audio source. Also, we have to take the references of all the other
    /// elements of the scene, so we can use them later.
    /// </summary>
    private void Awake()
    {
        // Initialization of the music audio source.
        musicSource = GameObject.Find("Music").GetComponent<AudioSource>();
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        musicSource.clip = backgroundMusic;

        // Initialization of the sound effects audio source.
        soundEffectsSource = GameObject.Find("SoundEffects").GetComponent<AudioSource>();
        soundEffectsSource.volume = PlayerPrefs.GetFloat("SoundEffectsVolume");
        soundEffectsSource.clip = startingSoundEffects;

        startingImage = GameObject.Find("StartingImage");

        loadingImage = GameObject.Find("LoadingImage");
        loadingImage.SetActive(false);

        buttonsPanel = GameObject.Find("ButtonsPanel");

        settingsPanel = GameObject.Find("SettingsPanel");
        settingsPanel.SetActive(false);

        continueButton = GameObject.Find("ContinueButton");
        if (PlayerPrefs.GetInt("GameInitialization") == 0)
            continueButton.SetActive(false);
        
        // If the animations has been played then we simply play the 
        // backgound music. Else, we play the animations.
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

    /// <summary>
    /// When we start the game, the sounds of steps plays first and then the 
    /// fade out animation plays. After that starts to play the background music.
    /// </summary>
    /// <returns></returns>
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
        StartCoroutine(LoadNewScene(PlayerPrefs.GetString("LastScene")));
    }

    /// <summary>
    /// This function runs the command that will initialize a
    /// new game.
    /// </summary>
    public void NewGame()
    {
        PlayerPrefs.SetString("LastScene", "FirstScene");
        PlayerPrefs.SetInt("GameInitialization", 0);
        DataHandler.instance.data = new GameData();
        DataHandler.instance.SaveData();
        StartCoroutine(LoadNewScene("FirstScene"));
    }

    /// <summary>
    /// This function plays the animation of loading until the
    /// new scene is ready to initialize.
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadNewScene(string sceneName)
    {
        loadingImage.SetActive(true);
        loadingImage.GetComponent<Animation>().Play();

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

        while (!async.isDone)
        {
            yield return null;
        }
    }

    /// <summary>
    /// This function enables the Settings panel and disables the
    /// starting buttons.
    /// </summary>
    public void OpenSettings()
    {
        buttonsPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    /// <summary>
    /// This function disables the Settings panel and enables the
    /// starting buttons.
    /// </summary>
    public void CloseSettings()
    {
        buttonsPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    /// <summary>
    /// This function close the game.
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
