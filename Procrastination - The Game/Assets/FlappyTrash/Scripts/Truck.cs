using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    private GameController manager;
    public float speed = 2.0f;

    void Start()
    {
        manager = FindObjectOfType<GameController>();    // Finds the game controller in the scene
    }

    void Update()
    {   
        // Increase the speed of the truck when the player reaches a score of 10
        if(manager.score == 10 && manager.lose == false) {  
            speed += .025f;
        }
        // Stop the truck when the player loses after reaching a score of 10
        else if(manager.score == 10 && manager.lose == true) {
            speed = 0f;
            // Resets the position of the truck to the right side of the screen
            transform.position = new Vector3(7.23f, transform.position.y, transform.position.z);  
        }
        
        // Move the truck to the left
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        
        // Destroy the buildings after they disappear from the screen
        if (transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }
}
