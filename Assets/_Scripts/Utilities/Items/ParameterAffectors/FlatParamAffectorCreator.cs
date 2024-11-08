using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FlatParamAffectorCreator : ParInteractionCreator
{
    public override ParInteraction CreateParInteraction(CharacterBlank character)
    {
        throw new System.NotImplementedException();
    }

    protected override (float, ModifierType) CalculationLogic(CharParameterBase affector)
        => new(ModifierAmount, ModifierType);
}
