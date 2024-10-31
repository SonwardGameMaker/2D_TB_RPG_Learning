using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IApCosted
{
    public int ApCost { get; }
    public ParInteraction CreateApCostEffect(List<CharParameterBase> affectors, ModValueCalculateLogic CalculateLogic);
    public ParInteraction CreateApCostEffect(CharParameterBase affector, ModValueCalculateLogic CalculateLogic)
        => CreateApCostEffect(new List<CharParameterBase> { affector }, CalculateLogic);
    public ParInteraction CreateApCostEffect((List<CharParameterBase>, ModValueCalculateLogic) parameters)
        => CreateApCostEffect(parameters.Item1, parameters.Item2);
}
