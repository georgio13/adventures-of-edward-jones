using UnityEngine;
using UnityEngine.UI;

public class Item : GameplayObject
{
    public bool isInventoryItem;

    public string dialogueText;
    public AudioClip dialogueSound;

    public Sprite itemIcon;
    public string itemName;

    public override void TransferInventory()
    {
        transform.GetComponent<AudioSource>().clip = inventoryInteractionSound;
        SubtitlesPanel.instance.TurnOn(this, inventoryInteractionText);

        if (isInventoryItem)
        {
            Inventory.instance.AddElement(itemName, itemIcon);
            transform.GetComponent<Image>().sprite = null;
            Invoke("InventoryCheck", transform.GetComponent<AudioSource>().clip.length);
        }
    }

    public override void TransferDialogue()
    {
        transform.GetComponent<AudioSource>().clip = dialogueSound;
        SubtitlesPanel.instance.TurnOn(this, dialogueText);
    }

    public override void TransferInteractive()
    {

    }

    private void InventoryCheck()
    {
        Destroy(gameObject);
    }
}