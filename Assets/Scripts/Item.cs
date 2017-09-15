/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        11/9/2017
 *  Last updated:   15/9/2017
 *
 *  File:           Item.cs
 *
 *  This class implements the special functions of the Item
 *  which mainly concern the inventory interraction.
 *
 *----------------------------------------------------------------*/

using UnityEngine;
using UnityEngine.UI;

public class Item : GameplayObject
{
    public bool isInventoryItem;        // This variable inform us if the item can be added to inventory or not.
    public Sprite itemIcon;             // This is the image that will be added to inventory if the item can be added to it.
    public string itemName;             // This is the name with which we can contol the interractions when this item is active.
    public string dialogueText;         // The text that will be shown when the user clicks the dialogue button. We don't add it to the super class
                                        // because the Character must have an array of dialogue texts to implement the dialogue.
    public AudioClip dialogueClip;      // The clip that will play when the user clicks the dialogue button.

    /// <summary>
    /// When the user clicks the inventory button of an Item we 
    /// pass the inventory clip to the speech audio source and 
    /// call the TurnOn function of SubtitlePanel. Also, we have
    /// to check if the Item is invenory item and if it is we
    /// must add to the inventory and when the player finish
    /// his phrase to detroy it. Also, if the clicked item is
    /// inventory item we update our saved data.
    /// </summary>
    public override void TransferInventory()
    {
        StageManager.speechSource.clip = inventoryInteractionClip;
        SubtitlesPanel.instance.TurnOn(inventoryInteractionText);

        if (isInventoryItem)
        {
            Inventory.instance.AddElement(itemName, itemIcon);
            DataHandler.instance.gameData.inventory.Add(itemName);
            DataHandler.instance.SaveData();
            PlayerPrefs.SetInt("GameInitialization", 1);
            transform.GetComponent<Image>().sprite = null;
            Invoke("InventoryCheck", StageManager.speechSource.clip.length);
        }
    }

    /// <summary>
    /// When the user clicks the dialogue button of an Item we 
    /// pass the dialogue clip to the speech audio source and 
    /// call the TurnOn function of SubtitlePanel.
    /// </summary>
    public override void TransferDialogue()
    {
        StageManager.speechSource.clip = dialogueClip;
        SubtitlesPanel.instance.TurnOn(dialogueText);
    }

    public override void TransferInteractive()
    {

    }

    /// <summary>
    /// Called when player finishes its phrase and destroy
    /// the selected item.
    /// </summary>
    private void InventoryCheck()
    {
        Destroy(gameObject);
    }
}