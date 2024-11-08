using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : Armour
{
    public Head(string name, string description, float price, Sprite imageUI) : base(name, description, price, imageUI) { }
    public Head(string name, string description, float price) : base(name, description, price) { }
}
