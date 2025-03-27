using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class playerSpawnTest
{
    private PlayerTankSpawner playerTankSpawner;
    private const float FPS_LIMIT = 20f; // Stop spawning when FPS drops below this
    private const int MAX_SPAWN_COUNT = 500000000; // Safety limit to prevent infinite loops

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    [UnityTest]
    public IEnumerator SpawnTanks_UntilFPSDrops()
    {
        playerTankSpawner = GameObject.FindObjectOfType<PlayerTankSpawner>();
        Assert.NotNull(playerTankSpawner, "playerTankSpawner not found in scene!");

        int tankCount = 0;
        float fps = 60f; // Start with high FPS

        while (fps > FPS_LIMIT && tankCount < MAX_SPAWN_COUNT)
        {
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;
            playerTankSpawner.SpawnPlayerTankWithRandomPosition();
            tankCount++;

            yield return new WaitForSeconds(0.1f); // Allow FPS to update

            fps = 1.0f / Time.deltaTime; // Calculate FPS
            Debug.Log($"Spawned Tanks: {tankCount}, FPS: {fps}");
            if (fps < FPS_LIMIT)
            {
                yield return new WaitForSeconds(1f);
                Assert.Fail("FPS below 20");
            }
        }

        Debug.Log($"Test Stopped - Final FPS: {fps}, Total Tanks Spawned: {tankCount}");
        Assert.Pass("FPS did not drop below 40 within the spawn limit.");
    }
}
