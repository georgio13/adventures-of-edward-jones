using UnityEngine;

public class Character : GameplayObject
{
    public string[] dialoguesText;
    public AudioClip[] dialoguesSound;

    public override void TransferInventory()
    {
        transform.GetComponent<AudioSource>().clip = inventoryInteractionSound;
        SubtitlesPanel.instance.TurnOn(this, inventoryInteractionText);
    }

    public override void TransferDialogue()
    {
        SubtitlesPanel.instance.TurnOn(this, dialoguesText, dialoguesSound);
    }

    public override void TransferInteractive()
    {
        transform.GetComponent<AudioSource>().clip = interactiveSound;
        SubtitlesPanel.instance.TurnOn(this, interactiveText);
    }
}