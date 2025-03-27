using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class EnemyTankSpawnTest
{
    private EnemyTankSpawner enemyTankSpawner;
    private const float FPS_LIMIT = 40f; // Stop spawning when FPS drops below this
    private const int MAX_SPAWN_COUNT = 500; //Safeyly Limit to prevent infinite loops

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    [UnityTest]
    public IEnumerator SpawnTanks_UntilFPSDrops()
    {
        enemyTankSpawner = GameObject.FindObjectOfType<EnemyTankSpawner>();
        Assert.NotNull(enemyTankSpawner, "EnemyTankSpawner not found in scene!");

        int tankCount = 0;
        float fps = 60f; // Start with high FPS

        while (fps > FPS_LIMIT && tankCount < MAX_SPAWN_COUNT)
        {
            enemyTankSpawner.SpawnEnemyTankWithRandomPosition();
            tankCount++;

            yield return new WaitForSeconds(.1f); // Allow FPS to update

            fps = 1.0f / Time.deltaTime; // Calculate FPS
            Debug.Log($"Tank No: {tankCount}, FPS: {fps}");
            if(fps < FPS_LIMIT)
            {
                yield return new WaitForSeconds(1f);
                Assert.Fail("FPS below 40");
            }
        }

        Debug.Log($"Test Stopped - Final FPS: {fps}, Total Tanks Spawned: {tankCount}");
        Assert.Pass("FPS did not drop below 40 within the spawn limit.");
    }
}
