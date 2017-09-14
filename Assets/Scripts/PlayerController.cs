/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        10/9/2017
 *  Last updated:   11/9/2017
 *
 *  File:           PlayerController.cs
 *
 *  This class implements the movement of the player to the scene.
 *
 *----------------------------------------------------------------*/

using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;    // We create an instance of Player.
    private Animator animator;                  // The animator of the player which plays his animations.
    private Image currentImage;                 // This image represents the image that player has now.
    public Sprite leftImage;                    // The image that player must have when he finishes his left animation.
    public Sprite rightImage;                   // The image that player must have when he finishes his right animation.
    private float speed;                        // The speed with which our player moves during his movement.
    private int limit;                          // This represents the half width of player and is used to secure that the player will not go out of the screen.
    private Vector2 destination;                // The vector which represents the point to which the user click and wants to move the player.
    private float itemStart;                    // This variable gives us the distance of the clicked item from its center.
    private Vector2 itemPosition;               // This variable gives us the center of the clicked item.
    private Vector2 mousePosition;              // The position of the cursor in the scene.
    private bool hasClicked;                    // Is true when the player clicks on the scene, except from buttons.
    private bool turnLeft;                      // Is true when player will move to the left and false when he will move to the right.
    private RaycastHit2D hit;                   // The variable that helps us to see which element of the scene has been clicked.
    private StageManager stageManager;          // We use this variable to prevent the player move before the end of clips.
    private GameplayObject[] items;             // This is the array of all items and characters in the scene.

    /// <summary>
    /// On the initialization we have to disable the animator so we can change
    /// the first image of the player. Also, we initialize all the variables
    /// of the scene and take reference to the Scene Manager.
    /// </summary>
    private void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
        animator.enabled = false;
        currentImage = transform.GetComponent<Image>();
        hasClicked = false;
        turnLeft = false;
        speed = 300.0f;
        limit = 48;
        stageManager = GameObject.Find("SceneManager").GetComponent<StageManager>();
    }

    /// <summary>
    /// In the Update method we check where the user has clicked and accordingly
    /// we set the movement of the player.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && stageManager.stageInitialized && !SubtitlesPanel.instance.isActiveAndEnabled)
        {
            mousePosition = Input.mousePosition;
            
            // Get the collider of object which has been clicked
            hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // The collider is null if the user don't have clicked the UI buttons
            if (hit.collider == null || hit.collider.gameObject.name.StartsWith("Item") || hit.collider.gameObject.name.StartsWith("Character"))
            {
                // If the user clicks on the background image we must turn off the action buttons and and activate all the items
                if (hit.collider == null)
                {
                    items = FindObjectsOfType<GameplayObject>();
                    ActionButtons.instance.TurnOff();

                    for (int i = 0; i < items.Length; i++)
                        items[i].GetComponent<Image>().raycastTarget = true;

                    // We set the destination to put it as argument to the MoveTowards function
                    destination = new Vector2(mousePosition.x, transform.position.y);
                }
                // If user clicks on an item or a character we want to move player to the start of the object
                else if (hit.collider.gameObject.name.StartsWith("Item") || hit.collider.gameObject.name.StartsWith("Character"))
                {
                    // We set the destination to put it as argument to the MoveTowards function
                    if (mousePosition.x < transform.position.x)
                        destination = new Vector2(itemPosition.x + itemStart + limit, transform.position.y);
                    else if (mousePosition.x > transform.position.x)
                        destination = new Vector2(itemPosition.x - itemStart - limit, transform.position.y);
                }

                hasClicked = true;
                
                // We must enable the animator to play the animation
                animator.enabled = true;

                // Check if the player is between the borders of the screen
                if (destination.x + limit > Screen.width)
                    destination.x = Screen.width - limit;
                else if (destination.x - limit < 0)
                    destination.x = limit;

                // Check if the player wiil move right or left
                if (mousePosition.x < transform.position.x)
                {
                    animator.SetInteger("Direction", 2);
                    turnLeft = true;
                }
                else if (mousePosition.x > transform.position.x)
                {
                    animator.SetInteger("Direction", 1);
                    turnLeft = false;
                }
            }
        }

        // Executes when the user has clicked on the scene
        if (hasClicked)
        {
            // Check if the player has reached the destination
            if (Mathf.Abs(transform.position.x - destination.x) > 0.1f)
                transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            else
            {
                hasClicked = false;
                animator.SetInteger("Direction", 0);
                animator.enabled = false;

                // When the player has reached the destination, we stop the animation and set the final image
                if (turnLeft)
                    currentImage.sprite = leftImage;
                else if (!turnLeft)
                    currentImage.sprite = rightImage;
            }
        }
    }

    // When the user clicks on an item or a character this function called to give us the position 
    // of the clicked item and the distance from the its center.
    public void UpdateDestination(Vector2 itemPosition, float itemOffset)
    {
        this.itemPosition = itemPosition;
        itemStart = itemOffset;
        Debug.Log(this.itemPosition);
        Debug.Log(itemStart);
    }
}
