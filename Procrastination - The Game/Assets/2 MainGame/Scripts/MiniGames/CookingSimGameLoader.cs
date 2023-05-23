using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

    // Add reference to the TextMeshProUGUI component
    public TextMeshProUGUI popupText;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        originalColor = GetComponent<Renderer>().material.color;
        customCollider2D = GetComponent<Collider2D>(); // Get the Collider2D component
        prompt = FindObjectOfType<Prompt>();

        // Hide the popup text at start
        HidePopupText();
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
            // Disable the CookingSimGameLoader script
        }
        else
        {
            customCollider2D.enabled = true;
            if (player == null)
            {
                player = FindObjectOfType<PlayerMovement>();
            }
            if (showGui && Input.GetKeyDown(KeyCode.E))
            {
                //GameManager.Instance.spawnPoint = newSpawnPoint.position;
                player.isKickboard = false;
                player.KickBoard();
                GameManager.Instance.sceneName = sceneToLoad;

                if (!Phone.Instance.inCall)
                {
                    prompt.showPrompt();
                    
                }
                else
                {
                    StartCoroutine(ShowPopupText("Finish the Call", 2f)); // Show the popup text for 2 seconds
                }
            }
        }
    }

    IEnumerator ShowPopupText(string text, float duration)
    {
        // Set the popup text
        popupText.text = text;
        // Show the popup text
        popupText.gameObject.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Hide the popup text
        HidePopupText();
    }

    void HidePopupText()
    {
        // Hide the popup text
        popupText.gameObject.SetActive(false);
    }
}
