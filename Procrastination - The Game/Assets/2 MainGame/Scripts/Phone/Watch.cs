using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Watch : MonoBehaviour
{
    // List of different watch images, assuming each watch is a different sprite
    [SerializeField] private List<Sprite> watchImages = new List<Sprite>();

    // Reference to the image component where the sprite will be applied
    private Image watchImage;

    private void Awake()
    {
        watchImage = GetComponent<Image>();
    }

    public void SetWatch(int index)
    {
        if (index < 0 || index >= watchImages.Count)
        {
            Debug.LogError("Invalid index for watch: " + index);
            return;
        }

        watchImage.sprite = watchImages[index];
    }
}
