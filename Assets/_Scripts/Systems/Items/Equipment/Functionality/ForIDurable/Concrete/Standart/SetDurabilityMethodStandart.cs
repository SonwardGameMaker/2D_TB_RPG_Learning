using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDurabilityMethodStandart : ItemBehaviourBase, ISetDurabilityMethod
{
    public void SetDurabilityRealization(CharResource durability, int amount)
        => durability.CurrentValue = amount;
}
