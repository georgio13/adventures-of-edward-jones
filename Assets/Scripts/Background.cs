using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Background : MonoBehaviour, IPointerDownHandler
{
    private GameplayObject[] items;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!SubtitlesPanel.instance.isActiveAndEnabled)
        {
            items = FindObjectsOfType<GameplayObject>();
            ActionButtons.instance.TurnOff();

            for (int i = 0; i < items.Length; i++)
                items[i].GetComponent<Image>().raycastTarget = true;
        }
    }
}
