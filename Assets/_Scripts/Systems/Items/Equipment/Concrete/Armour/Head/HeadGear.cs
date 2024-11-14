using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadGear : ArmourGear
{
    public HeadGear(string name, string description, float price, Sprite imageUI) : base(name, description, price, imageUI) { }
    public HeadGear(string name, string description, float price) : base(name, description, price) { }
}
