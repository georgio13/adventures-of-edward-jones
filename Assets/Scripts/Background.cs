using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Background : MonoBehaviour, IPointerDownHandler
{
    private Item[] items;

    void Awake()
    {
        items = FindObjectsOfType<Item>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ActionButtons.instance.TurnOff();
        SubtitlesPanel.instance.TurnOff();

        for (int i = 0; i < items.Length; i++)
            items[i].GetComponent<Image>().raycastTarget = true;
    }
}
