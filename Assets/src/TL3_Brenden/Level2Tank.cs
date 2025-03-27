using UnityEngine;

public class Level2Tank : TankType
{
    public override void Initialize()
    {
        health = 100;  // Set health specific to Level 2 Tank
       // Debug.Log("Level 2 Tank Spawned with " + health + " HP");
    }
}
