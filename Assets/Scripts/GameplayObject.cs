using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class GameplayObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private GameObject textReference;

    public string observationText;
    public AudioClip observationClip;

    public string inventoryInteractionText;
    public AudioClip inventoryInteractionSound;

    public string interactiveText;
    public AudioClip interactiveSound;

    void Awake()
    {
        textReference = transform.GetChild(0).gameObject;
        textReference.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textReference.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textReference.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        textReference.SetActive(false);
        ActionButtons.instance.TurnOn(this);
        transform.GetComponent<Image>().raycastTarget = false;
        ActionButtons.instance.UpdatePosition(eventData.position);
    }

    public void TransferObservation()
    {
        transform.GetComponent<AudioSource>().clip = observationClip;
        SubtitlesPanel.instance.TurnOn(this, observationText);
    }

    public abstract void TransferInventory();

    public abstract void TransferDialogue();

    public abstract void TransferInteractive();
}