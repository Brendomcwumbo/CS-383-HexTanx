using UnityEngine;

public abstract class TankType : MonoBehaviour
{
    public int health;              //Store tanks health
    public Vector3 tankLocation;    //Store tanksLocation
    public int damage;

    private int decisionToMoveOrShoot;  // 0 = Move, 1 = Shoot
    private int moveDirectionIndex;     // Randomized movement direction
    private int shotAccuracy;           // 0 = Miss, 1 = Hit

    public abstract void Initialize();
    

    public void GenerateRandomDecisions()
    {
        decisionToMoveOrShoot = Random.Range(0, 2);
        moveDirectionIndex = Random.Range(0, 6);  // Assuming 6 hex directions
        shotAccuracy = Random.Range(0, 2);
    }

    public bool ShouldShoot() => decisionToMoveOrShoot == 1;
    public bool ShotHitsPlayer() => shotAccuracy == 1;

    public void UpdateTankLocation(Vector3 newLocation)
    {
        tankLocation = newLocation;
        transform.position = newLocation;
    }
    
}
