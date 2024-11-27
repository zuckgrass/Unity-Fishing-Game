using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class rockspawn : MonoBehaviour
{
    public GameObject rockPrefab;               // Prefab to spawn
    public float spawnRate = 1.8f;              // Time interval between spawns
    private float timer = 0;
    private float currentDepthY = -53.8f;       // Initial Y-depth for spawning rocks
    private float depthOffsetY = 20f;           // Distance to move each rock down the Y-axis
    public float depthOffsetZ = 75f;            // Horizontal range offset for varied Z-axis spawning
    private float yPositionTolerance = 5f;      // Tolerance for Y-axis distance
    private float lateralTolerance = 5f;        // Tolerance for lateral (X and Z) distance
    public AudioSource RockSound;

    private LogicScript logic;
    private Transform hookTransform;
    private List<GameObject> spawnedRocks = new List<GameObject>(); // List to keep track of all spawned rocks

    void Start()
    {
        logic = GameObject.FindObjectOfType<LogicScript>();
        hookTransform = GameObject.FindGameObjectWithTag("hook").transform;
    }

    void Update()
    {
        if (logic == null || logic.isGameOver) return; // Stop spawning if game is over

        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnRock();
            currentDepthY -= depthOffsetY;  // Move the next spawn position further down
            timer = 0;                      // Reset spawn timer
        }

        // Check all spawned rocks
        CheckRockCollisions();
    }

    void SpawnRock()
    {
        if (rockPrefab == null) return;

        float randomZ = transform.position.z + Random.Range(-depthOffsetZ, depthOffsetZ);
        Vector3 spawnPosition = new Vector3(transform.position.x, currentDepthY, randomZ);

        GameObject spawnedRock = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
        spawnedRock.tag = "rock";
        spawnedRock.transform.localScale *= 20; // Adjust scale if necessary

        // Add spawned rock to the list for later collision checking
        spawnedRocks.Add(spawnedRock);
    }

    void CheckRockCollisions()
    {
        if (hookTransform == null || logic == null) return;

        // Loop through all spawned rocks
        foreach (GameObject rock in spawnedRocks)
        {
            if (rock != null && IsWithinTolerance(rock.transform)) // Check if the rock is within tolerance of the hook
            {
                Debug.Log("Hook hit a rock! Game Over.");
                logic.GameOver(); // Trigger game over
                Destroy(rock); // Destroy the rock if it hits the hook
                RockSound.Play();
            }
            Debug.Log("Rock is not caught!");
        }
    }

    private bool IsWithinTolerance(Transform objectTransform)
    {
        Vector3 hookTopPosition = new Vector3(hookTransform.position.x, hookTransform.position.y - 10f, hookTransform.position.z);

        // Adjust tolerance to ensure that only relevant objects are caught
        float distanceX = Mathf.Abs(objectTransform.position.x - hookTopPosition.x);
        float distanceY = Mathf.Abs(objectTransform.position.y - hookTopPosition.y);
        float distanceZ = Mathf.Abs(objectTransform.position.z - hookTopPosition.z);

        // Apply tighter tolerance for the rock catch logic
        bool withinTolerance = distanceY < yPositionTolerance && distanceX < lateralTolerance && distanceZ < lateralTolerance;

        if (withinTolerance)
        {
            Debug.Log($"IsWithinTolerance: True");
        }
        return withinTolerance;
    }

    public void ResetRockSpawning()
    {
        foreach (var rock in spawnedRocks)
        {
            if (rock != null)
            {
                Destroy(rock);
            }
        }
        spawnedRocks.Clear(); // Clear the list
        currentDepthY = -53.8f; // Reset depth
        timer = 0f; // Reset timer
        lateralTolerance = 10f;
    }
}
