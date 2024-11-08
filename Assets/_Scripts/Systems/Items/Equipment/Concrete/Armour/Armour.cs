using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armour : EquipmentItem
{


    #region init
    public Armour(string name, string description, float price, Sprite imageUI) : base(name, description, price, imageUI) { }
    public Armour(string name, string description, float price) : base(name, description, price) { }
    #endregion
}
