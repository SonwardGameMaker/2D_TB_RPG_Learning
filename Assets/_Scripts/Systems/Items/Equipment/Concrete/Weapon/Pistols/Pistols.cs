using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistols : Weapon
{
    public Pistols(string name, string description, float price, Sprite imageUI) : base(name, description, price, imageUI) { }
    public Pistols(string name, string description, float price) : base(name, description, price) { }
}
