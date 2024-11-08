using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : Armour
{
    public Boots(string name, string description, float price, Sprite imageUI) : base(name, description, price, imageUI) { }
    public Boots(string name, string description, float price) : base(name, description, price) { }
}
