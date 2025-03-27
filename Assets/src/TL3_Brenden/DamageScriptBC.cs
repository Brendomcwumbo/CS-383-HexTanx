using UnityEngine;

public class DamageScriptBC
{
    protected float damage = 100f;

    public virtual float getDamage()
    {
        return damage;
    }

    public virtual void setDamage(float damage)
    {
        Debug.Log("setDamage called in super class");
        damage = .1f;
   }
}

public class DamageScriptData : DamageScriptBC
{
    public override void setDamage(float damage)
    {
        Debug.Log("setHealth called in subclass");
        base.setDamage(damage);
    }
}
