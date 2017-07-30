using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotItem : MonoBehaviour, IPointerDownHandler
{
    public string title;
    private Sprite slotImage;

    public SlotItem(string title)
    {
        this.title = title;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        slotImage = transform.GetComponent<Image>().sprite;

        if (!slotImage.Equals(null))
        {
            InventoryPanel.activeItem.sprite = slotImage;
            InventoryPanel.activeItem.color = Color.white;
            Inventory.instance.TurnOff();
        }
    }
}
