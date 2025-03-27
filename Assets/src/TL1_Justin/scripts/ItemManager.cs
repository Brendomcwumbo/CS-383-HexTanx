using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private PlaceTile placeTileScript;
    // private ItemManager itemManager;
    private static ItemManager instance;
    public GameObject healthPack;
    public List<GameObject> spawnedHealthPacks = new List<GameObject>();

    private bool isSpawningHealthPack = false; // Prevent multiple coroutines
 
    // playerTank.gameObject.tag = "PlayerTank";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("Duplicate ItemManager found. Destroying extra instance");
            // Destroy(gameObject);
            return;
        }
        instance = this;

        Debug.Log("Awake() called. Instance ID: " + gameObject.GetInstanceID());
        placeTileScript = FindFirstObjectByType<PlaceTile>();

        if (placeTileScript != null)
        {
            Debug.Log("PlaceTile script found in awake");
            StartCoroutine(WaitForGridAndSpawnHealthPack());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.GetComponent<Projectile>() != null)
        {
            Debug.Log("Projectile collided with item");

            PlayerTank playerTank = FindObjectOfType<PlayerTank>();

            if (playerTank != null)
            {
                Debug.Log("Player health before health pack: " + playerTank.GetHealth());
                playerTank.SetHealth(100);
                Debug.Log("Player health after health pack: " + playerTank.GetHealth());
                Debug.Log("Player health set to 100");
            }
            else
            {

                Debug.Log("No PlayerTank found in scene");
            }

            Destroy(gameObject);
        }
    }

    public IEnumerator WaitForGridAndSpawnHealthPack()
    {
        if (isSpawningHealthPack) yield break; // Stop if already running
        isSpawningHealthPack = true;           // Mark as running

        while (placeTileScript.Grid == null || placeTileScript.Grid.GetLength(0) == 0)
        {
            Debug.Log("Waiting for grid to be populated...");
            yield return new WaitForSeconds(0.1f); // Check every 0.1 seconds
        }

        Debug.Log("Grid is finally populated");

        int width = placeTileScript.width;
        int height = placeTileScript.height;

        Debug.Log("Grid width is: " + width);
        Debug.Log("Grid height is: " + height);

        int randX = Random.Range(0, width);
        int randY = Random.Range(0, height);
        Debug.Log($"Random health pack locations are {randX} and {randY}");
        Vector3 healthPackPos = placeTileScript.Grid[randX, randY];
        healthPackPos.z = -1f; // Ensure health pack appears above the tiles

        Debug.Log("Spawning health pack at: " + healthPackPos);

        Debug.Log("HealthPack reference: " + (healthPack != null ? "Assigned" : "NULL"));

        // Instantiate(healthPack, healthPackPos, Quaternion.identity);
        GameObject spawnedHealthPack = Instantiate(healthPack, healthPackPos, Quaternion.identity);
        Debug.Log("Health pack instantiated at: " + spawnedHealthPack.transform.position);

        spawnedHealthPacks.Add(spawnedHealthPack);

        isSpawningHealthPack = false; // Reset flag after completion
    }
}
