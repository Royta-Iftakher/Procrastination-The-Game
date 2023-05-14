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
        if (spawnPoint != null &&GameManager.Instance.spawned == false)
        {
            GameManager.Instance.spawned = true;
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
