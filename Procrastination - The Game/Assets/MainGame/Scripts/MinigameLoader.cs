using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameLoader : MonoBehaviour
{
    private GameManager manager;
    public string guiText;
    public GameObject promptCanvas;
    public string sceneToLoad;

    public GUIStyle style;
    private bool showGui = false;

    public Color highlightColor = Color.yellow;
    private Color originalColor;
   
    

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        originalColor = GetComponent<Renderer>().material.color;
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
        if (showGui && Input.GetKeyDown(KeyCode.E)) 
        {   
            //GameManager.Instance.spawnPoint = newSpawnPoint.position;
            manager.sceneName = sceneToLoad;
            promptCanvas.SetActive(true);
        }
    }
}
