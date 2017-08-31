using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private float limit = 10.0f;
    private float speed = 300.0f;
    private float scaleMax = 6.02f;
    private float scaleMin = 3.02f;
    private Vector2 mousePosition;
    private bool hasClicked;
    private bool hasToScale;
    private float currentScale;
    private bool reduceScale;
    private float scaleRate = 0.02f;
    private Vector2 distance;
    private float angle_in_radians;
    private float angle_in_degrees;

    // Idle images
    private Image currentImage;
    public Sprite northImage;
    public Sprite northEastImage;
    public Sprite eastImage;
    public Sprite southEastImage;
    public Sprite southImage;
    public Sprite southWestImage;
    public Sprite westImage;
    public Sprite northWestImage;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentImage = transform.GetComponent<Image>();
        animator.enabled = false;
        hasClicked = false;
        hasToScale = false;
        reduceScale = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            distance = Input.mousePosition - transform.position;
            angle_in_radians = Mathf.Atan2(distance.y, distance.x);
            angle_in_degrees = angle_in_radians * Mathf.Rad2Deg;
            hasClicked = true;
            mousePosition = Input.mousePosition;
            hasToScale = true;

            animator.enabled = true;

            if (angle_in_degrees >= 90.0 - limit && angle_in_degrees <= 90.0 + limit)
            {
                animator.SetInteger("Direction", 1);
                Debug.Log("North");
                reduceScale = true;
            }
            else if (angle_in_degrees > limit && angle_in_degrees < 90.0 - limit)
            {
                animator.SetInteger("Direction", 2);
                Debug.Log("NorthEast");
                reduceScale = true;
            }
            else if (angle_in_degrees <= limit && angle_in_degrees >= -limit)
            {
                animator.SetInteger("Direction", 3);
                Debug.Log("East");
                hasToScale = false;
            }
            else if (angle_in_degrees < -limit && angle_in_degrees > -90.0 + limit)
            {
                animator.SetInteger("Direction", 4);
                Debug.Log("SouthEast");
                reduceScale = false;
            }
            else if (angle_in_degrees <= -90.0 + limit && angle_in_degrees >= -90.0 - limit)
            {
                animator.SetInteger("Direction", 5);
                Debug.Log("South");
                reduceScale = false;
            }
            else if (angle_in_degrees < -90.0 - limit && angle_in_degrees > -180.0 + limit)
            {
                animator.SetInteger("Direction", 6);
                Debug.Log("SouthWest");
                reduceScale = false;
            }
            else if (angle_in_degrees >= 180.0 - limit || angle_in_degrees <= -180.0 + limit)
            {
                animator.SetInteger("Direction", 7);
                Debug.Log("West");
                hasToScale = false;
            }
            else if (angle_in_degrees > 90.0 + limit || angle_in_degrees < 180.0 - limit)
            {
                animator.SetInteger("Direction", 8);
                Debug.Log("NorthWest");
                reduceScale = true;
            }
        }

        if (hasClicked)
        {
            currentScale = transform.localScale.y;

            if (Vector2.Distance(transform.position, mousePosition) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, mousePosition, speed * Time.deltaTime);
                
                if (hasToScale && currentScale + scaleRate <= scaleMax && currentScale - scaleRate >= scaleMin)
                {
                    if (reduceScale)
                    {
                        transform.localScale -= new Vector3(0, scaleRate, 0);
                    }
                    else
                    {
                        transform.localScale += new Vector3(0, scaleRate, 0);
                    }
                }
            }
            else
            {
                hasClicked = false;
                hasToScale = false;
                animator.SetInteger("Direction", 0);
                animator.enabled = false;

                if (angle_in_degrees >= 90.0 - limit && angle_in_degrees <= 90.0 + limit)
                {
                    currentImage.sprite = northImage;
                }
                else if (angle_in_degrees > limit && angle_in_degrees < 90.0 - limit)
                {
                    currentImage.sprite = northEastImage;
                }
                else if (angle_in_degrees <= limit && angle_in_degrees >= -limit)
                {
                    currentImage.sprite = eastImage;
                }
                else if (angle_in_degrees < -limit && angle_in_degrees > -90.0 + limit)
                {
                    currentImage.sprite = southEastImage;
                }
                else if (angle_in_degrees <= -90.0 + limit && angle_in_degrees >= -90.0 - limit)
                {
                    currentImage.sprite = southImage;
                }
                else if (angle_in_degrees < -90.0 - limit && angle_in_degrees > -180.0 + limit)
                {
                    currentImage.sprite = southWestImage;
                }
                else if (angle_in_degrees >= 180.0 - limit || angle_in_degrees <= -180.0 + limit)
                {
                    currentImage.sprite = westImage;
                }
                else if (angle_in_degrees > 90.0 + limit || angle_in_degrees < 180.0 - limit)
                {
                    currentImage.sprite = northWestImage;
                }
            }
        }
    }
}
