/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        11/9/2017
 *  Last updated:   16/9/2017
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
    private AudioSource speechSource;                   // This is the reference to the speech audio source.
    private GameObject startingImage;                   // This is the reference to the starting black image that shown
    private GameObject loadingImage;                    // This is the reference to the image that will be shown when the 
                                                        // new scene is loading. 
    private GameObject buttonsPanel;                    // This is the reference to the buttons of the main menu.
    private GameObject settingsPanel;                   // This is the reference to the settings panel.
    private static bool animationsPlayed = false;       // Is true when the starting animations has been played.
    private GameObject continueButton;                  // This is the reference to the continue button.
    private GameObject fadeOutTransition;               // This is the reference to the fade out transition.
    private GameObject fadeInTransition;                // This is the reference to the fade in transition.

    /// <summary>
    /// When we begin the game, we have to initialize the music and sound effects
    /// audio source. Also, we have to take the references of all the other
    /// elements of the scene, so we can use them later.
    /// </summary>
    private void Awake()
    {
        // Initialization of the music audio source.
        musicSource = GameObject.Find("Music").GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("MusicVolume"))
            musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        else
            musicSource.volume = 0.5f;
        musicSource.clip = backgroundMusic;

        // Initialization of the sound effects audio source.
        soundEffectsSource = GameObject.Find("SoundEffects").GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("SoundEffectsVolume"))
            soundEffectsSource.volume = PlayerPrefs.GetFloat("SoundEffectsVolume");
        else
            soundEffectsSource.volume = 0.5f;
        soundEffectsSource.clip = startingSoundEffects;

        // Initialization of the speech audio source.
        speechSource = GameObject.Find("Speech").GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("SpeechVolume"))
            speechSource.volume = PlayerPrefs.GetFloat("SpeechVolume");
        else
            speechSource.volume = 0.5f;

        startingImage = GameObject.Find("StartingImage");

        loadingImage = GameObject.Find("LoadingImage");
        loadingImage.SetActive(false);

        buttonsPanel = GameObject.Find("ButtonsPanel");

        settingsPanel = GameObject.Find("SettingsPanel");
        settingsPanel.SetActive(false);

        fadeOutTransition = GameObject.Find("FadeOut");
        fadeOutTransition.SetActive(false);

        fadeInTransition = GameObject.Find("FadeIn");
        fadeInTransition.SetActive(false);

        // If there is not a saved game to continue, we hide the button. 
        continueButton = GameObject.Find("ContinueButton");
        if (PlayerPrefs.HasKey("GameInitialization"))
        {
            if (PlayerPrefs.GetInt("GameInitialization") == 0)
                continueButton.SetActive(false);
        }
        else
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
            fadeOutTransition.SetActive(true);
            StartCoroutine(PlayFadeOutTransition());
        }
    }

    /// <summary>
    /// When we start the game, the sounds of steps plays first and then the 
    /// fade out animation plays. After that starts to play the background music.
    /// </summary>
    /// <returns>There is nothing to return.</returns>
    IEnumerator OpenScene()
    {
        soundEffectsSource.Play();

        yield return new WaitForSeconds(soundEffectsSource.clip.length);

        startingImage.GetComponent<Animation>().Play();
        
        musicSource.Play();

        yield return new WaitForSeconds(startingImage.GetComponent<Animation>().clip.length);

        startingImage.SetActive(false);
    }

    /// <summary>
    /// In this coroutine we play the fade out animation which will be played when
    /// we return to the main menu.
    /// </summary>
    /// <returns>There is nothing to return.</returns>
    IEnumerator PlayFadeOutTransition()
    {
        startingImage.SetActive(false);
        fadeOutTransition.GetComponent<Animation>().Play();
        musicSource.PlayDelayed(fadeOutTransition.GetComponent<Animation>().clip.length / 2);

        yield return new WaitForSeconds(fadeOutTransition.GetComponent<Animation>().clip.length);

        fadeOutTransition.SetActive(false);
    }

    /// <summary>
    /// This function loads the last scene that the player has played.
    /// </summary>
    public void ContinueGame()
    {
        StartCoroutine(LoadNewScene(PlayerPrefs.GetString("LastScene")));
    }

    /// <summary>
    /// This function reset the Game Data, the last scene and the variable
    /// which affect the continue button and loads the first scene.
    /// </summary>
    public void NewGame()
    {
        PlayerPrefs.SetString("LastScene", "FirstScene");
        PlayerPrefs.SetInt("GameInitialization", 0);
        DataHandler.instance.gameData = new GameData();
        DataHandler.instance.SaveData();
        StartCoroutine(LoadNewScene("FirstScene"));
    }

    /// <summary>
    /// This function plays the animation of loading until the
    /// new scene is ready to initialize.
    /// </summary>
    /// <returns>There is nothing to return.</returns>
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
        StartCoroutine(PlayFadeInTransition());
    }

    /// <summary>
    /// This function plays the fade in transition when we exit the game.
    /// </summary>
    /// <returns>There is nothing to return.</returns>
    IEnumerator PlayFadeInTransition()
    {
        fadeInTransition.SetActive(true);
        fadeInTransition.GetComponent<Animation>().Play();

        yield return new WaitForSeconds(fadeInTransition.GetComponent<Animation>().clip.length);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
