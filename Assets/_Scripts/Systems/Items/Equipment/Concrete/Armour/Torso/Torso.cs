using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torso : Armour
{
    public Torso(string name, string description, float price, Sprite imageUI) : base(name, description, price, imageUI) { }
    public Torso(string name, string description, float price) : base(name, description, price) { }
}
