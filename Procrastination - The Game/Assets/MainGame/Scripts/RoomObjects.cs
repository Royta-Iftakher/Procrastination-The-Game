using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomObjects : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    public string sceneName;
    private Color originalColor;
    private bool isMouseOver = false;

    private void Start()
    {
        originalColor = GetComponent<Renderer>().material.color;
    }

    private void OnMouseEnter()
    {
        isMouseOver = true;
        GetComponent<Renderer>().material.color = highlightColor;
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
        GetComponent<Renderer>().material.color = originalColor;
    }

    private void OnMouseDown()
    {
        if (isMouseOver)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }
}
