using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EquipmentItem
{
    public class UnbreakableStatus : ItemStatusEffect
    {
        public override void ApplyEffects()
        {
            ReplaceBehaviour<IChangeDurabilityMethod>(new ChangeDurabilityMethodUnbreakable());
            ReplaceBehaviour<ISetDurabilityMethod>(new SetDurabilityMethodUnbreakable());
            ReplaceBehaviour<IDisplayDurabilityMethod>(new DisplayDurabilityMethodUnbreakable());
        }

        public override void RemoveEffects()
        {
            ReplaceBehaviour<IChangeDurabilityMethod>(_item._defaultItemBehaviours.Find(ib => ib is IChangeDurabilityMethod));
            ReplaceBehaviour<ISetDurabilityMethod>(_item._defaultItemBehaviours.Find(ib => ib is ISetDurabilityMethod));
            ReplaceBehaviour<IDisplayDurabilityMethod>(_item._defaultItemBehaviours.Find(ib => ib is IDisplayDurabilityMethod));
        }
    }
}
