using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class HealthPackSpawnBoundaryTest1
{
    private ItemManager itemManager;
    private PlaceTile placeTileScript;

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // A Test behaves as an ordinary method
    // [Test]
    // public void HealthPackSpawnBoundaryTest1SimplePasses()
    // {
    //     // Use the Assert class to test conditions
    // }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator HealthPackSpawnBoundaryTest1WithEnumeratorPasses()
    {
        itemManager = GameObject.FindObjectOfType<ItemManager>();
        Assert.NotNull(itemManager, "ItemManager not found in the scene");

        itemManager.spawnedHealthPacks.Clear();

        placeTileScript = GameObject.FindObjectOfType<PlaceTile>();
        Assert.NotNull(placeTileScript, "PlaceTile not found in the scene");

        yield return new WaitForSeconds(0.1f);

        // Grid demensions
        int gridWidth = placeTileScript.width;
        int gridHeight = placeTileScript.height;

        // Track spawn locations
        Vector3 spawnPos = Vector3.zero;

        yield return itemManager.StartCoroutine(itemManager.WaitForGridAndSpawnHealthPack());

        // Ensure at least one health pack was spawned
        Assert.IsTrue(itemManager.spawnedHealthPacks.Count > 0, "No health packs spawned.");

        // Get the last spawned health pack
        GameObject spawnedHealthPack = itemManager.spawnedHealthPacks[0];
        Assert.NotNull(spawnedHealthPack, "Health pack was not spawned");

        if (spawnedHealthPack != null)
        {
            spawnPos = spawnedHealthPack.transform.position;
            Debug.Log($"Health pack spawned at position: {spawnedHealthPack.transform.position}");

            // Ensure the health pack spawn location is within bounds
            Assert.IsTrue(spawnPos.x >= 0 && spawnPos.x < gridWidth, "Health pack spawned outside the X grid boundary.");
            Assert.IsTrue(spawnPos.y >= 0 && spawnPos.y < gridHeight, "Health pack spawned outside the Y grid boundary.");
        }
        else
        {
            Assert.Fail("Health pack was not spawned");
        }
    }
}
