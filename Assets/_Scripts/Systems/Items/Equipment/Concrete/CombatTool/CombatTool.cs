using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTool : EquipmentItem
{
    public CombatTool(string name, string description, float price, Sprite imageUI) : base(name, description, price, imageUI) { }
    public CombatTool(string name, string description, float price) : base(name, description, price) { }
}
