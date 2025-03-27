using UnityEngine;

public class PlayerTankSpawner : MonoBehaviour
{
    private PlaceTile placeTileScript;
    public GameObject playerTankPrefab; 

    void Start()
    {
        placeTileScript = FindObjectOfType<PlaceTile>();

        if (placeTileScript != null)
        {
            //Invoke("SpawnPlayerTankWithPosition", 0.0f); 
        }
        else
        {
            Debug.LogError("PlayerTankSpawner: PlaceTile script not found in the scene!");
        }
    }

    public void SpawnPlayerTankWithPosition()
    {
        Debug.Log("SpawnPlayerTankWithPosition() called!");
        if (placeTileScript.Grid == null || placeTileScript.Grid.GetLength(0) == 0)
        {
            Debug.LogError("Grid is not populated properly.");
            return;
        }

        int width = placeTileScript.width;
        int height = placeTileScript.height;

        // Spawn at a fixed position 
        int spawnX = width / 2;
        int spawnY = 1;         
        Vector3 playerPos = placeTileScript.Grid[spawnX, spawnY];
        playerPos.z = -1f;
        Debug.Log($"Calling SpawnPlayerTank() at {playerPos}");
        SpawnPlayerTank(playerPos);
    }

    public void SpawnPlayerTankWithRandomPosition() // New method for test only
    {
        if (placeTileScript == null) placeTileScript = FindObjectOfType<PlaceTile>();

        if (placeTileScript.Grid == null || placeTileScript.Grid.GetLength(0) == 0 || placeTileScript.Grid.GetLength(1) == 0)
        {
            Debug.LogError("Grid is not populated properly.");
            return;
        }

        int width = placeTileScript.width;
        int height = placeTileScript.height;

        // Spawn at a random position within grid bounds
        int spawnX = Random.Range(0, width); // 0 to width-1
        int spawnY = Random.Range(0, height); // 0 to height-1
        Vector3 playerPos = placeTileScript.Grid[spawnX, spawnY];
        playerPos.z = -1f;
        SpawnPlayerTank(playerPos);
    }

    public void SpawnPlayerTank(Vector3 spawnLocation)
    {
        Debug.Log("SpawnPlayerTank() called!");
        if (playerTankPrefab == null)
        {
            Debug.LogError("PlayerTankSpawner: No player tank prefab assigned!");
            return;
        }

        Debug.Log("Spawning player tank at: " + spawnLocation); 

        GameObject spawnedTank = Instantiate(playerTankPrefab, spawnLocation, Quaternion.identity);
        spawnedTank.name = "PlayerTank"; 

        PlayerMovement movement = spawnedTank.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            Debug.Log("Player tank movement component found");
        }
        else
        {
            Debug.LogWarning("Player tank spawned but lacks PlayerMovement component");
        }
    }
}