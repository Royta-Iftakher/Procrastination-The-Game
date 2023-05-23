using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaundryGameController : MonoBehaviour
{
    public string guiText;
    public GameObject promptCanvas;

    public GUIStyle style;
    private bool showGui = false;


    private PlayerMovement player;
   
    

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        
    }

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
            
            promptCanvas.SetActive(false);
        }
    }

    void OnGUI() 
    {
        if (showGui) 
        {
            Vector3 objectPosition = Camera.main.WorldToScreenPoint(transform.position);
            Vector2 guiPosition = GUIUtility.ScreenToGUIPoint(objectPosition);
            GUI.Label(new Rect(guiPosition.x - 50, guiPosition.y, 100, 50), guiText, style);
        }
    }

    void Update() 
    {
        if(player == null) {
            player = FindObjectOfType<PlayerMovement>();
        }
        if (showGui && Input.GetKeyDown(KeyCode.E)) 
        {   
            //GameManager.Instance.spawnPoint = newSpawnPoint.position;
            player.isKickboard = false;
            player.KickBoard();
                    // Toggle the canvas on and off depending on its current state
            promptCanvas.SetActive(!promptCanvas.activeSelf);
        }
    }
}
