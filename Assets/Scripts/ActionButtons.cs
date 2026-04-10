/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        11/9/2017
 *  Last updated:   10/4/2026
 *
 *  File:           ActionButtons.cs
 *
 *  This class implements the all the functions of Action Buttons.
 *
 *----------------------------------------------------------------*/

using UnityEngine;
using UnityEngine.UI;

public class ActionButtons : MonoBehaviour
{
    public static ActionButtons instance;               // We create an instance of Action Buttons.
    public static GameplayObject[] gameplayObjects;     // This array contains all GameplayObjects which there are to the scene.
    private GameplayObject selectedGameplayObject;      // This is the selected GameplayObject.

    /// <summary>
    /// When we initialize Actions Buttons, we have to get the reference to them
    /// and them hide their panel. 
    /// </summary>
    private void Awake()
    {
        instance = this;
        instance.gameObject.SetActive(false);
    }

    /// <summary>
    /// When we want to show the Action Buttons panel, we have to check
    /// if it is already open and if it isn't to show it. Also, we have
    /// to enable all the other GameplayObjects of the scene so they 
    /// can be clicked.
    /// </summary>
    /// <param name="selectedGameplayObject">The GameplayObject that just has been clicked.</param>
    public void TurnOn(GameplayObject selectedGameplayObject)
    {
        gameplayObjects = FindObjectsByType<GameplayObject>();

        if (!instance.gameObject.activeSelf)
            instance.gameObject.SetActive(true);
        else
        {
            for (int i = 0; i < gameplayObjects.Length; i++)
                gameplayObjects[i].GetComponent<Image>().raycastTarget = true;
        }

        this.selectedGameplayObject = selectedGameplayObject;
    }

    /// <summary>
    /// When we want to close the Action Buttons panel, we have to
    /// check if it is enabled and then close it. The main reason of it
    /// is that there is the option to click on the background of the scene
    /// and if the panel is opened or not, we want to hide it.
    /// </summary>
    public void TurnOff()
    {
        if (instance.gameObject.activeSelf)
            instance.gameObject.SetActive(false);
    }

    /// <summary>
    /// This function sets the position of the Action Buttons panel
    /// to the center of the selected GameplayObject.
    /// </summary>
    /// <param name="position">The center of the clicked item.</param>
    public void UpdatePosition(Vector3 position)
    {
        instance.transform.position = position;
    }

    /// <summary>
    /// This function hide firstly the Action Button panel and secure to us 
    /// that when the user clicks the observation button, he cannot click 
    /// to other GameplayObject until the selected has completed its action.
    /// </summary>
    public void ObservationAction()
    {
        TurnOff();

        for (int i = 0; i < gameplayObjects.Length; i++)
            gameplayObjects[i].GetComponent<Image>().raycastTarget = false;

        selectedGameplayObject.TransferObservation();
    }

    // <summary>
    /// This function hide firstly the Action Button panel and secure to us 
    /// that when the user clicks the inventory button, he cannot click 
    /// to other GameplayObject until the selected has completed its action.
    /// </summary>
    public void InventoryAction()
    {
        TurnOff();

        for (int i = 0; i < gameplayObjects.Length; i++)
            gameplayObjects[i].GetComponent<Image>().raycastTarget = false;

        selectedGameplayObject.TransferInventory();
    }

    // <summary>
    /// This function hide firstly the Action Button panel and secure to us 
    /// that when the user clicks the dialogue button, he cannot click 
    /// to other GameplayObject until the selected has completed its action.
    /// </summary>
    public void DialogueAction()
    {
        TurnOff();

        for (int i = 0; i < gameplayObjects.Length; i++)
            gameplayObjects[i].GetComponent<Image>().raycastTarget = false;

        selectedGameplayObject.TransferDialogue();
    }

    // <summary>
    /// This function hide firstly the Action Button panel and secure to us 
    /// that when the user clicks the interraction button, he cannot click 
    /// to other GameplayObject until the selected has completed its action.
    /// </summary>
    public void InteractiveItemAction()
    {
        TurnOff();

        for (int i = 0; i < gameplayObjects.Length; i++)
            gameplayObjects[i].GetComponent<Image>().raycastTarget = false;

        selectedGameplayObject.TransferInteractive();
    }
}