using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDataContainer
{
    public Weapon Source { get; set; }
    public Damage Damage { get; set; }

    public HitDataContainer(Weapon source, Damage damage)
    {
        Source = source;
        Damage = damage;
    }
    public HitDataContainer(Weapon source, float damageAmont, DamageType damageType)
    {
        Source = source;
        Damage = new Damage(damageAmont, damageType);
    }
}
