using UnityEngine;
using UnityEngine.UI;

public class ActionButtons : MonoBehaviour
{
    public static ActionButtons instance;

    private Button observationButton;
    private Button pickUpButton;
    private Button dialogueButton;
    private Button interactiveItemButton;

    public static GameplayObject[] gameplayObjects;
    private GameplayObject selectedGameplayObject;

    void Awake()
    {
        instance = this;
        instance.gameObject.SetActive(false);

        observationButton = transform.GetChild(1).GetComponent<Button>();
        observationButton.onClick.AddListener(ObservationAction);

        pickUpButton = transform.GetChild(2).GetComponent<Button>();
        pickUpButton.onClick.AddListener(InventoryAction);

        dialogueButton = transform.GetChild(3).GetComponent<Button>();
        dialogueButton.onClick.AddListener(DialogueAction);

        interactiveItemButton = transform.GetChild(4).GetComponent<Button>();
        interactiveItemButton.onClick.AddListener(InteractiveItemAction);
    }

    public void TurnOn(GameplayObject selectedGameplayObject)
    {
        gameplayObjects = FindObjectsOfType<GameplayObject>();

        if (!instance.gameObject.activeSelf)
            instance.gameObject.SetActive(true);
        else
        {
            for (int i = 0; i < gameplayObjects.Length; i++)
                gameplayObjects[i].GetComponent<Image>().raycastTarget = true;
        }

        this.selectedGameplayObject = selectedGameplayObject;
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

    void ObservationAction()
    {
        TurnOff();

        for (int i = 0; i < gameplayObjects.Length; i++)
            gameplayObjects[i].GetComponent<Image>().raycastTarget = false;

        selectedGameplayObject.TransferObservation();
    }

    void InventoryAction()
    {
        TurnOff();

        for (int i = 0; i < gameplayObjects.Length; i++)
            gameplayObjects[i].GetComponent<Image>().raycastTarget = false;

        selectedGameplayObject.TransferInventory();
    }

    void DialogueAction()
    {
        TurnOff();

        for (int i = 0; i < gameplayObjects.Length; i++)
            gameplayObjects[i].GetComponent<Image>().raycastTarget = false;

        selectedGameplayObject.TransferDialogue();
    }

    void InteractiveItemAction()
    {

    }
}