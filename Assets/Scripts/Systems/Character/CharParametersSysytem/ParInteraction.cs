using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ParInteraction
{
    private List<CharParameterBase> _affectors;
    private List<CharParameterBase> _targets;

    public ModValueCalculateLogic CalculateLogic { get; set; }

    public List<CharParameterBase> Affectors {  get { return _affectors; } private set { } }
    public List<CharParameterBase> Targets { get { return _targets; } private set { } }

    public ParInteraction(List<CharParameterBase> affectors, List<CharParameterBase> targets)
    {
        _affectors = affectors;
        _targets = targets;
    }
    public ParInteraction(List<CharParameterBase> affectors, CharParameterBase target) : this(affectors, new List<CharParameterBase> { target }) { }
    public ParInteraction(CharParameterBase affector, List<CharParameterBase> targets) : this(new List<CharParameterBase> { affector }, targets) { }
    public ParInteraction(CharParameterBase affector, CharParameterBase target) : this(new List<CharParameterBase> { affector }, new List<CharParameterBase> { target }) { }

    public void Affect()
    {
        if (CalculateLogic != null)
            CalculateLogic(ref _affectors, ref _targets);
        else Debug.LogErrorFormat("Affect logic is not set");
    }
}
