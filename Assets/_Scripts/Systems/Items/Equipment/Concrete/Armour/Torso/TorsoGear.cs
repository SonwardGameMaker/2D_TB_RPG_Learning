using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorsoGear : ArmourGear
{
    public TorsoGear(string name, string description, float price, Sprite imageUI) : base(name, description, price, imageUI) { }
    public TorsoGear(string name, string description, float price) : base(name, description, price) { }
}
