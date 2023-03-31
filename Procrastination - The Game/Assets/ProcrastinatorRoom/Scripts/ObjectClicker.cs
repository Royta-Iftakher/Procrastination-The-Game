using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    public GameObject promptCanvas;

    private void OnMouseDown()
    {
        promptCanvas.SetActive(true);
    }
}
