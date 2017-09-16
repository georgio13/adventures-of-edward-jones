/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        11/9/2017
 *  Last updated:   16/9/2017
 *
 *  File:           MapButton.cs
 *
 *  This class implements the functions of the buttons that there
 *  are to the map.
 *
 *----------------------------------------------------------------*/

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MapButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Animation mapAnimation;         // The animation that will be played when we hover the button.
    public string sceneName;                // The name of scene that we want to go when we click the button.
    public AudioClip mapButtonClick;        // This is the audio of that will be played when the user clicks a map button.

    /// <summary>
    /// On the initialization we take the reference to the animation.
    /// </summary>
    private void Awake()
    {
        mapAnimation = transform.GetComponent<Animation>();
    }

    /// <summary>
    /// When the mouse hover the button, the animation starts to play.
    /// </summary>
    /// <param name="eventData">The data of the mouse.</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        mapAnimation.Play();
    }

    /// <summary>
    /// When the stop to hover the button, the animation stop and the 
    /// button return to its start state.
    /// </summary>
    /// <param name="eventData">The data of the mouse.</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        mapAnimation.Stop();
        transform.localScale = new Vector3(1, 1, 1);
    }

    /// <summary>
    /// When we click the button we play its audio clip and we check 
    /// if the scene that we want to go isn't the current. 
    /// If it isn't we open it.
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            StageManager.soundEffectsSource.clip = mapButtonClick;
            StartCoroutine(LoadNewScene());
            StageManager.soundEffectsSource.Play();
        } 
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
