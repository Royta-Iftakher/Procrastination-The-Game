using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaundryDoorScript : MonoBehaviour
{
    public Sprite[] doorSprites; // Assign the array of door sprites in the Inspector
    public GameObject doorIcon; // Assign the door icon GameObject in the Inspector
    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex = 0; // Initially, the first sprite is displayed
    private bool isPlayerNear = false; // Initially, the player is not near the door

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = doorSprites[currentSpriteIndex]; // Display the initial sprite
        doorIcon.SetActive(true); // Display the door icon at the start
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            // Player is near the door and presses E, so open the door completely
            currentSpriteIndex = 2;
            spriteRenderer.sprite = doorSprites[currentSpriteIndex];
            doorIcon.SetActive(false); // Hide the door icon when the door is not closed
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Player is entering the trigger, so open the door halfway
            isPlayerNear = true;
            currentSpriteIndex = 1;
            spriteRenderer.sprite = doorSprites[currentSpriteIndex];
            doorIcon.SetActive(false); // Hide the door icon when the door is not closed
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Player is exiting the trigger, so start the door closing animation
            isPlayerNear = false;
            StartCoroutine(CloseDoor());
        }
    }

    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(.5f); // Wait for 0.5 second
        currentSpriteIndex = 1;
        spriteRenderer.sprite = doorSprites[currentSpriteIndex]; // Change the sprite to the halfway open door

        yield return new WaitForSeconds(.5f); // Wait for 0.5 more second
        currentSpriteIndex = 0;
        spriteRenderer.sprite = doorSprites[currentSpriteIndex]; // Change the sprite to the closed door
        doorIcon.SetActive(true); // Display the door icon when the door is closed
    }
}
