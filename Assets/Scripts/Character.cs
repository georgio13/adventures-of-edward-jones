using UnityEngine;

public class Character : GameplayObject
{
    public string[] dialoguesText;
    public AudioClip[] dialoguesSound;

    public override void TransferDialogue()
    {
        SubtitlesPanel.instance.TurnOn(this, dialoguesText, dialoguesSound);
    }
}
