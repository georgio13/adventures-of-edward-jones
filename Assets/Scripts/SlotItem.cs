/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        11/9/2017
 *  Last updated:   18/9/2017
 *
 *  File:           SlotItem.cs
 *
 *  This class implements the functionality of the slot which
 *  added to the inventoty panel.
 *
 *----------------------------------------------------------------*/

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotItem : MonoBehaviour, IPointerClickHandler
{
    public string title;                // This is the name of the name the slot which will help us with the interraction.
    private Sprite slotImage;           // This is the image of the item that will be shown to the slot.
    public AudioClip slotItemClick;     // This is the audio clip that will play when we press a slot of the inventory.

    /// <summary>
    /// When we click a slot item, we take the reference to the image of the slot and change the active
    /// item image, if the slot item has an image different from null. Also, we change the name of
    /// the active item which which hold by the inventory.
    /// </summary>
    /// <param name="eventData">The data of the mouse.</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        slotImage = transform.GetComponent<Image>().sprite;

        if (!slotImage.Equals(null))
        {
            StageManager.soundEffectsSource.clip = slotItemClick;
            StageManager.soundEffectsSource.Play();
            Inventory.activeItemName = title;
            Inventory.activeItemImage.sprite = slotImage;
            Inventory.instance.TurnOff();
        }
    }
}
