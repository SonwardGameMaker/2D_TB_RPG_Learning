using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class ParInteractionCreator
{
    public float ModifierAmount;
    public ModifierType ModifierType;

    public ParInteractionCreator() { }
    public ParInteractionCreator(float modifierAmount, ModifierType modifierType)
    {
        ModifierAmount = modifierAmount;
        ModifierType = modifierType;
    }
    public ParInteractionCreator(ParInteractionCreator other) : this(other.ModifierAmount, other.ModifierType) { }

    public abstract ParInteraction CreateParInteraction(CharacterBlank character);

    protected abstract (float, ModifierType) CalculationLogic(CharParameterBase affector);

    protected FlatParameter CreateFlatParemFromFloat()
        => new FlatParameter("Item Modidfier", ModifierAmount);
}
