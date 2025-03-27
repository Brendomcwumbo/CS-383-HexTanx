using UnityEngine;

public class PlayerTank : MonoBehaviour
{
    private int health = 100;       // Default health
    private int ammoCount = 50;     // Default ammo
    private int armorCount = 30;    // Default armor
    private Vector3 tankLocation;   // Position of the tank
    private int actionPoints = 3;   // Default action points (e.g., 3 per turn)

    void Start()
    {
        tankLocation = transform.position;
    }

    // Getters
    public int GetHealth()
    {
        return health;
    }

    public int GetAmmoCount()
    {
        return ammoCount;
    }

    public int GetArmorCount()
    {
        return armorCount;
    }

    public Vector3 GetTankLocation()
    {
        return tankLocation;
    }

    public int GetActionPoints()
    {
        return actionPoints;
    }

    // Setters
    public void SetHealth(int newHealth)
    {
        health = newHealth;
    }

    public void SetAmmoCount(int newAmmo)
    {
        ammoCount = newAmmo;
        if (ammoCount < 0)
        {
            ammoCount = 0;
        }
    }

    public void SetArmorCount(int newArmor)
    {
        armorCount = newArmor;
        if (armorCount < 0)
        {
            armorCount = 0;
        }
    }

    public void SetTankLocation(Vector3 newLocation)
    {
        tankLocation = newLocation;
        transform.position = newLocation;
        Debug.Log("Tank location set to " + newLocation);
    }

    public void SetActionPoints(int newPoints)
    {
        actionPoints = newPoints;
        if (actionPoints < 0)
        {
            actionPoints = 0;
        }
    }

    public bool UseActionPoint()
    {
        if (actionPoints > 0)
        {
            actionPoints--;
            Debug.Log($"Action point used. Remaining: {actionPoints}");
            return true;
        }
        Debug.Log("No action points remaining!");
        return false;
    }
}