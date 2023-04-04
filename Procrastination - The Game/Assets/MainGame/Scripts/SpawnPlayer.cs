using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private Transform doorTransform;

    private void Start()
    {
        GameManager manager = FindObjectOfType<GameManager>();
        if (spawnPoint != null && manager != null &&manager.spawned == false)
        {
            manager.spawned = true;
            Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
            Destroy(spawnPoint);
        }
        else
        {
            Vector3 spawnPosition = doorTransform.position + new Vector3(0f, -1.5f, 0f);
            Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
