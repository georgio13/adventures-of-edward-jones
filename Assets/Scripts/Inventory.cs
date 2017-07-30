using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private int slotToInsert;
    private GameObject inventoryPanel;
    public GameObject slot;
    private GameObject[] slots;

    private void Awake()
    {
        instance = this;
        instance.gameObject.SetActive(false);
        slotToInsert = 0;
        inventoryPanel = gameObject;
        
        slots = new GameObject[9];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = Instantiate(slot);
            slots[i].transform.SetParent(inventoryPanel.transform);
            slots[i].name = "Slot " + i;
        }
    }

    public void TurnOn()
    {
        if (!instance.gameObject.activeSelf)
            instance.gameObject.SetActive(true);
        else
            TurnOff();
    }

    public void TurnOff()
    {
        instance.gameObject.SetActive(false);
    }

    public void AddElement(string title, Sprite image)
    {
        slots[slotToInsert].transform.GetChild(0).GetComponent<SlotItem>().title = title;
        slots[slotToInsert].transform.GetChild(0).GetComponent<SlotItem>().GetComponent<Image>().sprite = image;
        slots[slotToInsert].transform.GetChild(0).GetComponent<SlotItem>().GetComponent<Image>().color = Color.white;
        slotToInsert++;
    }
}