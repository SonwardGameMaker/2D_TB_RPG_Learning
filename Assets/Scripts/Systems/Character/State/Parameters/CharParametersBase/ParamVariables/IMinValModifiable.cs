using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMinValModifiable
{
    public float MinValue { get; }
    public float MinValueBase { get; set; }

    public event Action MinValChanged;

    public void AddMinValueModifier(Modifier modifier);
    public IReadOnlyList<Modifier> GetMinValueModifiers();
    public IReadOnlyList<Modifier> GetMinValueModifiers(ModifierType modifierType);
    public bool TryRemoveMinValueModifier(Modifier modifier);
    public bool TryRemoveMinValueAllModifiersOf(object source);
}
