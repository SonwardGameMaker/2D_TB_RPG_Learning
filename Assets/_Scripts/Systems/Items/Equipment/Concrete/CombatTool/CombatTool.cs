using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTools : EquipmentItem
{
    public CombatTools(string name, string description, float price, Sprite imageUI) : base(name, description, price, imageUI) { }
    public CombatTools(string name, string description, float price) : base(name, description, price) { }
}
