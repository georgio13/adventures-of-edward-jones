using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private GameObject pauseMenu;
    private Text pauseMenuTitle;
    private GameObject pauseMenuOptions;
    private GameObject settingsPanel;

    private void Awake()
    {
        pauseMenu = GameObject.Find("PausePanel");
        pauseMenuTitle = GameObject.Find("PauseMenuTitle").GetComponent<Text>();
        pauseMenuOptions = GameObject.Find("PauseMenuOptions");
        pauseMenu.SetActive(false);

        settingsPanel = GameObject.Find("SettingsPanel");
        settingsPanel.SetActive(false);
    }

    public void TogglePauseMenu()
    {
        if (pauseMenu.activeSelf)
            pauseMenu.SetActive(false);
        else
            pauseMenu.SetActive(true);
    }

    public void OpenSettings()
    {
        pauseMenuTitle.text = "SETTINGS";
        pauseMenuOptions.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        pauseMenuTitle.text = "PAUSE MENU";
        pauseMenuOptions.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void QuitToMainMenu()
    {
        StartCoroutine(LoadNewScene());
    }

    IEnumerator LoadNewScene()
    {
        StageManager.loadingImage.SetActive(true);
        StageManager.loadingImage.GetComponent<Animation>().Play();

        AsyncOperation async = SceneManager.LoadSceneAsync("StartScreen");

        while (!async.isDone)
        {
            yield return null;
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
