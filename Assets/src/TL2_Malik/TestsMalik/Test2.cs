/*
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class Tests
{
    // SetUp is called before each test
    [SetUp]
    public void LoadTestScene()
    {
        // Load the scene before each test
        SceneManager.LoadScene("SampleScene");
    }

    // UnityTest will automatically wait for a yield return, so this works well for async tests
    [UnityTest]
    public IEnumerator TestsWithEnumeratorPasses()
    {
        // Wait until the scene is loaded
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "SampleScene");

       
        var tileManager = GameObject.FindObjectOfType<PlaceTile>();
        Assert.IsNotNull(tileManager, "TileManager not found in the scene");

        // Loop through the map sizes and test
        for (int i = 1; i < 100; ++i)
        {
            for (int t = 1; t < 100; ++t)
            {
                tileManager.MakeMap(i, t);
                yield return null; // Yield to prevent the test from running too quickly (helpful for larger tests)
            }
        }

        // Add some validation after your test
        Assert.Pass("Test completed successfully");
    }
}
*/
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlaceTileTests
{
    private GameObject placeTileObject;
    private PlaceTile placeTileScript;
    private GameObject tilePrefab;
    private GameObject terrainPrefab;

    [SetUp]
    public void Setup()
    {
        GameObject cameraObject = new GameObject("MainCamera");
        Camera camera = cameraObject.AddComponent<Camera>();
        camera.tag = "MainCamera";
        // Create a GameObject for testing and add the PlaceTile component to it
        placeTileObject = new GameObject();
        placeTileScript = placeTileObject.AddComponent<PlaceTile>();

        // Create mock Tile and Terrain prefabs
        tilePrefab = new GameObject("Tile");
        terrainPrefab = new GameObject("Terrain");

        // Assign the prefabs to the PlaceTile script
        placeTileScript.Tile = tilePrefab;
        placeTileScript.Terrain = terrainPrefab;

        // Optionally, you can assign random values for min/max
        placeTileScript.min = 5;
        placeTileScript.max = 5;
    }

    [UnityTest]
    public IEnumerator MakeMap_CreatesGridCorrectly()
    {
        placeTileScript.width = 5;
        placeTileScript.height = 5;
        placeTileScript.Grid = null;

        // Verify the Grid is null before making the map
        Assert.IsNull(placeTileScript.Grid, "Grid should be null before creating the map");

        // Call the method to create the map
        placeTileScript.MakeMap(placeTileScript.width, placeTileScript.height);

        // Wait for one frame to let Unity process the instantiation
        yield return null;

        // Ensure the Grid is populated after map creation
        Assert.IsNotNull(placeTileScript.Grid, "Grid should not be null after calling MakeMap");

        // Verify the Grid dimensions
        Assert.AreEqual(5, placeTileScript.Grid.GetLength(0), "Grid width is incorrect");
        Assert.AreEqual(5, placeTileScript.Grid.GetLength(1), "Grid height is incorrect");

        // Verify that the correct number of tiles/terrain have been instantiated
        int tileCount = GameObject.FindGameObjectsWithTag("Tile").Length;
        int terrainCount = GameObject.FindGameObjectsWithTag("Terrain").Length;
        
        //Assert.AreEqual(25, tileCount + terrainCount, "Total number of tiles and terrain is incorrect");

        // Optional: You can also check if specific positions are filled with tiles or terrain
        // For example, check if some specific spots in the Grid contain terrain or tiles
        bool foundTerrain = false;
        for (int x = 0; x < placeTileScript.width; ++x)
        {
            for (int y = 0; y < placeTileScript.height; ++y)
            {
                if (placeTileScript.Grid[x, y] != Vector3.zero)
                {
                    if (placeTileScript.Grid[x, y] != null)
                    {
                        foundTerrain = true;
                    }
                }
            }
        }

        Assert.IsTrue(foundTerrain, "No terrain was found in the grid");
        
        /*
        // Check the camera position
        Vector3 expectedCameraPosition = new Vector3(2.0f, 2.0f, -10);
        Assert.AreEqual(expectedCameraPosition, placeTileScript.cam.transform.position, "Camera position is incorrect after map creation");
        */
    }

     [UnityTest]
    public IEnumerator StressTest_LargeMapInstantiation()
    {
        // Start measuring time
        float startTime = Time.realtimeSinceStartup;

        // Use a very large grid size for the stress test (e.g., 500x500)
        placeTileScript.width = 500;
        placeTileScript.height = 500;
        placeTileScript.Grid = null;

        // Call the method to create the map
        placeTileScript.MakeMap(placeTileScript.width, placeTileScript.height);

        // Wait for the instantiation to complete (allow Unity to process)
        yield return null;  // Yield once to ensure the first frame is processed
        yield return null;  // Yield again to let more objects get instantiated

        // Measure the time after the process is completed
        float endTime = Time.realtimeSinceStartup;

        // Log the time taken to instantiate the objects
        float duration = endTime - startTime;
        Debug.Log($"Stress Test: Time taken to instantiate the map: {duration} seconds.");

        // Assert that the map was created
        Assert.AreEqual(placeTileScript.width, placeTileScript.Grid.GetLength(0), "Grid width is incorrect");
        Assert.AreEqual(placeTileScript.height, placeTileScript.Grid.GetLength(1), "Grid height is incorrect");

        // Ensure the total number of tiles/terrain is as expected (width * height)
        int tileCount = GameObject.FindGameObjectsWithTag("Tile").Length;
        int terrainCount = GameObject.FindGameObjectsWithTag("Terrain").Length;
        //Assert.AreEqual(placeTileScript.width * placeTileScript.height, tileCount + terrainCount, "Total number of tiles and terrain is incorrect");

        // Log the count of instantiated objects
        //Debug.Log($"Number of Tiles instantiated: {tileCount}");
        //Debug.Log($"Number of Terrain instantiated: {terrainCount}");

        // Optional: Check if the frame rate dropped below a threshold (e.g., 30 FPS) during the test
        float frameRate = 1 / Time.unscaledDeltaTime;
        Debug.Log($"Frame rate during stress test: {frameRate} FPS");
        Assert.Greater(frameRate, 30, "Frame rate dropped below 30 FPS during stress test.");
    }

    [TearDown]
    public void Teardown()
    {
        // Clean up after each test
        Object.Destroy(placeTileObject);
        Object.Destroy(tilePrefab);
        Object.Destroy(terrainPrefab);
    }
}


