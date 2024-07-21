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

    public ParInteraction(List<CharParameterBase> affectors, List<CharParameterBase> targets, ModValueCalculateLogic CalculateLogic)
    {
        _affectors = affectors;
        _targets = targets;
        foreach(CharParameterBase affector in _affectors)
        {
            affector.MinValChanged += Affect;
            affector.CurrentValChanged += Affect;
            affector.MaxValChanged += Affect;
        }
        this.CalculateLogic = CalculateLogic;
    }
    #region derived constructors and destructor
    public ParInteraction(List<CharParameterBase> affectors, List<CharParameterBase> targets) : this(affectors, targets, null) { }
    public ParInteraction(List<CharParameterBase> affectors, CharParameterBase target) : this(affectors, new List<CharParameterBase> { target }, null) { }
    public ParInteraction(CharParameterBase affector, List<CharParameterBase> targets) : this(new List<CharParameterBase> { affector }, targets, null) { }
    public ParInteraction(CharParameterBase affector, CharParameterBase target) : this(new List<CharParameterBase> { affector }, new List<CharParameterBase> { target }, null) { }
    public ParInteraction(List<CharParameterBase> affectors, CharParameterBase target, ModValueCalculateLogic CalculateLogic) : this(affectors, new List<CharParameterBase> { target }, CalculateLogic) { }
    public ParInteraction(CharParameterBase affector, List<CharParameterBase> targets, ModValueCalculateLogic CalculateLogic) : this(new List<CharParameterBase> { affector }, targets, CalculateLogic) { }
    public ParInteraction(CharParameterBase affector, CharParameterBase target, ModValueCalculateLogic CalculateLogic) : this(new List<CharParameterBase> { affector }, new List<CharParameterBase> { target }, CalculateLogic) { }
    ~ParInteraction() 
    {
        foreach(CharParameterBase affector in _affectors)
        {
            affector.MinValChanged -= Affect;
            affector.CurrentValChanged -= Affect;
            affector.MaxValChanged -= Affect;
        }
    }
    #endregion

    private void Affect()
    {
        if (CalculateLogic != null)
            CalculateLogic(ref _affectors, ref _targets);
        else Debug.LogErrorFormat("Affect logic is not set");
    }
}
