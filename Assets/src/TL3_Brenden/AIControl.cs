using UnityEngine;

public class AIControl : MonoBehaviour
{
    private TankType tank;
    private PlaceTile placeTileScript;

    public void Start()
    {
        tank = GetComponent<TankType>();
        if (tank == null)
        {
            Debug.LogError("AIControl: No TankType found on " + gameObject.name);
            InvokeRepeating(nameof(MakeDecision), 1.0f, 2.0f);
        }
        // Get a reference to the PlaceTile script
        placeTileScript = FindObjectOfType<PlaceTile>();
        if (placeTileScript == null)
        {
            Debug.LogError("AIControl: No PlaceTile found in the scene!");
        }
    }


    public void MakeDecision()
    {
        if (tank == null) return;

        tank.GenerateRandomDecisions();

        if (tank.ShouldShoot())
        {
            ShootAtPlayer();
        }
        else
        {
            MoveToNewLocation();
        }
    }

    private void ShootAtPlayer()
    {
        if (tank.ShotHitsPlayer())
        {
            Debug.Log(gameObject.name + " shot hit the player!");
            // Implement damage logic for player
        }
        else
        {
            Debug.Log(gameObject.name + " shot missed!");
        }
    }

    public void MoveToNewLocation()
    {
        Vector3 newLocation = GetRandomAdjacentHex();
        if (IsWithinMapBounds(newLocation)) // Check if the new location is within the grid bounds
        {
            tank.UpdateTankLocation(newLocation);
            Debug.Log(gameObject.name + " moved to " + newLocation);
        }
        else
        {
            Debug.Log(gameObject.name + " tried to move out of bounds, staying in place." + newLocation);
        }
    }

    private Vector3 GetRandomAdjacentHex()
    {
        float hexHeight = Mathf.Sqrt(3) / 2f;
        Vector3[] possibleMoves = {
            new Vector3(tank.tankLocation.x + 1f, tank.tankLocation.y, -1), //East
            new Vector3(tank.tankLocation.x - 1f, tank.tankLocation.y, -1), //West
            new Vector3(tank.tankLocation.x + 0.5f, tank.tankLocation.y + hexHeight, -1), // NorthEast
            new Vector3(tank.tankLocation.x - 0.5f, tank.tankLocation.y + hexHeight, -1), //NorthWest
            new Vector3(tank.tankLocation.x + 0.5f, tank.tankLocation.y - hexHeight, -1), //SouthEast
            new Vector3(tank.tankLocation.x - 0.5f, tank.tankLocation.y - hexHeight, -1), //SouthWest
        };

        return possibleMoves[Random.Range(0, possibleMoves.Length)];
    }
    private bool IsWithinMapBounds(Vector3 position)
    {
        float hexHeight = Mathf.Sqrt(3) / 2f;
        if (position.x > 0f && position.x <= placeTileScript.width && position.y >= 0f && position.y < (placeTileScript.height - hexHeight))
        {
            return true;
        }

        return false;
    }
}
