/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        11/9/2017
 *  Last updated:   11/9/2017
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
    public static Inventory instance;       // We create an instance of Inventory.
    private int slotToInsert;               // This number shows us the next available slot to insert item.
    private GameObject inventoryPanel;      // The reference to the inventory panel.
    public GameObject slot;                 // This is the template of slot which will be added to the inventory.
    private GameObject[] slots;             // This is the array with the slots of the inventory. 
    public static Image activeItem;         // The image of the active item.

    /// <summary>
    /// When we initialize the inventory, we have to take the reference of it
    /// and hide it. Also, we have to instantiate the nine slots which will be
    /// the available slots for the inventory items and get the reference to
    /// the active item. Finally, we load the saved inventory.
    /// </summary>
    private void Awake()
    {
        instance = this;
        instance.gameObject.SetActive(false);
        activeItem = GameObject.Find("ActiveItemIcon").GetComponent<Image>();
        slotToInsert = 0;
        inventoryPanel = gameObject;

        slots = new GameObject[9];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = Instantiate(slot);
            slots[i].transform.SetParent(inventoryPanel.transform);
            slots[i].name = "Slot " + i;
        }

        for (int i = 0; i < DataHandler.instance.data.inventory.Count; i++)
            AddElement(DataHandler.instance.data.inventory[i], DataHandler.instance.data.inventoryItems[i]);
    }

    /// <summary>
    /// When we click the inventory button we have to check if the inventory
    /// is opened. If it is opened, we have to close it, otherwise we have to 
    /// open it.
    /// </summary>
    public void ToggleInventory()
    {
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