using UnityEngine;

public class EnemyTankSpawner : MonoBehaviour
{
    private PlaceTile placeTileScript;
    private TankManager tankManager;
    private GameObject enemyTank;

    void Start()
    {
        placeTileScript = FindObjectOfType<PlaceTile>();
        tankManager = FindObjectOfType<TankManager>();

        if (tankManager == null)
        {
            Debug.LogError("EnemyTankSpawner: No TankManager found in the scene!");
            return;
        }

        if (placeTileScript != null)
        {
            //Invoke("SpawnEnemyTankWithRandomPosition", 0.1f);
        }
        else
        {
            Debug.LogError("PlaceTile script not found in the scene!");
        }
    }

    public void SpawnEnemyTankWithRandomPosition()
    {
        if (placeTileScript.Grid == null || placeTileScript.Grid.GetLength(0) == 0)
        {
            Debug.LogError("Grid is not populated properly.");
            return;
        }

        int width = placeTileScript.width;
        int height = placeTileScript.height;

        int randX = Random.Range(0, width);
        int randY = Random.Range(0, height);
        Vector3 enemyPos = placeTileScript.Grid[randX, randY];
        enemyPos.z = -1f; // Ensure the enemy appears above the tiles

        SpawnEnemyTank(enemyPos);
    }

    public void SpawnEnemyTank(Vector3 spawnLocation)
    {
        if (tankManager == null)
        {
            Debug.LogError("EnemyTankSpawner: No TankManager assigned!");
            return;
        } // Prevent errors if TankManager is missing
        GameObject tankPrefab = tankManager.GetRandomTank();
        if (tankPrefab == null)
        {
            Debug.LogError("EnemyTankSpawner: No tank prefab returned from TankManager!");
            return;
        }
        //Debug.Log("Spawning an enemy tank at: " + spawnLocation);  //  Confirm method runs

        GameObject spawnedTank = Instantiate(tankPrefab, spawnLocation, Quaternion.identity);
        TankType tank = spawnedTank.GetComponent<TankType>();

        if (tank != null)
        {
            tank.Initialize();// Call Initialize() since we made it parameterless
            tank.UpdateTankLocation(spawnLocation);
            //Debug.Log("Enemy tank initialized successfully!");
        }
        else
        {
            Debug.LogError("Spawned object does not have a TankType component!");
        }

    }
}

