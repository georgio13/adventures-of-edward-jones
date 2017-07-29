using UnityEngine;

public class Item : GameplayObject
{
    public string dialogueText;
    public AudioClip dialogueSound;

    public override void TransferDialogue()
    {
        transform.GetComponent<AudioSource>().clip = dialogueSound;
        SubtitlesPanel.instance.TurnOn(this, dialogueText);
    }
}