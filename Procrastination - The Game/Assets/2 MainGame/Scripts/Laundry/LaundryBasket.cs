using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaundryBasket : MonoBehaviour
{
    public Sprite fullBasketSprite; // Assign the full basket sprite in the Inspector
    public Sprite emptyBasketSprite; // Assign the empty basket sprite in the Inspector
    public Sprite hiddenSprite; // Assign the hidden sprite in the Inspector

    private SpriteRenderer spriteRenderer;
    public bool basketCarried = false;
    private bool playerIsNear = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = fullBasketSprite;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !basketCarried && playerIsNear)
        {
            // Player "carries" the basket
            spriteRenderer.sprite = hiddenSprite;
            Inventory.Instance.ShowItemImage();
            basketCarried = true;
        }
    }

    public void EmptyBasket()
    {
        spriteRenderer.sprite = emptyBasketSprite;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerIsNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerIsNear = false;
        }
    }
}
