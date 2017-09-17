/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        17/9/2017
 *  Last updated:   17/9/2017
 *
 *  File:           SceneButton.cs
 *
 *  This class implements the functionality of scene button.
 *
 *----------------------------------------------------------------*/

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour, IPointerClickHandler
{
    public string sceneName;                // The name of scene that we want to go when we click the button.
    public AudioClip sceneButtonClick;      // This is the audio of that will be played when the user clicks a scene button.

    /// <summary>
    /// When we click the button we play its audio clip and we open
    /// the scene that is wanted.
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        StageManager.soundEffectsSource.clip = sceneButtonClick;
        StartCoroutine(LoadNewScene());
        StageManager.soundEffectsSource.Play();
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

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

        while (!async.isDone)
        {
            yield return null;
        }
    }
}
