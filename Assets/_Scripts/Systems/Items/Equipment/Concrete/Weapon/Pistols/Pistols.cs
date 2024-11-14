using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistols : Weapon
{
    public Pistols(string name,
        string description,
        int maxDamage,
        int minDamage,
        DamageType damageType,
        AttackRangeType attackRangeType,
        int attackRange = 1,
        float penetrationPercentage = 0,
        float critChancePercents = 0,
        float critDamageBonusPercents = 100,
        float price = 0,
        Sprite imageUI = null) : 
        base(
            name, 
            description, 
            maxDamage, 
            minDamage, 
            damageType, 
            attackRangeType, 
            attackRange, 
            penetrationPercentage,
            critChancePercents,
            critDamageBonusPercents,
            price,
            imageUI) { }
}
