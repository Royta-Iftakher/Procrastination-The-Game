using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public string sceneToLoad;

    public GUIStyle style;
    private bool showGui = false;



    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            showGui = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            showGui = false;
        }
    }

    void OnGUI() 
    {
        if (showGui) 
        {
            Vector3 doorPosition = Camera.main.WorldToScreenPoint(transform.position);
            Vector2 guiPosition = GUIUtility.ScreenToGUIPoint(doorPosition);
            GUI.Label(new Rect(guiPosition.x - 50, guiPosition.y, 100, 50), "Press E to enter", style);
        }
    }

    void Update() 
    {
        if (showGui && Input.GetKeyDown(KeyCode.E)) 
        {
            //GameManager.Instance.spawnPoint = newSpawnPoint.position;

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
