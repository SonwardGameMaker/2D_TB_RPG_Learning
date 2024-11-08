using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDurabilityMethodStandart : ItemBehaviourBase, IDisplayDurabilityMethod
{
    public string DisplayDurabilityRealization(CharResource duarbility)
        => $"{duarbility.CurrentValue}/{duarbility.MaxValue}";
}
