using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameController manager;    // Reference to the game controller script
    public GameObject prefab;    // The prefab object to be spawned
    public float spawnRate = 1f;    // The rate at which objects will be spawned
    public float minHeight = -1f;    // The minimum height at which the object can spawn
    public float maxHeight = 2f;    // The maximum height at which the object can spawn
    
    private void Start() {
        manager = FindObjectOfType<GameController>();    // Find and assign the game controller script
    }
    
    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);    // Invoke the Spawn() method repeatedly with a delay of spawnRate seconds
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));    // Cancel the Spawn() method invocation
    }

    private void Spawn()
    {
        if(manager.score < 8) {    // Spawn only if the score is less than 8
            GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);    // Spawn the prefab object at the spawner's position
            pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);    // Move the spawned object randomly within the specified height range
        }
    }
}
