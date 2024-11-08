using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDurabilityMethodStandart : ItemBehaviourBase, IChangeDurabilityMethod
{
    public void ChangeDurabilityRealization(CharResource targetDurability, int amount)
        => targetDurability.CurrentValue += amount;
}
