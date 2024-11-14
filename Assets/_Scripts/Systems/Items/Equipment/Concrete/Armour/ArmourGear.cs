using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourGear : EquipmentItem
{


    #region init
    public ArmourGear(string name, string description, float price, Sprite imageUI) : base(name, description, price, imageUI) { }
    public ArmourGear(string name, string description, float price) : base(name, description, price) { }
    public ArmourGear(ArmourGearSO itemSO) : base(itemSO)
    {

    }
    #endregion
}
