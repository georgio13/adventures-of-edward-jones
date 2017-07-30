using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    private Button inventoryButton;
    public static Image activeItem;

    private void Awake()
    {
        inventoryButton = transform.GetChild(0).GetComponent<Button>();
        inventoryButton.onClick.AddListener(OpenInventory);

        activeItem = transform.GetChild(1).GetChild(0).GetComponent<Image>();
    }

    void OpenInventory()
    {
        Inventory.instance.TurnOn();
    }
}