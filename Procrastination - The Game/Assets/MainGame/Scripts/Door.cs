using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
     public string sceneToLoad;
    public float interactDistance = 2f;
    public GameObject canvasPrefab;
    
    private GameObject canvas;
    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNear = true;
            canvas = Instantiate(canvasPrefab, transform.position + new Vector3(0f, 2f, 0f), Quaternion.identity);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNear = false;
            Destroy(canvas);
        }
    }
}
