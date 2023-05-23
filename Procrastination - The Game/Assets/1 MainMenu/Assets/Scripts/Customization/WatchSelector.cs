using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WatchSelector : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Dropdown dropdown;
    [SerializeField] private List<GameObject> watches = new List<GameObject>();

    void Start()
    {
        dropdown.onValueChanged.AddListener(delegate { UpdateWatch(dropdown); });
    }

    public void UpdateWatch(TMPro.TMP_Dropdown change)
    {
        for (int i = 0; i < watches.Count; i++)
        {
            watches[i].SetActive(i == change.value);
        }
        GameTimer.Instance.SetSelectedWatch(change.value);  // Set selected watch
    }
}
