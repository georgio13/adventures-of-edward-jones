/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        11/9/2017
 *  Last updated:   12/9/2017
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
            pauseMenu.SetActive(true);
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
    /// This function returns the user to the main menu.
    /// </summary>
    public void QuitToMainMenu()
    {
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        StartCoroutine(LoadNewScene());
    }

    /// <summary>
    /// This function plays the animation of loading until the
    /// main menu is ready to initialize.
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadNewScene()
    {
        StageManager.loadingImage.SetActive(true);
        StageManager.loadingImage.GetComponent<Animation>().Play();

        AsyncOperation async = SceneManager.LoadSceneAsync("StartScene");

        while (!async.isDone)
        {
            yield return null;
        }
    }

    /// <summary>
    /// This function close the game.
    /// </summary>
    public void QuitGame()
    {
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
