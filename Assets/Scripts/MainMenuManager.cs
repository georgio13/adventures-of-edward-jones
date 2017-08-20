using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public AudioClip backgroundMusic;
    private GameObject startingImage;
    private GameObject loadingImage;

    private void Awake()
    {
        startingImage = GameObject.Find("StartingImage");
        loadingImage = GameObject.Find("LoadingImage");
        loadingImage.SetActive(false);
        StartCoroutine(OpenScene());
    }

    IEnumerator OpenScene()
    {
        yield return new WaitForSeconds(transform.GetComponent<AudioSource>().clip.length);

        startingImage.GetComponent<Animation>().Play();

        transform.GetComponent<AudioSource>().clip = backgroundMusic;
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
