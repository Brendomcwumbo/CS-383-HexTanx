using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class SpawnBoundsTest
{
    private EnemyTankSpawner enemyTankSpawner;
    private PlaceTile placeTileScript;

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    [UnityTest]
    public IEnumerator SpawnTanks_WithinMapBounds()
    {
        yield return new WaitForSeconds(1f); // Wait for scene to load properly

        enemyTankSpawner = GameObject.FindObjectOfType<EnemyTankSpawner>();
        placeTileScript = GameObject.FindObjectOfType<PlaceTile>();

        Assert.NotNull(enemyTankSpawner, "EnemyTankSpawner not found in scene!");
        Assert.NotNull(placeTileScript, "PlaceTile script not found in scene!");

        int initialTankCount = GameObject.FindGameObjectsWithTag("EnemyTank").Length;

        // Spawn a new enemy tank
        enemyTankSpawner.SpawnEnemyTankWithRandomPosition();
        yield return new WaitForSeconds(0.5f); // Allow time for spawning

        // Find the newly spawned enemy tank
        GameObject[] allTanks = GameObject.FindGameObjectsWithTag("EnemyTank");
        Assert.Greater(allTanks.Length, initialTankCount, "No new tank was spawned!");

        GameObject spawnedTank = null;
        foreach (GameObject tank in allTanks)
        {
            if (!tank.activeSelf) continue; // Skip inactive tanks
            if (tank.transform.position.z == -1f) // Ensure it's a newly spawned tank
            {
                spawnedTank = tank;
                break;
            }
        }

        Assert.NotNull(spawnedTank, "Failed to detect the newly spawned enemy tank!");

        Vector3 tankPosition = spawnedTank.transform.position;
        float minX = placeTileScript.Grid[0, 0].x;
        float minY = placeTileScript.Grid[0, 0].y;
        float maxX = placeTileScript.Grid[placeTileScript.width - 1, placeTileScript.height - 1].x;
        float maxY = placeTileScript.Grid[placeTileScript.width - 1, placeTileScript.height - 1].y;

        bool withinBounds = tankPosition.x >= minX && tankPosition.x <= maxX &&
                            tankPosition.y >= minY && tankPosition.y <= maxY;
        yield return new WaitForSeconds(2f);
        Assert.IsTrue(withinBounds, $"Tank spawned out of bounds at {tankPosition}");
        if(withinBounds)
        {
            Assert.Pass("Tank Spawned in Bounds");
        }
    }
}
