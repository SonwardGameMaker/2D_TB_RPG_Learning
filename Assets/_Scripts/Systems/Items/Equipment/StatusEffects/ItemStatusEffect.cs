using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EquipmentItem
{
    #region inner classes
    public abstract class ItemStatusEffect
    {
        protected EquipmentItem _item;
        protected List<ItemBehaviourBase> _behaviours;

        public virtual void Setup(EquipmentItem item)
            => _item = item;

        public abstract void ApplyEffects();
        public abstract void RemoveEffects();

        protected void ReplaceBehaviour<Interface>(ItemBehaviourBase newBehaviour)
        {
            var temp = _item._itemBehaviours.Find(ib => ib is Interface);
            if (temp != null)
            {
                _item._itemBehaviours.Remove(temp);
                _item._itemBehaviours.Add(newBehaviour);
            }
        }
    }
    #endregion
}
