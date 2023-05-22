using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CookingSimGameLoader : MonoBehaviour
{
    public string guiText;
    public GameObject promptCanvas;
    public string sceneToLoad;

    public GUIStyle style;
    private bool showGui = false;

    public Color highlightColor = Color.yellow;
    private Color originalColor;

    private PlayerMovement player;
    private Prompt prompt;
    public float curEnergy;

    private Collider2D customCollider2D; // Cache the Collider2D component
    

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        originalColor = GetComponent<Renderer>().material.color;
        customCollider2D = GetComponent<Collider2D>(); // Get the Collider2D component
        prompt = FindObjectOfType<Prompt>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            showGui = true;
            GetComponent<Renderer>().material.color = highlightColor;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            showGui = false;
            GetComponent<Renderer>().material.color = originalColor;
            prompt.hidePrompt();
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
        curEnergy = Energy.Instance.currentEnergy;
        if (PauseMenu.Instance.foodCooked || curEnergy == 5)
        {
            customCollider2D.enabled = false; // Disable the Collider2D component
         // Disable the MinigameLoader script
        }
        else
        {
            customCollider2D.enabled = true;
            if(player == null) {
                player = FindObjectOfType<PlayerMovement>();
            }
            if (showGui && Input.GetKeyDown(KeyCode.E)) 
            {   
                //GameManager.Instance.spawnPoint = newSpawnPoint.position;
                player.isKickboard = false;
                player.KickBoard();
                GameManager.Instance.sceneName = sceneToLoad;
                
                prompt.showPrompt();
            }
        }
    }

}
