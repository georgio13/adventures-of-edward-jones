/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        11/9/2017
 *  Last updated:   11/9/2017
 *
 *  File:           GameplayObject.cs
 *
 *  This class the basic functions of the GameplayObject which is
 *  the superclass of the Item and Character class.
 *
 *----------------------------------------------------------------*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class GameplayObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private GameObject textReference;               // The reference to the name of the item or the character.
    private BoxCollider2D itemCollider;             // The collider which we use so the player know where he clicked.
    public AudioSource speechSource;                // The reference to the speech audio source of the scene.
    public string observationText;                  // The text that will be shown when the user clicks the observation button.
    public AudioClip observationClip;               // The clip that will play when the user clicks the observation button.
    public string inventoryInteractionText;         // The text that will be shown when the user clicks the inventory button.
    public AudioClip inventoryInteractionClip;      // The clip that will play when the user clicks the inventory button.
    public string interactiveText;                  // The text that will be shown when the user clicks the interraction button.
    public AudioClip interactiveClip;               // The clip that will play when the user clicks the interraction button.

    /// <summary>
    /// When we initialize a GameplayObject we must turn off its text.
    /// </summary>
    void Awake()
    {
        textReference = transform.GetChild(0).gameObject;
        textReference.SetActive(false);
        itemCollider = transform.GetComponent<BoxCollider2D>();
        speechSource = GameObject.Find("Speech").GetComponent<AudioSource>();
    }

    /// <summary>
    /// When the user hovers the GameplayObject we have to show its text.
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        textReference.SetActive(true);
    }

    /// <summary>
    /// When the cursor exits from the GameplayObject we have to hide its text.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        textReference.SetActive(false);
    }

    /// <summary>
    /// When we click on a GameplayObject we have to hide its text, show up
    /// to the center of the GameplayObject the ActionButtons and to inform
    /// the player for the position and extends of collider of the object.
    /// </summary>
    /// <param name="eventData">The data that we get from the mouse.</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        textReference.SetActive(false);
        ActionButtons.instance.TurnOn(this);
        transform.GetComponent<Image>().raycastTarget = false;
        ActionButtons.instance.UpdatePosition(transform.position);
        PlayerController.instance.UpdateDestination(transform.position, itemCollider.bounds.extents.x);
    }

    /// <summary>
    /// Because the observation button has the same functionality for items
    /// and characters, we implement it here. We pass the observation clip
    /// to the speech audio source and call the TurnOn function of
    /// SubtitlePanel.
    /// </summary>
    public void TransferObservation()
    {
        speechSource.clip = observationClip;
        SubtitlesPanel.instance.TurnOn(observationText);
    }

    public abstract void TransferInventory();

    public abstract void TransferDialogue();

    public abstract void TransferInteractive();
}