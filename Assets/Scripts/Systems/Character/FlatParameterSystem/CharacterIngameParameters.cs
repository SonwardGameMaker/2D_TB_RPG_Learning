using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIngameParameters
{
    public FlatParameter ArmorConsuming { get; set; }
    public FlatParameter ArmorTheshhold {  get; set; }

    public CharacterIngameParameters()
    {
        ArmorConsuming = new FlatParameter("Armor");
    }
}
