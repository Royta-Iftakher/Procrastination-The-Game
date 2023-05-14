using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject doorSprite; // Assign the door sprite object in the Inspector
    public Sprite[] doorSprites; // Assign the open and closed door sprites in the Inspector
    public float delayBeforeClose = 0.5f; // Delay before the door closes when player leaves

    private SpriteRenderer spriteRenderer;
    private Vector3 originalPosition;
    private Coroutine closeDoorCoroutine;
    private bool isPlayerNearby;

    void Start()
    {
        spriteRenderer = doorSprite.GetComponent<SpriteRenderer>();
        originalPosition = doorSprite.transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = true;

            // If the close door coroutine is running, stop it
            if (closeDoorCoroutine != null)
            {
                StopCoroutine(closeDoorCoroutine);
                closeDoorCoroutine = null;
            }
            
            // Change the sprite to the open door when the player is nearby
            spriteRenderer.sprite = doorSprites[1];
            // Move the door to the left by 1.1f
            doorSprite.transform.position = new Vector3(originalPosition.x - 1.1f, originalPosition.y, originalPosition.z);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = false;

            // Start the coroutine to close the door after a delay
            closeDoorCoroutine = StartCoroutine(CloseDoorAfterDelay());
        }
    }

    IEnumerator CloseDoorAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeClose);

        // Only close the door if the player is not nearby
        if (!isPlayerNearby)
        {
            // Change the sprite back to the closed door
            spriteRenderer.sprite = doorSprites[0];
            // Move the door back to its original position
            doorSprite.transform.position = originalPosition;
        }
    }
}
