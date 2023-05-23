using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Add this to use TextMeshPro

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText; // Reference to your TMP text

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        itemImage.enabled = false;
        itemText.enabled = false; // Initially hide the text as well
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.outside) {
            HideItemImage();
        }
    }

    public void ShowItemImage() {
        itemImage.enabled = true;
        itemText.enabled = true; // Show the text when the item is shown
    }

    public void HideItemImage() {
        itemImage.enabled = false;
        itemText.enabled = false; // Hide the text when the item is hidden
    }
}
