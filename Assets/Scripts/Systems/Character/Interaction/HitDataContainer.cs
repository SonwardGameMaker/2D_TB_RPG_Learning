using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDataContainer
{
    public CharacterInfo Source;
    public Damage Damage;
    public float WeaponSkill;

    public HitDataContainer(CharacterInfo source, Damage damage, float weaponSkill)
    {
        Source = source;
        Damage = damage;
    }
    public HitDataContainer(CharacterInfo source, float damageAmont, DamageType damageType, float weaponSkill)
    {
        Source = source;
        Damage = new Damage(damageAmont, damageType);
    }
}
