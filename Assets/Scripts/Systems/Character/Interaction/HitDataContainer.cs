using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDataContainer
{
    public IEquipable Source { get; set; }
    public Damage Damage { get; set; }

    public HitDataContainer(IEquipable source, Damage damage)
    {
        Source = source;
        Damage = damage;
    }
    public HitDataContainer(IEquipable source, DamageType damageType, float damageAmont)
    {
        Source = source;
        Damage = new Damage(damageAmont, damageType);
    }
}
