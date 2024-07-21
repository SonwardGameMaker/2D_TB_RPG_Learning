using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IArmorMitigationMod
{
    public float Mitigation { get; }
    public float MitigationBase { get; set; }

    public void AddMitigationValueModifier(Modifier modifier);
    public IReadOnlyList<Modifier> GetMitigationValueModifiers();
    public IReadOnlyList<Modifier> GetMitigationValueModifiers(ModifierType modifierType);
    public bool TryRemoveMitigationValueModifier(Modifier modifier);
    public bool TryRemoveMitigationValueAllModifiersOf(object source);
}
