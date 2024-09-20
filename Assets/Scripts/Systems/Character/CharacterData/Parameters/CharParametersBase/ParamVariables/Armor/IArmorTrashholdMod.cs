using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IArmorTrashholdMod
{
    public float Trashhold { get; }
    public float TrashholdBase { get; }

    public void AddTrashholdValueModifier(Modifier modifier);
    public IReadOnlyList<Modifier> GetTrashholdValueModifiers();
    public IReadOnlyList<Modifier> GetTrashholdValueModifiers(ModifierType modifierType);
    public bool TryRemoveTrashholdValueModifier(Modifier modifier);
    public bool TryRemoveTrashholdValueAllModifiersOf(object source);
}
