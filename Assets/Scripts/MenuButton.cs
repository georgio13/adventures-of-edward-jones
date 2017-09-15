/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        11/9/2017
 *  Last updated:   15/9/2017
 *
 *  File:           MenuButton.cs
 *
 *  This class implements the functions of the buttons that there
 *  are to the menus, which consists of only text.
 *
 *----------------------------------------------------------------*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject textReference;           // This is the reference to the text of the button.
    private AudioSource soundEffectsSource;     // This is the reference to the sound effects audio source.
    public AudioClip buttonHover;               // This is the clip that will play we hover the button.
    public AudioClip buttonClick;               // This is the clip that will play when we click the button.
    public Color32 startColor;                  // This is the start color of the button.
    public Color32 hoverColor;                  // This is the color that the button will take when we hover it.

    /// <summary>
    /// On the initialization we take the reference to the text of the button and
    /// to the sound effects audio source.
    /// </summary>
    private void Awake()
    {
        textReference = transform.gameObject;
        soundEffectsSource = GameObject.Find("SoundEffects").GetComponent<AudioSource>();
    }

    /// <summary>
    /// When we hover the button, we change its color and play the 
    /// propriate clip.
    /// </summary>
    /// <param name="eventData">The data of the mouse.</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        textReference.GetComponent<Text>().color = hoverColor;
        soundEffectsSource.clip = buttonHover;
        soundEffectsSource.Play();
    }

    /// <summary>
    /// When the mouse stop to hover the button, we change its
    /// color to starting.
    /// </summary>
    /// <param name="eventData">The data of the mouse.</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        textReference.GetComponent<Text>().color = startColor;
    }

    /// <summary>
    /// When we click the button we play the propriate clip and
    /// its color to the starting, because we can return to the main
    /// menu and we want all the buttons to have their starting color.
    /// </summary>
    public void OnPointerClick()
    {
        soundEffectsSource.clip = buttonClick;
        soundEffectsSource.Play();
        textReference.GetComponent<Text>().color = startColor;
    }
}
