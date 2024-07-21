using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterResistancesSystem 
{
    private List<DamageResistance> damageResistances;

    public CharacterResistancesSystem()
    {
        damageResistances = new List<DamageResistance>();
        foreach (DamageType damageType in Enum.GetValues(typeof(DamageType)))
        {
            damageResistances.Add(new DamageResistance(damageType));
        }
    }
}
