/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        11/9/2017
 *  Last updated:   12/9/2017
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

public class SlotItem : MonoBehaviour, IPointerDownHandler
{
    public string title;            // This is the name of the name the slot which will help us with the interraction.
    private Sprite slotImage;       // This is the image of the item that will be shown to the slot.

    /// <summary>
    /// This is the simple constructor of the slot item.
    /// </summary>
    /// <param name="title">The name of the slot item.</param>
    public SlotItem(string title)
    {
        this.title = title;
    }

    /// <summary>
    /// When we click a slot item, we take the reference to the image of the slot and change the active
    /// item image, if the slot item has an image different from null.
    /// </summary>
    /// <param name="eventData">The data of the mouse.</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        slotImage = transform.GetComponent<Image>().sprite;

        if (!slotImage.Equals(null))
        {
            Inventory.activeItem.sprite = slotImage;
            Inventory.activeItem.color = Color.white;
            Inventory.instance.TurnOff();
        }
    }
}
