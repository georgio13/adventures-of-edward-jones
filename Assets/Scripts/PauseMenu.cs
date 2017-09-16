/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        11/9/2017
 *  Last updated:   16/9/2017
 *
 *  File:           PauseMenu.cs
 *
 *  This class implements all the functionality of the pause menu.
 *
 *----------------------------------------------------------------*/

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private GameObject pauseMenu;           // This is the reference to the pause menu.
    private Text pauseMenuTitle;            // This is the reference to the pause menu title.
    private GameObject pauseMenuOptions;    // This is the reference to the pause menu options panel.
    private GameObject settingsPanel;       // This is the reference to the settings panel.
    public AudioClip clickClip;             // This is the audio clip that will play when we press the pause button.

    /// <summary>
    /// On the initialization we take the reference of all elements and hide them.
    /// </summary>
    private void Awake()
    {
        pauseMenu = GameObject.Find("PausePanel");
        pauseMenuTitle = GameObject.Find("PauseMenuTitle").GetComponent<Text>();
        pauseMenuOptions = GameObject.Find("PauseMenuOptions");
        pauseMenu.SetActive(false);

        settingsPanel = GameObject.Find("SettingsPanel");
        settingsPanel.SetActive(false);
    }

    /// <summary>
    /// This function checks if the pause menu is open to close it or else to open it.
    /// </summary>
    public void TogglePauseMenu()
    {
        if (pauseMenu.activeSelf)
            pauseMenu.SetActive(false);
        else
        {
            StageManager.soundEffectsSource.clip = clickClip;
            StageManager.soundEffectsSource.Play();
            pauseMenu.SetActive(true);
        }
    }

    /// <summary>
    /// This function change the title of the pause menu, hide the buttons of
    /// the pause menu and show the settings panel.
    /// </summary>
    public void OpenSettings()
    {
        pauseMenuTitle.text = "SETTINGS";
        pauseMenuOptions.SetActive(false);
        settingsPanel.SetActive(true);
    }

    /// <summary>
    /// This function change the title of the pause menu, hide the settings
    /// panel and show the pause menu buttons.
    /// </summary>
    public void CloseSettings()
    {
        pauseMenuTitle.text = "PAUSE MENU";
        pauseMenuOptions.SetActive(true);
        settingsPanel.SetActive(false);
    }

    /// <summary>
    /// This function returns the user to the main menu and saves the last scene.
    /// </summary>
    public void QuitToMainMenu()
    {
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        StartCoroutine(LoadNewScene());
    }

    /// <summary>
    /// This function plays first the fade in animation and then 
    /// the animation of loading until the new scene is ready to initialize.
    /// </summary>
    /// <returns>There is nothing to return.</returns>
    IEnumerator LoadNewScene()
    {
        StageManager.fadeInTransition.SetActive(true);
        StageManager.fadeInTransition.GetComponent<Animation>().Play();

        yield return new WaitForSeconds(StageManager.fadeInTransition.GetComponent<Animation>().clip.length);

        StageManager.loadingImage.SetActive(true);
        StageManager.loadingImage.GetComponent<Animation>().Play();

        AsyncOperation async = SceneManager.LoadSceneAsync("StartScene");

        while (!async.isDone)
        {
            yield return null;
        }
    }

    /// <summary>
    /// This function close the game and saves the last scene.
    /// </summary>
    public void QuitGame()
    {
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        StartCoroutine(PlayFadeInTransition());
    }

    /// <summary>
    /// This function plays the fade in transition when we exit the game.
    /// </summary>
    /// <returns>There is nothing to return.</returns>
    IEnumerator PlayFadeInTransition()
    {
        StageManager.fadeInTransition.SetActive(true);
        StageManager.fadeInTransition.GetComponent<Animation>().Play();

        yield return new WaitForSeconds(StageManager.fadeInTransition.GetComponent<Animation>().clip.length);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
