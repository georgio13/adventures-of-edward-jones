using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private int slotToInsert;
    private SlotItem[] slots;
    public SlotItem slot;
    private GameObject inventoryPanel;

    private void Awake()
    {
        instance = this;
        slotToInsert = 0;
        inventoryPanel = gameObject;

        slots = new SlotItem[8];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = Instantiate(slot);
            slots[i].transform.SetParent(inventoryPanel.transform);
            slots[i].name = "Slot " + i;
        }
    }

    public void AddElement(int id, string title, string description, Sprite image)
    {
        slots[slotToInsert].id = id;
        slots[slotToInsert].title = title;
        slots[slotToInsert].description = description;
        slots[slotToInsert].transform.GetComponent<Image>().sprite = image;
        slots[slotToInsert].transform.GetComponent<Image>().color = Color.white;
            //"FFFFFFFF";


        slotToInsert++;
    }
}
