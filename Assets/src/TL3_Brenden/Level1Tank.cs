using UnityEngine;

public class Level1Tank : TankType
{
    public override void Initialize()
    {
        health = 50;
        // Debug.Log("Level 1 Tank Spawned with " + health + " HP");
        damage = 0;
        //damage = DamageScriptBC.getDamage();
    }
}
