using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDurabilityMethodUnbreakable : ItemBehaviourBase, IDisplayDurabilityMethod
{
    public string DisplayDurabilityRealization(CharResource duarbility)
        => "Inf";
}
