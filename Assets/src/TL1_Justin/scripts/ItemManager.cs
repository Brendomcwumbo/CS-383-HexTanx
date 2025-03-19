using System.Collections;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private PlaceTile placeTileScript;
    private ItemManager itemManager;
    public GameObject healthPack;

    private bool isSpawningHealthPack = false; // Prevent multiple coroutines

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
        if (collision.CompareTag("PlayerTank"))
        {
            Debug.Log("Player collided with item");

            Destroy(gameObject);
        }
    }

    private IEnumerator WaitForGridAndSpawnHealthPack()
    {
        if (isSpawningHealthPack) yield break; // Stop if already running
        isSpawningHealthPack = true;           // Mark as running

        if (placeTileScript.Grid == null || placeTileScript.Grid.GetLength(0) == 0)
        {
            Debug.Log("Waiting for grid to be populated...");
            yield return new WaitForSeconds(0.1f); // Check every 0.1 seconds
        }

        Debug.Log("Grid is finally populated");

        int width = placeTileScript.width;
        int height = placeTileScript.height;

        Debug.Log("Grid width is: " + width);
        Debug.Log("Grid height is: " + height);

        isSpawningHealthPack = false; // Reset flag after completion
    }
}
