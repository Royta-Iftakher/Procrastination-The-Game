using UnityEngine;

public class Pipes : MonoBehaviour
{
    public Transform top;       // Reference to the top part of the pipe
    public Transform bottom;    // Reference to the bottom part of the pipe

    public float speed = 5f;    // Speed at which the pipes move to the left
    private float leftEdge;     // Left edge of the screen

    private void Start()
    {
        // Set the left edge of the screen
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    private void Update()
    {
        // Move the pipe to the left
        transform.position += Vector3.left * speed * Time.deltaTime;

        // If the pipe has moved past the left edge of the screen, destroy it
        if (transform.position.x < leftEdge) {
            Destroy(gameObject);
        }
    }
}
