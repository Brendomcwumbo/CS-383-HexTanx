using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class PlayerTankMovementTest
{
    private GameObject playerTank;
    private PlayerMovement playerMovement;
    private Vector3 initialPosition;

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    [UnityTest]
    public IEnumerator PlayerTank_MovesAfterRandomClick()
    {
        // Find the player tank
        playerTank = GameObject.Find("PlayerTank");  
        Assert.NotNull(playerTank, "Player tank not found in the scene!");

        playerMovement = playerTank.GetComponent<PlayerMovement>();
        Assert.NotNull(playerMovement, "PlayerMovement component not found on player tank!");

        Camera mainCamera = Camera.main;
        Assert.NotNull(mainCamera, "Main camera not found in the scene!");

        // Get PlaceTile for tile validation
        PlaceTile placeTile = GameObject.FindObjectOfType<PlaceTile>();
        Assert.NotNull(placeTile, "PlaceTile not found in the scene!");
        Debug.Log($"PlaceTile found with grid size: {placeTile.width}x{placeTile.height}");

        // Store initial position
        initialPosition = playerTank.transform.position;
        yield return new WaitForSeconds(0.5f); 
        Debug.Log("Initial position: " + initialPosition);

     
      
        // Generate random screen position
        Vector3 randomScreenPos = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 0);
        Debug.Log($"Random screen position: {randomScreenPos}");

        // Convert to world space
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(randomScreenPos);
        worldPos.z = -1f;  // Match tank’s Z
        Debug.Log($"World position: {worldPos}");

        // Move tank directly using FindNearestTile
       // playerTank.transform.position = playerMovement.FindNearestTile(worldPos);
        Debug.Log($"Tank moved to: {playerTank.transform.position}");

        yield return new WaitForSeconds(1.0f);  // Wait one frame
     

        Vector3 newPosition = playerTank.transform.position;
        Debug.Log($"Initial Position: {initialPosition}, New Position: {newPosition}");

        // Check if the position has changed
        Assert.AreNotEqual(initialPosition, newPosition, "Player tank did not move!");

        // Verify final position is on a tile
        bool isOnTile = false;
        for (int x = 0; x < placeTile.width; x++)
        {
            for (int y = 0; y < placeTile.height; y++)
            {
                Vector3 gridPos = placeTile.Grid[x, y];
                gridPos.z = -1f;
                if (Vector3.Distance(newPosition, gridPos) < 0.1f)
                {
                    isOnTile = true;
                    break;
                }
            }
            if (isOnTile) break;
        }
        Debug.Log($"Tank check: Is on tile: {isOnTile}");
        Assert.IsTrue(isOnTile, $"New position {newPosition} is not on a tile!");
        if (isOnTile)
        {
            Assert.Pass("Player Tank moved in Bounds");
        }
    }
}