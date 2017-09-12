/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        11/9/2017
 *  Last updated:   12/9/2017
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

public class MapButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animation mapAnimation;     // The animation that will be played when we hover the button.
    public string sceneName;            // The name of scene that we want to go when we click the button.

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
    /// When we click the button we check if the scene that we want to
    /// go isn't the current. If it isn't we open it.
    /// </summary>
    public void OnPointerClick()
    {
        if (SceneManager.GetActiveScene().name != sceneName)
            StartCoroutine(LoadNewScene());
    }

    /// <summary>
    /// This function plays the animation of loading until the
    /// new scene is ready to initialize.
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadNewScene()
    {
        StageManager.loadingImage.SetActive(true);
        StageManager.loadingImage.GetComponent<Animation>().Play();

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

        while (!async.isDone)
        {
            yield return null;
        }
    }
}
