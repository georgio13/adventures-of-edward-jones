/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        11/9/2017
 *  Last updated:   18/9/2017
 *
 *  File:           Inventory.cs
 *
 *  This class concerns the functionality of the inventory button
 *  and the items that will be shown when we show the inventory
 *  panel.
 *
 *----------------------------------------------------------------*/

using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;           // We create an instance of Inventory.
    private int slotToInsert;                   // This number shows us the next available slot to insert item.
    public GameObject slot;                     // This is the template of slot which will be added to the inventory.
    private GameObject[] slots;                 // This is the array with the slots of the inventory. 
    public static string activeItemName;        // This is the name of the active item.
    public static Image activeItemImage;        // The image of the active item.
    public AudioClip inventoryButtonClick;      // This is the audio clip that will play when we press the inventory button.

    /// <summary>
    /// When we initialize the inventory, we have to take the reference of it
    /// and hide it. Also, we have to instantiate the nine slots which will be
    /// the available slots for the inventory items and get the reference to
    /// the active item. Finally, we load the saved inventory.
    /// </summary>
    private void Awake()
    {
        instance = this;
        activeItemImage = GameObject.Find("ActiveItemIcon").GetComponent<Image>();
        slotToInsert = 0;

        slots = new GameObject[9];
        for (int i = 0; i < slots.Length; i++)
            slots[i] = GameObject.Find("Slot " + i);

        for (int i = 0; i < DataHandler.instance.gameData.inventory.Count; i++)
            AddElement(DataHandler.instance.gameData.inventory[i], Resources.Load<Sprite>(DataHandler.instance.gameData.inventory[i]));

        instance.gameObject.SetActive(false);
    }

    /// <summary>
    /// When we click the inventory button we have to check if the inventory
    /// is opened. If it is opened, we have to close it, otherwise we have to 
    /// open it.
    /// </summary>
    public void ToggleInventory()
    {
        StageManager.soundEffectsSource.clip = inventoryButtonClick;
        StageManager.soundEffectsSource.Play();

        if (!instance.gameObject.activeSelf)
            instance.gameObject.SetActive(true);
        else
            TurnOff();
    }

    /// <summary>
    /// This function hide the inventory.
    /// </summary>
    public void TurnOff()
    {
        instance.gameObject.SetActive(false);
    }

    /// <summary>
    /// This function adds an item to the inventory. It sets its title, which we use it for the interraction,
    /// the image of the item that will be shown when we open the inventory and set the color of the image to white
    /// so we can see the image of the item, because by default it is set to black.
    /// </summary>
    /// <param name="title">The name of the item that we will add to the inventory, which will help us with the interraction with obejects of the scene.</param>
    /// <param name="image">The image of the item that will be shown when we open the inventory.</param>
    public void AddElement(string title, Sprite image)
    {
        slots[slotToInsert].transform.GetChild(0).GetComponent<SlotItem>().title = title;
        slots[slotToInsert].transform.GetChild(0).GetComponent<SlotItem>().GetComponent<Image>().sprite = image;
        slotToInsert++;
    }
}