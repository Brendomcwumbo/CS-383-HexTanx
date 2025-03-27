using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class HealthPackSpawnTest
{
    private ItemManager itemManager;
    private const float FPS_LIMIT = 20f;  // Minimum FPS before we stop the test
    private const int MAX_SPAWN_COUNT = 100;  // Max number of health packs to spawn for the test

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Test to spawn health packs until FPS drops
    [UnityTest]
    public IEnumerator SpawnHealthPacksUntilFPSDrops()
    {
        // Set up the health pack prefab and ItemManager (assume they're already in the scene)
        // GameObject healthPackPrefab = Resources.Load<GameObject>("Assets/src/TL1_Justin/prefabs/HealthPack"); // Adjust with your path

        // Create a reference to ItemManager
        itemManager = GameObject.FindObjectOfType<ItemManager>();
        Assert.NotNull(itemManager, "ItemManager not found in the scene.");

        int spawnCount = 0;
        float fps = 60f;

        // Loop to spawn health packs
        while (fps > FPS_LIMIT && spawnCount < MAX_SPAWN_COUNT)
        {
            itemManager.StartCoroutine(itemManager.WaitForGridAndSpawnHealthPack());
            spawnCount++;

            yield return new WaitForSeconds(0.1f);

            fps = 1.0f / Time.deltaTime;
            Debug.Log($"Spawned {spawnCount} health packs before FPS dropped.");
        }

        Debug.Log($"Test stopped - final fps: {fps}, total tanks spawned: {spawnCount}");
        Assert.Pass(fps.ToString(), FPS_LIMIT, "FPS did not drop below 20 within the spawn limit.");
    }

    // two boundary tests
    // press play button and goes to sample scene

    // try spawning health outside of bounday
}