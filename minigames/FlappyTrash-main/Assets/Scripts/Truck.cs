using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    private GameManager manager;
    public float speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(manager.score == 10 && manager.lose == false) {
            speed += .0055f;
        }
        else if(manager.score == 10 && manager.lose == true) {
            speed = 0f;
            transform.position = new Vector3(7.23f, transform.position.y, transform.position.z);
        }
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        
        if (transform.position.x < -15)
    {
        Destroy(gameObject);
    }
    }

}
