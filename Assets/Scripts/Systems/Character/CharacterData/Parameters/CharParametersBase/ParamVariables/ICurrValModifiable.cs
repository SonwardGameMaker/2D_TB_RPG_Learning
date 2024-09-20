using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICurrValModifiable
{
    public float CurrentValue { get; }
    public float CurrentValueBase { get; set; }

    public event Action CurrentValChanged;

    public void AddCurrentValueModifier(Modifier modifier);
    public IReadOnlyList<Modifier> GetCurrentValueModifiers();
    public IReadOnlyList<Modifier> GetCurrentValueModifiers(ModifierType modifierType);
    public bool TryRemoveCurrentValueModifier(Modifier modifier);
    public bool TryRemoveCurrentValueAllModifiersOf(object source);
}
