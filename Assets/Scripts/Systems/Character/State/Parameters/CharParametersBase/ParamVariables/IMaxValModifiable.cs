using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMaxValModifiable
{
    public float MaxValue { get; }
    public float MaxValueBase { get; set; }

    public event Action MaxValChanged;

    public void AddMaxValueModifier(Modifier modifier);
    public IReadOnlyList<Modifier> GetMaxValueModifiers();
    public IReadOnlyList<Modifier> GetMaxValueModifiers(ModifierType modifierType);
    public bool TryRemoveMaxValueModifier(Modifier modifier);
    public bool TryRemoveMaxValueAllModifiersOf(object source);
}
