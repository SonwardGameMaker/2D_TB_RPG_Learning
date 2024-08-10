using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDataContainer
{
    public Weapon Source;
    public Damage Damage;
    public float WeaponSkill;

    public HitDataContainer(Weapon source, Damage damage, float weaponSkill)
    {
        Source = source;
        Damage = damage;
    }
    public HitDataContainer(Weapon source, float damageAmont, DamageType damageType, float weaponSkill)
    {
        Source = source;
        Damage = new Damage(damageAmont, damageType);
    }
}
