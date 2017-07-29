using UnityEngine;
using UnityEngine.UI;

public class ActionButtons : MonoBehaviour
{
    public static ActionButtons instance;
    private Button observationButton;
    private Button pickUpButton;
    private Button dialogueButton;
    private Button interactiveItemButton;
    private Item[] items;
    private GameplayObject itemToCall;

    void Awake()
    {
        instance = this;
        instance.gameObject.SetActive(false);
        observationButton = transform.GetChild(1).GetComponent<Button>();
        observationButton.onClick.AddListener(ReadObservationText);
        pickUpButton = transform.GetChild(2).GetComponent<Button>();
        pickUpButton.onClick.AddListener(AddToInventory);
        dialogueButton = transform.GetChild(3).GetComponent<Button>();
        dialogueButton.onClick.AddListener(ReadDialogueText);
        interactiveItemButton = transform.GetChild(4).GetComponent<Button>();
        //interactiveItemButton.onClick.AddListener(AddToInventory);
    }

    public void TurnOn(GameplayObject itemToCall)
    {
        items = FindObjectsOfType<Item>();

        if (!instance.gameObject.activeSelf)
            instance.gameObject.SetActive(true);
        else
        {
            for (int i = 0; i < items.Length; i++)
                items[i].GetComponent<Image>().raycastTarget = true;
        }

        this.itemToCall = itemToCall;
    }

    public void TurnOff()
    {
        if (instance.gameObject.activeSelf)
            instance.gameObject.SetActive(false);
    }

    public void UpdatePosition(Vector3 position)
    {
        instance.transform.position = position;
    }

    void ReadObservationText()
    {
        TurnOff();

        itemToCall.TransferObservation();

        for (int i = 0; i < items.Length; i++)
            items[i].GetComponent<Image>().raycastTarget = false;
    }

    void AddToInventory()
    {
        TurnOff();
        //int id = itemCall.transform.GetComponent<Item>().id;
        //string title = itemText;
        //string description = itemCall.transform.GetComponent<Item>().description;
        //Sprite itemImage = itemCall.transform.GetComponent<Item>().itemImage;
        //Inventory.instance.AddElement(id, title, description, itemImage);
        //Destroy(itemCall);
    }

    void ReadDialogueText()
    {
        TurnOff();

        itemToCall.TransferDialogue();

        for (int i = 0; i < items.Length; i++)
            items[i].GetComponent<Image>().raycastTarget = false;
    }
}
