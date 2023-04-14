using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;

    public float strength = 5f;
    public float gravity = -9.81f;
    public float tilt = 5f;

    private Vector3 direction;

    private void Awake()       
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()    // Starts the sprite animation with InvokeRepeating
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;      // Resets the position and direction of the player
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) { // When player presses space bar or left mouse button, jump
            direction = Vector3.up * strength;
        }

        direction.y += gravity * Time.deltaTime;                    // Apply gravity and update the position
        transform.position += direction * Time.deltaTime;

        Vector3 rotation = transform.eulerAngles;                   // Tilt the trash based on the direction
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;
    }

    private void AnimateSprite()                                // Changes the sprite image to animate it
    {
        spriteIndex++;

        if (spriteIndex >= sprites.Length) {                    // Loops back to the beginning of the sprite array
            spriteIndex = 0;
        }

        if (spriteIndex < sprites.Length && spriteIndex >= 0) { // Changes the sprite image to the current index in the sprite array
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle")) {                  // If player collides with an obstacle, try again
            FindObjectOfType<GameController>().GameOver();
        } 
        else if (other.gameObject.CompareTag("Scoring")) {              // If player collides with scoring object, increase score
            FindObjectOfType<GameController>().IncreaseScore();
        }
        else if (other.gameObject.CompareTag("Finish")) {
            FindObjectOfType<GameController>().GameWin();               // If player collides with finish object, game is done
        }
    }
}
