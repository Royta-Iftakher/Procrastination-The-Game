using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject npc;
    
    public bool npcActive=false;

    public string guiText;
    public GameObject promptCanvas;
    public string sceneToLoad;

    public GUIStyle style;
    private bool showGui = false;

    public Color highlightColor = Color.yellow;
    private Color originalColor;

    private PlayerMovement player;
    private Prompt prompt;

    private Collider2D customCollider2D; // Cache the Collider2D component

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        originalColor = GetComponent<Renderer>().material.color;
        customCollider2D = GetComponent<Collider2D>(); // Get the Collider2D component
        prompt = FindObjectOfType<Prompt>();
        if(!GameManager.Instance.phoneFinished) {
            npc.SetActive(false);
            npcActive = false;
        }
        else {
            spawnNPC();
            npcActive = true;
        }
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
    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.phoneFinished) {
            Invoke("spawnNPC", 3f);
            npcActive = true;
        }

        if (PauseMenu.Instance.socializingDone)
        {
            customCollider2D.enabled = false; // Disable the Collider2D component
            this.enabled = false; // Disable the MinigameLoader script
            gameObject.SetActive(false);
        }
        else
        {
            if(player == null) {
                player = FindObjectOfType<PlayerMovement>();
            }
            if (showGui && Input.GetKeyDown(KeyCode.E)) 
            {   
                GameManager.Instance.socialSwitch = true;
                //GameManager.Instance.spawnPoint = newSpawnPoint.position;
                player.isKickboard = false;
                player.KickBoard();
                GameManager.Instance.sceneName = sceneToLoad;
                
                prompt.showPrompt();
            }
        }
    }


    public void Socialize() {
        //load socializing scene
    }

    public void spawnNPC() {
        npc.SetActive(true);
    }


}
