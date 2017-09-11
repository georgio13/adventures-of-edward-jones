/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        11/9/2017
 *  Last updated:   11/9/2017
 *
 *  File:           Character.cs
 *
 *  This class implements the special functions of the Character
 *  which mainly concern the dialogue interraction.
 *
 *----------------------------------------------------------------*/

using UnityEngine;

public class Character : GameplayObject
{
    public string[] dialoguesText;          // The array of text that will be shown when the user clicks the dialogue button.
    public AudioClip[] dialoguesClips;      // The array if clips that will play when the user clicks the dialogue button.

    /// <summary>
    /// When the user clicks the dialogue button of a Character we 
    /// pass the inventory clip to the speech audio source and 
    /// call the TurnOn function of SubtitlePanel.
    /// </summary>
    public override void TransferInventory()
    {
        speechSource.clip = inventoryInteractionClip;
        SubtitlesPanel.instance.TurnOn(inventoryInteractionText);
    }

    /// <summary>
    /// When the user clicks the dialogue button of a Character we 
    /// call the TurnOn function of SubtitlePanel.
    /// </summary>
    public override void TransferDialogue()
    {
        SubtitlesPanel.instance.TurnOn(dialoguesText, dialoguesClips);
    }

    /// <summary>
    /// When the user clicks the interraction button of a Character we 
    /// pass the interraction clip to the speech audio source and 
    /// call the TurnOn function of SubtitlePanel.
    /// </summary>
    public override void TransferInteractive()
    {
        speechSource.clip = interactiveClip;
        SubtitlesPanel.instance.TurnOn(interactiveText);
    }
}