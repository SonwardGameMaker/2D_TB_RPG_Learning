using System;
using System.Collections.Generic;
using UnityEngine;

internal sealed class ModifierOperationsCollection
{
    private readonly Dictionary<ModifierType, Func<IModifierOperations>> _modifierOperationsDict = new();
    private bool _modifiersCollectionHasBeenReturned;

    internal ModifierType AddModifierOperation(int order, Func<IModifierOperations> modifierOperationsDelegate)
    {
        if (_modifiersCollectionHasBeenReturned)
            throw new InvalidOperationException("Cannot change collection after it has been returned");

        var modifierType = (ModifierType)order;

        if (modifierType is ModifierType.Flat or ModifierType.Additive or ModifierType.Multiplicative)
            Debug.LogWarning("modifier operations for types flat, additive and multiplicative cannot be changed! Default operations for these types will be used.");

        _modifierOperationsDict[modifierType] = modifierOperationsDelegate;

        return modifierType;
    }

    internal Dictionary<ModifierType, Func<IModifierOperations>> GetModifierOperations(int capacity)
    {
        _modifierOperationsDict[ModifierType.Flat] = () => new FlatModifierOperations();
        _modifierOperationsDict[ModifierType.Additive] = () => new AdditiveModifierOperations();
        _modifierOperationsDict[ModifierType.Multiplicative] = () => new MultiplicativeModifierOperations();

        _modifiersCollectionHasBeenReturned = true;

        return _modifierOperationsDict;
    }
}
