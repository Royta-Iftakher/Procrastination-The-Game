using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameController manager;    // Reference to the game controller script
    public GameObject prefab;    // The prefab object to be spawned
    public float spawnRate = 1f;    // The rate at which objects will be spawned
    public float minHeight = -1f;    // The minimum height at which the object can spawn
    public float maxHeight = 2f;    // The maximum height at which the object can spawn
    public float pipeGap = 2f;
    private float maxGapSize = 11f;
    private bool hasIncreasedGap = false;
    private float currentGapSize = 9f;

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

    private void Spawn() //stops spawning pipes after the score hits 7 + if player fails a lot of times, the gap between pipes get bigger
    {
        if (manager.score < 7) 
        {
            GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);
            pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
            Transform topPipe = pipes.transform.Find("Top Pipe");
            Transform bottomPipe = pipes.transform.Find("Bottom Pipe");
            
            int currentFails = manager.fails % 10; // get the current number of fails (up to 10)
            
            if (currentFails > 0 && currentFails % 5 == 0) // check if current fails is a multiple of 5
            {
                if (!hasIncreasedGap)
                {
                    currentGapSize += .5f;
                    hasIncreasedGap = true;
                }
            }
            else
            {
                hasIncreasedGap = false;
            }
            
            if (currentGapSize > 9 && currentGapSize < maxGapSize) // make sure the new gap size doesn't exceed the maximum or go below the minimum
            {
                float currentGap = Mathf.Abs(topPipe.position.y - bottomPipe.position.y);
                float newGap = currentGapSize - currentGap;
                
                topPipe.position += Vector3.up * (newGap / 2f);
                bottomPipe.position += Vector3.down * (newGap / 2f);
                
                Debug.Log($"Gap increased by {newGap}, new gap size: {currentGapSize}");
            }  
        }
    }
}
