/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        11/9/2017
 *  Last updated:   14/9/2017
 *
 *  File:           SubtitlesPanel.cs
 *
 *  This class implements the all the functions of Subtitles Panel.
 *
 *----------------------------------------------------------------*/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SubtitlesPanel : MonoBehaviour
{
    public static SubtitlesPanel instance;      // We create an instance of Subtitles Panel.
    private AudioSource speechSource;           // The reference to the speech audio source of the scene.

    /// <summary>
    /// When we initialize the Subtitles Panel, we have to take the
    /// reference to it and then hide it.
    /// </summary>
   private void Awake()
    {
        instance = this;
        instance.gameObject.SetActive(false);
        speechSource = GameObject.Find("Speech").GetComponent<AudioSource>();
    }

    /// <summary>
    /// In the most cases, we the clip of speech audio source has been
    /// changed and then we call this function to show the subtitle text
    /// and to play the clip. Then we have to hide the Subtitles Panel
    /// and to enable all the GameplayObjects of the scene.
    /// </summary>
    /// <param name="subtitleText">The subtitle text that we will show in the Subtitles Panel.</param>
    public void TurnOn(string subtitleText)
    {
        instance.gameObject.SetActive(true);

        GetComponent<Text>().text = subtitleText;
        speechSource.Play();
        Invoke("TurnOff", speechSource.clip.length);
    }

    /// <summary>
    /// This function enabels the Subtitles Panel and then calls the 
    /// PlayDialogue coroutine.
    /// </summary>
    /// <param name="subtitlesText">The array of texts that will be shown to the Subtitles Panel.</param>
    /// <param name="subtitlesSound">The array of clips that will be played during the dialogue.</param>
    public void TurnOn(string[] subtitlesText, AudioClip[] subtitlesSound)
    {
        instance.gameObject.SetActive(true);

        StartCoroutine(PlayDialogue(subtitlesText, subtitlesSound));
    }

    /// <summary>
    /// In this function we hide the Subtitles Panel and then enable all
    /// the GameplayObjects of the scene.
    /// </summary>
    public void TurnOff()
    {
        instance.gameObject.SetActive(false);

        for (int i = 0; i < ActionButtons.gameplayObjects.Length; i++)
            ActionButtons.gameplayObjects[i].GetComponent<Image>().raycastTarget = true;
    }

    /// <summary>
    /// This function shows each element of the string array while the corresponding
    /// clip is playing. When all clips has been played the function calls the 
    /// TurnOff function.
    /// </summary>
    /// <param name="subtitlesText">The array of texts that will be shown to the Subtitles Panel.</param>
    /// <param name="subtitlesSound">The array of clips that will be played during the dialogue.</param>
    /// <returns>There is nothing to return.</returns>
    IEnumerator PlayDialogue(string[] subtitlesText, AudioClip[] subtitlesSound)
    {
        for (int i = 0; i < subtitlesText.Length; i++)
        {
            GetComponent<Text>().text = subtitlesText[i];
            speechSource.clip = subtitlesSound[i];
            speechSource.Play();
            yield return new WaitForSeconds(speechSource.clip.length);
        }

        TurnOff();
    }
}