using UnityEngine;
using System.Collections.Generic; // Needed for the List

public class FishMovement : MonoBehaviour
{
    private GameObject fishPrefab;               // Prefab to spawn
    public float spawnRate = 1.8f;              // Time interval between spawns
    private float timer = 0;
    private float currentDepthY = -53.8f;       // Initial Y-depth for spawning fish
    private float depthOffsetY = 20f;           // Distance to move each fish down the Y-axis
    public float depthOffsetZ = 75f;            // Horizontal range offset for varied Z-axis spawning
    private float yPositionTolerance = 5f;      // Tolerance for Y-axis distance
    private float lateralTolerance = 10f;        // Tolerance for lateral (X and Z) distance
    public AudioSource bubble;
    public GameObject GoldFish;
    public GameObject BlueFish;
   

    private LogicScript logic;
    private Transform hookTransform;
    private List<GameObject> spawnedFish = new List<GameObject>(); // List to keep track of all spawned fish
    public GameObject[] fishArray;

    void Start()
    {
        logic = GameObject.FindObjectOfType<LogicScript>();
        hookTransform = GameObject.FindGameObjectWithTag("hook").transform;
        // Create an array of the two GameObjects
        fishArray = new GameObject[] { GoldFish, BlueFish };
    }

    void Update()
    {
        if (logic == null || logic.isGameOver) return; // Stop spawning if game is over

        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnFish();
            currentDepthY -= depthOffsetY;  // Move the next spawn position further down
            timer = 0;                      // Reset spawn timer
        }

        // Check all spawned fish
        CheckFishCatches();
    }

    void SpawnFish()
    {
        // Select a random index (0 or 1)
        int randomIndex = Random.Range(0, 2);

        // Access the randomly chosen GameObject
        fishPrefab = fishArray[randomIndex];

        // Log the selected object
        Debug.Log("Selected Object: " + fishPrefab.name);

        // Perform an action with the selected object
        fishPrefab.SetActive(true); // Example: Activate the selected object

        float randomZ = transform.position.z + Random.Range(-depthOffsetZ, depthOffsetZ);
        Vector3 spawnPosition = new Vector3(transform.position.x, currentDepthY, randomZ);

        GameObject spawnedFishObj = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
        // Check if the spawned fish is BlueFish and adjust its rotation and size
        if (fishPrefab == BlueFish)
        {
            spawnedFishObj.transform.Rotate(90f, 0f, -25f); // Rotate 90 degrees on the X-axis
            spawnedFishObj.transform.localScale /= 1.5f;   // Reduce size by 10 times
            spawnedFishObj.transform.position += new Vector3(0f, 0f, -10f);

        }
        else
        {
            spawnedFishObj.transform.localScale *= 5f;    // Adjust scale for other fish if necessary
        }

        spawnedFishObj.tag = "fish";

        // Add spawned fish to the list for later collision checking
        spawnedFish.Add(spawnedFishObj);
        Debug.Log($"Spawned {fishPrefab.name} at position {spawnedFishObj.transform.position}");
    }

    void CheckFishCatches()
    {
        if (hookTransform == null || logic == null) return;
       
        // Loop through all spawned fish
        foreach (GameObject fish in spawnedFish)
        {
            if (fish != null && IsWithinTolerance(fish.transform)) // Check if the fish is within tolerance of the hook
            {
                Debug.Log("Fish caught!");
                logic.AddScore(); // Add score for the catch
                Destroy(fish); // Destroy the fish once caught
                bubble.Play();
            }
            Debug.Log("Fish is not caught!");
        }
    }

    private bool IsWithinTolerance(Transform objectTransform)
    {
        Vector3 hookTopPosition = new Vector3(hookTransform.position.x, hookTransform.position.y - 10f, hookTransform.position.z);

        // Adjust tolerance to ensure that only relevant objects are caught
        float distanceX = Mathf.Abs(objectTransform.position.x - hookTopPosition.x);
        float distanceY = Mathf.Abs(objectTransform.position.y - hookTopPosition.y);
        float distanceZ = Mathf.Abs(objectTransform.position.z - hookTopPosition.z);

        // Apply tighter tolerance for the fish catch logic
        bool withinTolerance = distanceY < yPositionTolerance && distanceX < lateralTolerance && distanceZ < lateralTolerance;

        if (withinTolerance)
        {
            Debug.Log($"IsWithinTolerance: True");
        }
        return withinTolerance;
    }
    public void ResetFishSpawning()
    {
        foreach (var fish in spawnedFish)
        {
            if (fish != null)
            {
                Destroy(fish);
            }
        }
        spawnedFish.Clear(); // Clear the list
        currentDepthY = -53.8f; // Reset depth
        timer = 0f; // Reset timer
        lateralTolerance = 10f;
    }
}
